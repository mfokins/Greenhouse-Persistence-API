namespace Core.Models;

public class MoistureMeasurement
{
    public string GreenHouseId { get; set; }

    public double Moisture { get; set; }

    public DateTime Time { get; set; }
    public int PotId { get; set; }
}