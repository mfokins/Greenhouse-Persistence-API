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
                ?.FirstOrDefault(t => t.GreenHouseId == greenhouseId)?.Thresholds;
            if (thresholds == null)
            {
                return Threshold.Empty;
            }
            foreach (Models.Threshold threshold in thresholds)
            {
                if (threshold.Type == (Models.ThresholdType)type)
                {
                    return DbToDom.Convert();
                }
            }
            return Threshold.Empty;
        }
        private void SetThreshold(string greenhouseId, Threshold threshold)
        {
            var thresholds = _dbContext.Greenhouses
                .Include(g => g.Thresholds)
                ?.FirstOrDefault(t => t.GreenHouseId == greenhouseId)?.Thresholds;
            if (thresholds == null)
            {
                thresholds.Add(DomToDb.Convert(threshold));
                _dbContext.SaveChanges();

            }
            int? thresholdId = null;
            foreach (Models.Threshold thresholdInList in thresholds)
            {
                if (thresholdInList.Type == (Models.ThresholdType)threshold.Type)
                {
                    thresholdId = thresholdInList.Id;
                    break;
                }
            }
            if (thresholdId != null)
            {
                Models.Threshold thresholdConverted = DomToDb.Convert(threshold);
                thresholdConverted.Id = thresholdId ?? default(int);
                _dbContext.Update(thresholdConverted);
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
            return GetThreshold(greenhouseId, ThresholdType.Humidity);
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
            if(thresholdStored == null)
            {

            }
        }

        public void SetTemperatureThresholds(string greenhouseId, Threshold threshold)
        {
            SetThreshold(greenhouseId, threshold);
        }
    }
}
