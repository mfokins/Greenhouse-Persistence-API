/*
 * MessageHandler.cs
 *
 * Created: 5/27/2022
 *  Author: Lukas
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;

namespace Api.BridgeIot.Domain
{
    public class GwMessage :LoraWANMessage
    {
        public List<Gateway> gws { get; set; }

        public GwMessage(string cmd, string EUI, string json, List<Gateway> gws) :base(cmd,EUI,json)
        {
            this.gws = gws;
        }

        public static GwMessage GetGwMessage(string json)
        {
            GwMessage theMessage = JsonSerializer.Deserialize<GwMessage>(json);
            if (theMessage != null)
            {
                theMessage.json = json;
            }
            return theMessage;
        }
    }
}
