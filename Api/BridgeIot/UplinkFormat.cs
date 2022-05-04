using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.BridgeIot
{
    public class UplinkFormat // this class describe structure of uplink described for lorawan from supervisors
    {
        public string cmd {get;set;}
        public int seqno {get;set;}
        public string EUI {get;set;} // device EUI
        public long ts {get;set;} // time stamp
        public bool ack {get;set;} 
        public long fcnt {get;set;} //frame counter
        public int port {get;set;}
        public string data {get;set;} // payload

        public UplinkFormat(string eui, int port, string data, bool? confirmed){
            this.cmd = "rx";
            this.EUI = eui;
            this.port = port;
            this.data = data;
        }
    }
}