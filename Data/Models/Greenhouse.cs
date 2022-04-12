using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Greenhouse
    {
        [Key]
        public string GreenHouseId { get; set; }
        [Required]
        public IList<TemperatureMesurment> TemperatureMesurments { get; set; }
        [Required]
        public IList<CO2Mesurment> CO2Mesurments { get; set; }
        [Required]
        public IList<HumidityMesurment> HumidityMesurments { get; set; }
        [Required]
        public IList<LuminosityMesurment> LuminosityMesurments { get; set; }

        [Required]
        public IList<Pot> Pots { get; set; }

    }
}
