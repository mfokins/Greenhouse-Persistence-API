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

namespace Api.BridgeIot.Domain
{
    public class Gateway
    {
        public string gwEUI { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }

        public Gateway(string gwEUI, float lat, float lon)
        {
            this.gwEUI = gwEUI;
            this.lat = lat;
            this.lon = lon;
        }

    }
}
