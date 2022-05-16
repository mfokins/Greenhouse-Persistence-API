using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text.Json;


namespace Api.BridgeIot
{
    public class RxMessage : LoraWANMessage// this class describe structure of uplink described for lorawan from supervisors
    {
        //public string cmd {get;set;}
        public int seqno {get;set;}
        //public string EUI {get;set;} // device EUI
        public long ts {get;set;} // time stamp
        public bool ack {get;set;} 
        public long fcnt {get;set;} //frame counter
        public int port {get;set;}
        public string data {get;set;} // payload    

        public RxMessage(string cmd, string EUI, string json, int seqno, long ts,bool ack,long fcnt, int port, string data): base(cmd,EUI,json) {

            this.seqno = seqno;
            this.ts = ts;
            this.ack = ack;
            this.fcnt = fcnt;
            this.port = port;
            this.data = data;
        }

        public static RxMessage GetRxMessage(string json){
            RxMessage theMessage = JsonSerializer.Deserialize<RxMessage>(json);
            if (theMessage != null){
                theMessage.json = json;
            }
            return theMessage;
        }
    }
}