﻿using Data.Models.Measurements;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Pot
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        public int MoistureSensorId { get; set; }
        public int MoistureThresholdId { get; set; }
        [ForeignKey("MoistureThresholdId")]
        public Threshold MoistureThreshold { get; set; }
        [Required]
        public IList<MoistureMeasurement> MoistureMeasurements { get; set; }
    }
}