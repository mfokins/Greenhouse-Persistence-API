using System.Collections;
using Core.Interfaces.Temperature;
using Core.Models;
using Api.BridgeIot.Domain;

namespace Api.BridgeIot
{
    public interface IMessageHandler
    {
        public void HandleRxMessage(RxMessage message);
        public void HandleTxMessage(TxMessage message);
    }
}