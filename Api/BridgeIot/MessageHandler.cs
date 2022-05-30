/*
 * MessageHandler.cs
 *
 * Created: 5/23/2022 2:13:02 PM
 *  Author: Lukas
 */

using System.Collections;
using Core.Interfaces.Temperature;
using Core.Models;
using Api.BridgeIot.Domain;
using Core.Interfaces.Humidity;
using Core.Interfaces.DioxideCarbon;
using Core.Interfaces.Pot;
using Core.Interfaces.Greenhouse;
using Core.Interfaces;
using Core.Interfaces.Sensors;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api.BridgeIot
{
    public class MessageHandler : IMessageHandler
    {
        //there are precision that us ised for the tresholds - first min and max for each in temp, hum and co2
        private static readonly float[] tresholdPrecision = new float[4] {0.1f,0.1f,1f,1f};
        private static readonly int tresholdCount = 4;
        private ITemperatureService _tempService;
        private IHumidityService _humService;
        private IDioxideCarbonService _Co2Service;
        private IMoistureService _moistureService;
        private IPotService _potService;
        private ISensorService _sensorService;
        private IGreenhouseService _greenhouseService;


        private DownlinkHandler _downlinkHandler;
        private Action<TxMessage> _socketResponse;
        public MessageHandler(ITemperatureService tempService, IHumidityService humService, 
            IDioxideCarbonService co2Service, DownlinkHandler downlinkHandler, IMoistureService moistureService,
            IPotService potService, ISensorService sensorService, IGreenhouseService greenhouseService)
        {
            _tempService = tempService;
            _downlinkHandler = downlinkHandler;
            _humService = humService;
            _Co2Service = co2Service;
            _moistureService = moistureService;
            _potService = potService;
            _sensorService = sensorService;
            _greenhouseService = greenhouseService;
        }

        public void setResponseAction(Action<TxMessage> responseAction)
        {
            _socketResponse = responseAction;
        }

        public void HandleRxMessage(RxMessage message){
            float? temperature = null;
            float? humidity = null;
            int? CO2 = null;
            int[] moisture = null;
            HardwareStatus? status = null;

            switch (message.port)
            {
                case 2:
                    temperature = this.extractFromHexToInt(message.data, 2, 3, true) / 10.0F; //this is hardcoded value to know that temperature is on oth and 1st byte
                    Console.WriteLine(">>> Bridge: the temp is: {0}", temperature);
                    break;

                case 3:
                    temperature = this.extractFromHexToInt(message.data, 0, 1, true) / 10.0F; //this is hardcoded value to know that temperature is on oth and 1st byte
                    Console.WriteLine(">>> Bridge: the temp (v3) is: {0}", temperature);
                    break;

                case 4:
                    temperature = this.extractFromHexToInt(message.data, 0, 1, true) / 10.0F; //this is hardcoded value to know that temperature is on oth and 1st byte
                    humidity = this.extractFromHexToInt(message.data, 2, 3, false) / 10.0F;
                    Console.WriteLine(">>> Bridge: the temp (v4) is: {0}, and humidity: {1}", temperature, humidity);
                    break;

                case 5:
                    temperature = this.extractFromHexToInt(message.data, 0, 1, true) / 10.0F;
                    humidity = this.extractFromHexToInt(message.data, 2, 3, false) / 10.0F;
                    CO2 = this.extractFromHexToInt(message.data, 4, 5, false);
                    Console.WriteLine(">>> Bridge: the temp (v5) is: {0}, humidity: {1}, CO2: {2}", temperature, humidity, CO2);
                    break;

                case 6:
                    temperature = this.extractFromHexToInt(message.data, 0, 1, true) / 10.0F;
                    humidity = this.extractFromHexToInt(message.data, 2, 3, false) / 10.0F;
                    CO2 = this.extractFromHexToInt(message.data, 4, 5, false);
                    Console.Write(">>> Bridge: the temp (v6) is: {0}, humidity: {1}, CO2: {2}, moisture: ", temperature, humidity, CO2);
                    moisture = this.readMoisture(message.data);
                    Console.Write("\n");
                    break;
                case 7:
                    //the same as the above
                    temperature = this.extractFromHexToInt(message.data, 0, 1, true) / 10.0F;
                    humidity = this.extractFromHexToInt(message.data, 2, 3, false) / 10.0F;
                    CO2 = this.extractFromHexToInt(message.data, 4, 5, false);
                    Console.Write(">>> Bridge: the temp (v7) is: {0}, humidity: {1}, CO2: {2}, moisture: ", temperature, humidity, CO2);
                    moisture = this.readMoisture(message.data);
                    Console.Write("\n");

                    //read sensor status here
                    int sensorStatuses = extractFromHexToInt(message.data, 12, 12, false);
                    // this is a char converted to int - ones on bit positions mean working zero means not working

                    status = HardwareStatus.fromCharToHardwareStatus(sensorStatuses, 0, 1, 2, 3);
                    break;
            }

            string greenhouseEUI = message.EUI;
            long unixInSec = message.ts / 1000; // I get time in milisec from epoch, C# need it in seconds

            //-------------------------------------------------------------------------------------------------
            
            if (temperature != null){
                TemperatureMeasurement thisTemp = new TemperatureMeasurement();
                thisTemp.Temperature = (float)temperature;
                thisTemp.GreenHouseId = greenhouseEUI; 

                thisTemp.Time = DateTimeOffset.FromUnixTimeSeconds(unixInSec).DateTime.ToLocalTime();
                
                _tempService.Add(thisTemp);
            }

            if (humidity != null)
            {
                HumidityMeasurement thisHum = new HumidityMeasurement();
                thisHum.Humidity = (float)humidity;
                thisHum.GreenHouseId= greenhouseEUI;

                thisHum.Time = DateTimeOffset.FromUnixTimeSeconds(unixInSec).DateTime.ToLocalTime();

                _humService.Add(thisHum);
            }

            if (CO2 != null)
            {
                DioxideCarbonMeasurement thisCo2 = new DioxideCarbonMeasurement();
                thisCo2.Co2Measurement = (int)CO2;
                thisCo2.GreenHouseId = greenhouseEUI;

                thisCo2.Time = DateTimeOffset.FromUnixTimeSeconds(unixInSec).DateTime.ToLocalTime();
                
                _Co2Service.Add(thisCo2);
            }

            if (moisture != null)
            {
                for (int i = 0; i < moisture.Length; i++)
                {
                    MoistureMeasurement thisMoisture = new MoistureMeasurement();

                    thisMoisture.Moisture = moisture[i];
                    thisMoisture.PotId = i;
                    thisMoisture.GreenHouseId = greenhouseEUI;

                    thisMoisture.Time = DateTimeOffset.FromUnixTimeSeconds(unixInSec).DateTime.ToLocalTime();

                    _moistureService.Add(thisMoisture, i);
                }
                /*//get all pots that need the data
                IEnumerator<Pot> pots = _potService.GetAll(greenhouseEUI).GetEnumerator();

                
                int index = 0;

                while (pots.MoveNext() && index<6)
                {
                    MoistureMeasurement thisMoisture = new MoistureMeasurement();
                    
                    thisMoisture.Moisture = moisture[index++];
                    thisMoisture.PotId = pots.Current.Id;
                    thisMoisture.GreenHouseId = greenhouseEUI;

                    thisMoisture.Time = DateTimeOffset.FromUnixTimeSeconds(unixInSec).DateTime.ToLocalTime();
                    //Add here the id of the sensor
                    var sendorId = 3;
                    _moistureService.Add(thisMoisture, sendorId);
                }
                //we have 6 sensors allways, they specify which of them did they set up*/
                
            }

            if (status != null)
            {
                Console.WriteLine(">>> Bridge: statuses: [ temp: {0}, hum: {1}, co2: {2}, moisture: {3} ] ",
                    status.temperatureWorking,status.humidityWorking,status.co2Working,status.moistureWorking);

                try
                {
                    // storing statuses in database
                    //saving status for temperature
                    SensorStatus tempSensor = new SensorStatus() { Type = SensorType.Temperature, IsWorking = status.temperatureWorking };
                    _sensorService.SetSensorStatus(tempSensor, greenhouseEUI, null);

                    //saving status for humidity
                    SensorStatus humiditySensor = new SensorStatus() { Type = SensorType.Humidity, IsWorking = status.humidityWorking };
                    _sensorService.SetSensorStatus(humiditySensor, greenhouseEUI, null);

                    //making it for co2
                    SensorStatus co2Sensor = new SensorStatus() { Type = SensorType.Co2, IsWorking = status.co2Working };
                    _sensorService.SetSensorStatus(co2Sensor, greenhouseEUI, null);
                } catch (NotImplementedException ex)
                {
                    Console.WriteLine("!!!!!!!!!!!!! sensor state is not implemented in data part!!!");
                }
                
            }
            //return;
            sendTresholds(message.EUI); // checking if tresholds were updated and sending it
        }

        public void HandleTxMessage(TxMessage message)
        {
            return;
        }

        private int[] readMoisture(string messageData)
        {
            int[] moisture = new int[6];
            for (int i = 0; i < 6; i++)
            {
                moisture[i] = this.extractFromHexToInt(messageData, i + 6, i + 6, false);
                Console.Write("{0}, ", moisture[i]);
            }
            
            return moisture;
        }

        private void sendTresholds(string EUI){
            if (!_downlinkHandler.isThresholdChanged(EUI))
            {
                return;

            }
            float[] tresholds = _downlinkHandler.getTresholds(EUI);
            int[] roundedTresholds = new int[tresholdCount];
            for (int i = 0; i < tresholdCount; i++){
                roundedTresholds[i] = (int) (tresholds[i]/tresholdPrecision[i]);
            }
            string  myData = "";

            
            for (int i = 0; i< tresholdCount; i++){
                myData = convertFromIntToHex(
                    myData,
                    4*i,
                    (4*i)+3, // each number corespont to 2 characters
                    roundedTresholds[tresholdCount - 1 - i] //this is in opposite order so it can utilise simple string concat
                );
            }
            TxMessage finalMessage = new TxMessage("tx",EUI,"",true,1,myData);
            Console.WriteLine(">>> Bridge: Data to be send (tx): {0}", finalMessage.data);
            _socketResponse(finalMessage);

        }

        private string convertFromIntToHex(string data, int firstByte, int lastByte,int number){
            //I include last and also first byte
            string myString = data;

            if (number < 0)
            { //if the number is negative make it second compliment
                number += (int) Math.Pow(16, lastByte - firstByte + 1); 
            }

            while(lastByte>=firstByte){
                char tmpCh;
                int tempNum = number% 16;
                number = number/16;

                if (tempNum >=0 && tempNum <=9) tmpCh = Convert.ToChar(tempNum + '0');
                else tmpCh = Convert.ToChar(tempNum - 10 + 'a');
                myString = tmpCh + myString;
                lastByte--;
            }
            return myString;
        }

        // this method convert hex values to integers. 
        // data is string of the entire data part, data is then in first byte and last byte inclusvely
        private int extractFromHexToInt(string data,int firstByte, int lastByte, bool isSigned){
            ArrayList hexChar = new ArrayList();

            //calculate the first and last char index for the string
            int startIndex = firstByte*2;
            int lastIndex = lastByte*2+1;

            int curentIndex = startIndex;
            int finalValue = 0;

            do {
                finalValue *= 16; // multiply the previous by 16 because we are at different index now
                //Console.WriteLine("char: {0}, is {1}",data[curentIndex],convertCharToHex(data[curentIndex]));
                int value = convertCharToHex(data[curentIndex]);

                if (isSigned) {
                    int maxPositiveValue = (int)Math.Pow(2, 8 * (lastByte - firstByte + 1)) / 2 - 1;
                    //                       bit value^|      ^bit count |^ byte count   |      ^max number in signed int
                    if (value > maxPositiveValue) value = (maxPositiveValue * 2 - value);
                } 
                
                
                finalValue += value;
                //Console.WriteLine(finalValue);
                curentIndex++;
            } while (curentIndex <=lastIndex);

            return finalValue;
        }

        private int convertCharToHex(char ch){
            if (ch>='a' && ch<='f'){ // if the char is lowercase a-f then get order after a and add 10
                return (ch -'a') + 10; 
            }
            if (ch>='A' && ch<='F'){ // if the char is uppercase A-F then get order after A and add 10
                return (ch -'A') +10;
            }
            if (ch>='0' && ch<='9'){ // if the char is number then get the order afte 0
                return ch - '0';
            }
            throw new ArgumentOutOfRangeException("char is not a letter form a to F or number");
        }

        public void HandleGwMessage(GwMessage message)
        {
            string greenhouseId = message.EUI;

            //from my observation the closest tower, and the one that arduiono connects to is first in list
            Gateway connectedGateway = message.gws.ToArray()[0];

            float lon = connectedGateway.lon;
            float lat = connectedGateway.lat;
            //TODO send this to an interface when there will be some

            try
            {
                Greenhouse greenhouse = new Greenhouse()
                {
                    GreenHouseId = greenhouseId,
                    Latitude = lat,
                    Longitude = lon
                };
                _greenhouseService.UpdateGreenhouse(greenhouse);
            } catch (NotImplementedException ex)
            {
                Console.WriteLine("!!!!!!!updating greenhouse location is not made");
            }
        }
    }
}