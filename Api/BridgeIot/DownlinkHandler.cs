using System.Collections.Generic;

namespace Api.BridgeIot
{
    public class DownlinkHandler
    {
        private Dictionary<string,DateTime> lastTresholdsSent;
        public DownlinkHandler(){
            lastTresholdsSent = new Dictionary<string, DateTime>();
        }

        public bool isThresholdChanged(string EUI){
            if (!lastTresholdsSent.ContainsKey(EUI)){
                
                return true;
            }

            DateTime lastTresholdSent = lastTresholdsSent.GetValueOrDefault(EUI);

            //mocked up date
            DateTime lastTresholdUpdate = new DateTime(1990,1,1);

            if (lastTresholdUpdate > lastTresholdSent){
                return true;
            }
            return false;
        }

        public float[] getTresholds(string EUI){
            lastTresholdsSent.Add(EUI,DateTime.Now);
            //just mocked up data
            float min_temp = 0.0f;
            float max_temp = 50.2f;

            float min_hum = 0.0f;
            float max_hum = 100.0f;

            float min_co2 = 0.0f;
            float max_co2 = 20000.3f;

            return new float[] {min_temp,max_temp,min_hum,max_hum,min_co2,max_co2};

        }
    }
}