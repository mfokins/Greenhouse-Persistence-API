using System.Collections;
using Core.Interfaces.Temperature;
using Core.Models;
using Api.BridgeIot.Domain;
using Core.Interfaces.Humidity;
using Core.Interfaces.DioxideCarbon;

namespace Api.BridgeIot
{
    public class MessageHandler : IMessageHandler
    {
        //there are precision that us ised for the tresholds - first min and max for each in temp, hum and co2
        private static float[] tresholdPrecision = new float[6] {1f,1f,1f, 1f,1f,1f};
        private ITemperatureService _tempService;
        private IHumidityService _humService;
        private IDioxideCarbonService _Co2Service;
        private DownlinkHandler _downlinkHandler;
        //private BridgeMain _socketResponse;
        public MessageHandler(ITemperatureService tempService, IHumidityService humService, IDioxideCarbonService co2Service, DownlinkHandler downlinkHandler){
            _tempService = tempService;
            _downlinkHandler = downlinkHandler;
            //_socketResponse = socketResponse;
            _humService = humService;
            _Co2Service = co2Service;
        }

        public void HandleRxMessage(RxMessage message){
            float? temperature = null;
            float? humidity = null;
            int? CO2 = null;
            if (message.port == 2){
                temperature = this.extractFromHexToInt(message.data, 2,3) /10.0F; //this is hardcoded value to know that temperature is on oth and 1st byte
                Console.WriteLine("the temp is: {0}",temperature);
                
            }
            if (message.port == 3){
                temperature = this.extractFromHexToInt(message.data, 0,1)/ 10.0F; //this is hardcoded value to know that temperature is on oth and 1st byte
                Console.WriteLine("the temp (v3) is: {0}",temperature);
            }
            if (message.port == 4)
            {
                temperature = this.extractFromHexToInt(message.data, 0, 1) / 10.0F; //this is hardcoded value to know that temperature is on oth and 1st byte
                humidity = this.extractFromHexToInt(message.data, 2, 3) / 10.0F;
                Console.WriteLine("the temp (v4) is: {0}, and humidity: {1}", temperature, humidity);
            }
            if (message.port == 5)
            {
                temperature = this.extractFromHexToInt(message.data, 0, 1) / 10.0F;
                humidity = this.extractFromHexToInt(message.data, 2, 3) / 10.0F;
                CO2 = this.extractFromHexToInt(message.data, 4, 5);
                Console.WriteLine("the temp (v5) is: {0}, humidity: {1}, CO2: {2}", temperature, humidity,CO2);
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

            if (_downlinkHandler.isThresholdChanged(message.EUI)){
                _downlinkHandler.getTresholds(message.EUI); //don't send messages to arduino, it is not tested.
            }
        }

        public void testMethod(string EUI){
            if (_downlinkHandler.isThresholdChanged(EUI)){
                sendTresholds(_downlinkHandler.getTresholds(EUI),EUI);
            }
        }

        public void HandleTxMessage(TxMessage message){
            return;//TODO
        }

        private void sendTresholds(float[] tresholds,string EUI){
            int[] roundedTresholds = new int[6];
            for (int i = 0; i < 6;i++){
                roundedTresholds[i] = (int) (tresholds[i]*tresholdPrecision[i]);
            }
            string  myData = "";

            // each number corespont to 2 characters
            for (int i = 0; i<6; i++){
                myData = convertFromIntToHex(myData,2*i,(2*i)+1,roundedTresholds[5-i]);
            }
            TxMessage finalMessage = new TxMessage("tx",EUI,null,true,100,myData);
            //_socketResponse.send(finalMessage);

        }

        private string convertFromIntToHex(string data, int firstByte, int lastByte,int number){
            //I include last and also first byte so I need to add 1
            //int counter = 0;
            string myString = data;

            while(lastByte>=firstByte){
                char tmpCh;
                int tempNum = number% 16;
                number = number/16;
                if (tempNum >=0 && tempNum <9) tmpCh = Convert.ToChar(tempNum + '0');
                else tmpCh = Convert.ToChar(tempNum - 10 + 'a');
                myString = tmpCh + myString;
                lastByte--;
            }
            return myString;
        }

        // this method convert hex values to integers. 
        // data is string of the entire data part, data is then in first byte and last byte inclusvely
        private int extractFromHexToInt(string data,int firstByte, int lastByte){
            ArrayList hexChar = new ArrayList();

            //calculate the birst and last char index for the string
            int startIndex = firstByte*2;
            int lastIndex = lastByte*2+1;

            int curentIndex = startIndex;
            int finalValue = 0;

            do {
                finalValue *= 16; // multiply the previous by 16 because we are at different index now
                //Console.WriteLine("char: {0}, is {1}",data[curentIndex],convertCharToHex(data[curentIndex]));
                finalValue += convertCharToHex(data[curentIndex]);
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
    }
}