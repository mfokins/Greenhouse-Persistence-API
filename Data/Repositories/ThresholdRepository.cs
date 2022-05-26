using Core.Interfaces;
using Core.Models;
using Data.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ThresholdRepository : IThresholdRepository
    {

        private Threshold GetThreshold(string greenhouseId, ThresholdType type)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();
            var thresholds = dbContext.Greenhouses
                .Include(g => g.Thresholds)
                ?.FirstOrDefault(t => t.GreenHouseId == greenhouseId)?.Thresholds
                .Where(th => th.Type == (Models.ThresholdType)type);
            if (!thresholds.Any())
            {
                return Threshold.Empty;
            }
            return DbToDom.Convert(thresholds.FirstOrDefault());
        }
        private void SetThreshold(string greenhouseId, Threshold threshold)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();
            
            var thresholds = dbContext.Greenhouses
                 .Include(g => g.Thresholds)
                ?.FirstOrDefault(t => t.GreenHouseId == greenhouseId)?
                .Thresholds;
            var thresholdsWithRightType = thresholds
                    .Where(th => th.Type == (Models.ThresholdType)threshold.Type);
            if (thresholdsWithRightType.Count() == 0)
            {
                thresholds.Add(DomToDb.Convert(threshold));
                dbContext.SaveChanges();

            }
            else
            {
                var editableThreshold = thresholdsWithRightType.FirstOrDefault();
                editableThreshold.LowerThreshold = threshold.LowerThreshold;
                editableThreshold.HigherThreshold = threshold.HigherThreshold;
                dbContext.SaveChanges();
            }
        }

        public Threshold GetDioxideCarbonThresholds(string greenhouseId)
        {

            return GetThreshold(greenhouseId, ThresholdType.DioxideCarbon);
        }

        public Threshold GetHumidityThresholds(string greenhouseId)
        {
            return GetThreshold(greenhouseId, ThresholdType.Humidity);
        }

        public Threshold GetMoisturehresholds(string greenhouseId, int potId)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();
            
            var pot = dbContext.Greenhouses
                           .Include(g => g.Pots)
                           ?.FirstOrDefault(t => t.GreenHouseId == greenhouseId)?.Pots?
                           .FirstOrDefault(t => t.Id == potId);
            if (pot == null)
            {
                return Threshold.Empty;
            }
            return pot.MoistureThresholdId == null ? Threshold.Empty : DbToDom.Convert(dbContext.Find<Models.Threshold>(pot.MoistureThresholdId));
        }

        public Threshold GetTemperatureThresholds(string greenhouseId)
        {
            return GetThreshold(greenhouseId, ThresholdType.Temperature);
        }

        public void SetDioxideCarbonThresholds(string greenhouseId, Threshold threshold)
        {
            SetThreshold(greenhouseId, threshold);
        }

        public void SetHumidityThresholds(string greenhouseId, Threshold threshold)
        {
            SetThreshold(greenhouseId, threshold);
        }

        public void SetMoistureThresholds(string greenhouseId, int potId, Threshold threshold)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();
            
            var pot = dbContext.Greenhouses
               .Include(g => g.Pots)
               ?.FirstOrDefault(t => t.GreenHouseId == greenhouseId)?.Pots?
               .FirstOrDefault(t => t.Id == potId);
            if (pot == null)
            {
                throw new Exception("Pot was not found");
            }
            var thresholdStored = pot.MoistureThreshold;
            throw new NotImplementedException();
            
        }

        public void SetTemperatureThresholds(string greenhouseId, Threshold threshold)
        {
            SetThreshold(greenhouseId, threshold);
        }
    }
}
