using System.Collections.Generic;
using Core.Interfaces;
using Core.Models;

namespace Api.BridgeIot
{
    public class DownlinkHandler
    {
        private Dictionary<string,DateTime> lastTresholdsSent;
        private IThresholdService _thresholdService;
        public DownlinkHandler(IThresholdService thresholdService)
        {
            lastTresholdsSent = new Dictionary<string, DateTime>();
            _thresholdService = thresholdService;
        }

        public bool isThresholdChanged(string EUI){
            //there is currently no need and no option to check if the treshold was changed so allway return true
            return true;

            //--------------------------------------------------------------------------------
            if (!lastTresholdsSent.ContainsKey(EUI)){
                Console.WriteLine("bridge got asked about new treshold and returned true");
                return true;
            }

            DateTime lastTresholdSent = lastTresholdsSent.GetValueOrDefault(EUI);

            //mocked up date
            DateTime lastTresholdDatabase = new DateTime(1990,1,1);

            if (lastTresholdDatabase > lastTresholdSent){
                return true;
            }
            return false;
        }

        public float[] getTresholds(string EUI){
            //do not add it because we send it every time
            //lastTresholdsSent.Add(EUI,DateTime.Now); 
            //just mocked up data

            float min_temp = -20;
            float max_temp = 60; //the way it is implemented there have to be some default values
            Threshold tempTreshold = _thresholdService.GetTemperatureThresholds(EUI);
            if (tempTreshold.Type != ThresholdType.Empty)
            {
                min_temp = (int) tempTreshold.LowerThreshold;

                //because max treshold can be null I need to check it
                if (tempTreshold.HigherThreshold != null) max_temp = (int) tempTreshold.HigherThreshold;
                Console.WriteLine("temperature got changed");
            }

            int min_co2 = 0;
            int max_co2 = 5000;
            Threshold co2Treshold = _thresholdService.GetHumidityThresholds(EUI);
            if (co2Treshold.Type != ThresholdType.Empty)
            {
                min_co2 = (int)co2Treshold.LowerThreshold;

                //because max treshold can be null I need to check it
                if (co2Treshold.HigherThreshold != null) max_co2 = (int)co2Treshold.HigherThreshold;
            }

            return new float[] {min_temp,max_temp,min_co2,max_co2}; // this is definition for the order of values
        }
    }
}