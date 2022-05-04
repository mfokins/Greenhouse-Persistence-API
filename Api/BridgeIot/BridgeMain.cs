using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace Api.BridgeIot
{
    public class BridgeMain
    {
        static ClientWebSocket ws = new ClientWebSocket();
        public static void startBridge(){
            Console.WriteLine("connection initialised!");

            Uri loraWanUri = new Uri("wss://iotnet.teracom.dk/app?token=*********");
            ws.ConnectAsync(loraWanUri,CancellationToken.None).ContinueWith(async (t)=>{
                Thread.Sleep(2000);
                Console.WriteLine("connected sucessfully!");
                
                await readFromDevice("0004A30B00E7E7C1");
                
                Console.WriteLine("connection gonna close");
                await ws.CloseAsync(WebSocketCloseStatus.NormalClosure,null,CancellationToken.None);
            });
        }

        private static async Task<UplinkFormat?> readFromDevice(string EUI){
            /*//make object to be send
            UplinkFormat packetToSend = new UplinkFormat(EUI,1,"", null);

            // serialize the obejct to string and make byte array to be send
            string s = JsonSerializer.Serialize(packetToSend);
            byte[] dataToServer = Encoding.ASCII.GetBytes(s);

            await ws.SendAsync(dataToServer,WebSocketMessageType.Text,true,CancellationToken.None);*/

            // now receive data from the lorawan
            byte[] dataFromServer = new byte[10000];
            WebSocketReceiveResult answer = await ws.ReceiveAsync(dataFromServer,CancellationToken.None);

            Console.WriteLine("I have received something");

            string response = Encoding.ASCII.GetString(dataFromServer, 0, answer.Count);

            Console.WriteLine("the response that we got : {0}",response);

            UplinkFormat? returnMessage = JsonSerializer.Deserialize<UplinkFormat>(response);

            Console.WriteLine("object: {0}",returnMessage);

            if (returnMessage != null){
                Console.WriteLine("the cmd is: {0}",returnMessage.cmd);
            }

            return returnMessage;
        }
    }
}