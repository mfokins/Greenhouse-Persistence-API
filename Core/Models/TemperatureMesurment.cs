using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class TemperatureMesurment
    {
        public int Temperature { get; set; }
        public DateTime Time { get; set; }
        public string GreenHouseId { get; set; }
    }
}
