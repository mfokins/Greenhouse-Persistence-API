using System.Collections.Generic;
using Core.Interfaces;
using Core.Models;

namespace Core.Interfaces.DioxideCarbon
{

    public interface IDioxideCarbonRepository : IDataReadRepository<DioxideCarbonMeasurement>,
        IDataWriteRepository<DioxideCarbonMeasurement>
    {
        /// <summary>
        /// Returns all the CO2 measurements from the database from a specific greenhouse
        /// </summary>
        /// <param name="greenhouseId"> The greenhouse in the database owned by a user</param>
        /// <returns>A collection of all the CO2 measurement</returns>
        IEnumerable<DioxideCarbonMeasurement> GetAll(string greenhouseId);

        /// <summary>
        /// Returns the latest CO2 measurements from the database from a specific greenhouse
        /// </summary>
        /// <param name="greenhouseId">The greenhouse in the database owned by a user</param>
        /// <returns>The latest measurement</returns>
        DioxideCarbonMeasurement GetLatest(string greenhouseId);
    }
}