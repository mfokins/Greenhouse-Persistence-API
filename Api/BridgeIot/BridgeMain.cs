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
using Api.BridgeIot.Domain;

namespace Api.BridgeIot
{
    public class BridgeMain : BackgroundService
    {
        private IServiceScopeFactory _scopeFactory;

        static ClientWebSocket ws = new ClientWebSocket();
        static IMessageHandler messageHandler;
        static DownlinkHandler downlinkHandler;

        public BridgeMain(IServiceScopeFactory factory){
            _scopeFactory = factory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken){
            messageHandler = _scopeFactory.CreateScope().ServiceProvider.GetService<IMessageHandler>();
            if (messageHandler == null)
            {
                Console.WriteLine("no service!");
                return;
            }
            /*new MessageHandler(_scopeFactory.CreateScope().ServiceProvider.GetService<ITemperatureService>(),
                _scopeFactory.CreateScope().ServiceProvider.GetService<DownlinkHandler>(),
                this);*/
            downlinkHandler = new DownlinkHandler();

            Console.WriteLine(">>> Bridge: connection initialised!");

            string? token = Environment.GetEnvironmentVariable("BRIDGE_TOKEN");

            if (token == null)
            {
                Console.WriteLine("set up enviroment variables first");
                return;
            }

            Uri loraWanUri = new Uri("wss://iotnet.teracom.dk/app?token=" + token);
            await ws.ConnectAsync(loraWanUri,CancellationToken.None);

            Thread.Sleep(2000);
            Console.WriteLine(">>> Bridge: connected sucessfully!");
            //messageHandler.testMethod("0004A30B00E7E7C1");
            
            while (!stoppingToken.IsCancellationRequested){
                LoraWANMessage message = await readFromDevices();
                receive(message);
            }
             
            Console.WriteLine(">>> Bridge: connection gonna close");
            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure,null,CancellationToken.None);
        }

        private void receive(LoraWANMessage message){
            switch (message.cmd){
                case "rx": //this is message from arduino
                    RxMessage theMessage = RxMessage.GetRxMessage(message.json);
                    messageHandler.HandleRxMessage(theMessage);
                    break;
                case "tx": //this is message from lorawan about succesfull enquee
                    //do nothing this just say that the messsage was enqued
                    //Console.WriteLine(message.json);
                    TxMessage txMessage = TxMessage.GetTxMessage(message.json);
                    messageHandler.HandleTxMessage(txMessage);
                    break;
                case "gw": //this message do not have reason for our project, it gives info about gateway
                    break;
                case "txd": //this is confirmation about downlik if requested
                    //Console.WriteLine(">>> Bridge: "+message.json);
                    break;
            }
        }

        public void send(TxMessage message){
            //TODO finish the convert from object to socket
            string jsonMessage = message.getJson();
            //Console.WriteLine("message: "+jsonMessage);
            byte[] dataToServer = Encoding.ASCII.GetBytes(jsonMessage);
            ws.SendAsync(dataToServer,WebSocketMessageType.Text,true,CancellationToken.None);
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