using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Core.Interfaces.Temperature;

namespace Api.BridgeIot
{
    public class BridgeMain : BackgroundService
    {
        private IServiceScopeFactory _scopeFactory;

        static ClientWebSocket ws = new ClientWebSocket();
        static MessageHandler messageHandler;

        public BridgeMain(IServiceScopeFactory factory){
            _scopeFactory = factory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken){
            messageHandler = new MessageHandler(_scopeFactory.CreateScope().ServiceProvider.GetService<ITemperatureService>());

            Console.WriteLine(">>> Bridge: connection initialised!");

            Uri loraWanUri = new Uri("wss://iotnet.teracom.dk/app?token=*********");
            await ws.ConnectAsync(loraWanUri,CancellationToken.None);

            Thread.Sleep(2000);
            Console.WriteLine(">>> Bridge: connected sucessfully!");
            
            while (!stoppingToken.IsCancellationRequested){
                LoraWANMessage message = await readFromDevices();
                receive(message);
            }
             
            Console.WriteLine(">>> Bridge: connection gonna close");
            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure,null,CancellationToken.None);
        }

        private void receive(LoraWANMessage message){//TODO remove this
            if (message.cmd == "rx"){
                RxMessage theMessage = RxMessage.GetRxMessage(message.json);
                messageHandler.HandleRxMessage(theMessage);
            }
        }

        private static async Task<LoraWANMessage> readFromDevices(){
            // make a buffer that will be big eneught to get json from lorawan
            byte[] dataFromServer = new byte[3000];

            //wait until I get some message
            WebSocketReceiveResult answer = await ws.ReceiveAsync(dataFromServer,CancellationToken.None);

            Console.WriteLine(">>> Bridge: Message received form lorawan");

            string response = Encoding.ASCII.GetString(dataFromServer, 0, answer.Count);

            LoraWANMessage? returnMessage = LoraWANMessage.getLoraWANMessage(response);//JsonSerializer.Deserialize<LoraWANMessage>(response);

            

            return returnMessage;
        }
    }
}