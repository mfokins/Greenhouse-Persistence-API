using Core.Interfaces;
using Core.Models;
using Data.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ThresholdRepository : IThresholdRepository
    {
        private GreenHouseDbContext _dbContext;

        public ThresholdRepository(GreenHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private Threshold GetThreshold(string greenhouseId, ThresholdType type)
        {
            var thresholds = _dbContext.Greenhouses
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
            var thresholds = _dbContext.Greenhouses
                 .Include(g => g.Thresholds)
                ?.FirstOrDefault(t => t.GreenHouseId == greenhouseId)?
                .Thresholds;
            var thresholdsWithRightType = thresholds
                    .Where(th => th.Type == (Models.ThresholdType)threshold.Type);
            if (thresholdsWithRightType.Count() == 0)
            {
                thresholds.Add(DomToDb.Convert(threshold));
                _dbContext.SaveChanges();

            }
            else
            {
                var editableThreshold = thresholdsWithRightType.FirstOrDefault();
                editableThreshold.LowerThreshold = threshold.LowerThreshold;
                editableThreshold.HigherThreshold = threshold.HigherThreshold;
                _dbContext.SaveChanges();
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
            var pot = _dbContext.Greenhouses
                           .Include(g => g.Pots)
                           ?.FirstOrDefault(t => t.GreenHouseId == greenhouseId)?.Pots?
                           .FirstOrDefault(t => t.Id == potId);
            if (pot == null)
            {
                return Threshold.Empty;
            }
            return pot.MoistureThreshold == null ? Threshold.Empty : DbToDom.Convert(pot.MoistureThreshold);
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
            var pot = _dbContext.Greenhouses
               .Include(g => g.Pots)
               ?.FirstOrDefault(t => t.GreenHouseId == greenhouseId)?.Pots?
               .FirstOrDefault(t => t.Id == potId);
            if (pot == null)
            {
                throw new Exception("Pot was not found");
            }
            var thresholdStored = pot.MoistureThreshold;
            if (thresholdStored == null)
            {

            }
        }

        public void SetTemperatureThresholds(string greenhouseId, Threshold threshold)
        {
            SetThreshold(greenhouseId, threshold);
        }
    }
}
