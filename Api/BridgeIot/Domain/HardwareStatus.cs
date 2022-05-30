using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BridgeIot.Domain
{
    internal class HardwareStatus
    {
        public static readonly int sensorStatusCount = 4;
        public bool temperatureWorking { get; set; }
        public bool humidityWorking { get; set; }
        public bool co2Working { get; set; }
        public bool moistureWorking { get; set; }

        public HardwareStatus()
        {
            temperatureWorking = false;
            humidityWorking = false;
            co2Working = false;
            moistureWorking = false;
        }

        public static HardwareStatus fromCharToHardwareStatus(int data, int temperatureBit, int humidityBit, int co2Bit, int moistureBit)
        {
            HardwareStatus result = new HardwareStatus();  // empty constructor make everything false by default
            bool[] statuses = new bool[sensorStatusCount]; 

            for (int i = 0; i < sensorStatusCount; i++)
            {
                if (data%2 == 1) // one stands for working zero stands for broken
                {
                    statuses[i] = true;
                }
                data /= 2;

            }

            if (temperatureBit >= 0 && temperatureBit < sensorStatusCount)
            {
                result.temperatureWorking = statuses[temperatureBit];
            }

            if (humidityBit >= 0 && humidityBit < sensorStatusCount)
            {
                result.humidityWorking = statuses[humidityBit];
            }

            if (co2Bit  >= 0 && co2Bit < sensorStatusCount)
            {
                result.co2Working = statuses[co2Bit];
            }

            if (moistureBit >= 0 && moistureBit < sensorStatusCount)
            {
                result.moistureWorking = statuses[moistureBit];
            }

            return result;
        }
    }
}
