using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text;
using System.Text.Json;

namespace Api.BridgeIot.Domain
{
    public class LoraWANMessage
    {
        public string cmd {get;set;}
        public string EUI {get;set;}
        public string json {get;set;}

        //constructor to be used be deserializer
        public LoraWANMessage(string cmd, string EUI,string json){
            this.cmd = cmd;
            this.EUI = EUI;
            this.json = "";
        }

        public static LoraWANMessage getLoraWANMessage(string json){
            LoraWANMessage theMessage = JsonSerializer.Deserialize<LoraWANMessage>(json);
            if (theMessage != null){
                theMessage.json = json;
            }
            return theMessage;
        }
    }
}