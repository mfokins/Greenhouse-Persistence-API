using Core.Interfaces.Temperature;
using Core.Models;
using Data.Repositories;


namespace Api.BridgeIot
{
    public class Class1
    {
        TemperatureRepository tempRep;
        private ITemperatureService _service;

        public Class1(ITemperatureService service)
        {
            _service = service;
        }
        public void testMethod(){
            string response = "{\"cmd\":\"rx\",\"seqno\":107,\"EUI\":\"0004A30B00E7E7C1\",\"ts\":1651649069890,\"fcnt\":122,\"port\":2,\"freq\":867100000,\"rssi\":-110,\"snr\":-6,\"toa\":0,\"dr\":\"SF8 BW125 4/5\",\"ack\":false,\"bat\":255,\"offline\":false,\"data\":\"303902a3041a\"}";
            
            //start of IoT bridge
            Console.WriteLine("Start of bridge");

            TemperatureMeasurement myTemperature = new TemperatureMeasurement();
            myTemperature.GreenHouseId = "10000";
            myTemperature.Temperature = 50;
            myTemperature.Time = DateTime.Now;

            //humidityWriteRepo.Add(myHumidity);
            _service.Add(myTemperature);
        }

        public static void initClass1(ITemperatureService service)
        {
            Class1 myClass = new Class1(service);
            myClass.testMethod();
        }
    }
}
