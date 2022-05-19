using System.Text.Json;

namespace Api.BridgeIot.Domain
{
    public class TxMessage : LoraWANMessage
    {
        public bool confirmed {get;set;}
        public int port {get;set;}
        public string data {get;set;}
        public TxMessage(string cmd, string EUI, string json, bool confirmed, int port, string data): base(cmd,EUI,json) {
            this.confirmed = confirmed;
            this.port = port;
            this.data = data;
        }

        public static TxMessage GetTxMessage(string json){
            TxMessage theMessage = JsonSerializer.Deserialize<TxMessage>(json);
            if (theMessage != null){
                theMessage.json = json;
            }
            return theMessage;
        }

        public string getJson(){
            return JsonSerializer.Serialize(this);

        }
    }
}