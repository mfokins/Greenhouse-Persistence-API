using System.Collections;
using Core.Interfaces.Temperature;
using Core.Models;

namespace Api.BridgeIot
{
    public class MessageHandler
    {
        private ITemperatureService _tempService;
        public MessageHandler(ITemperatureService tempService){
            _tempService = tempService;
            // so far no need to have something saved because 
            // there is no need to have somehting persistant so far
        }

        public void HandleRxMessage(RxMessage message){
            int? temperature = null;
            if (message.port == 2){
                temperature = this.extractFromHexToInt(message.data, 2,3); //this is hardcoded value to know that temperature is on oth and 1st byte
                Console.WriteLine("the temp is: {0}",temperature/10.0);

                
            }
            if (message.port == 3){
                temperature = this.extractFromHexToInt(message.data, 0,1); //this is hardcoded value to know that temperature is on oth and 1st byte
                Console.WriteLine("the temp (v3) is: {0}",temperature/10.0);
            }

            if (temperature != null){
                TemperatureMeasurement thisTemp = new TemperatureMeasurement();
                thisTemp.Temperature = (float)temperature / 10.0F; //TODO change after the temperature will be changed to float
                thisTemp.GreenHouseId = "test"; //Change to message.EUI when database will be able to create new greenhouse records

                long unixInSec = message.ts / 1000; // I get time in milisec from epoch, C# need it in seconds
                thisTemp.Time = DateTimeOffset.FromUnixTimeSeconds(unixInSec).DateTime.ToLocalTime();
                
                _tempService.Add(thisTemp);
            }
            
            
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