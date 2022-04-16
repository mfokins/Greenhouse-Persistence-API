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
        public IList<TemperatureMeasurement> TemperatureMesurments { get; set; }
        [Required]
        public IList<DioxideCarbonMeasurement> Co2Measurements { get; set; }
        [Required]
        public IList<HumidityMeasurement> HumidityMeasurements { get; set; }
        [Required]
        public IList<LuminosityMeasurement> LuminosityMesurments { get; set; }

        [Required]
        public IList<Pot> Pots { get; set; }

    }
}
