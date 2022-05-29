using Core.Models;
using Data;
using Data.Repositories;

TimeSpan[] sunrise = new TimeSpan[]
{
    new TimeSpan(8, 26, 0),
    new TimeSpan(7, 31, 0),
    new TimeSpan(6, 22, 0),
    new TimeSpan(6, 2, 0),
    new TimeSpan(4, 55, 0),
    new TimeSpan(4, 22, 0),
    new TimeSpan(4, 44, 0),
    new TimeSpan(5, 40, 0),
    new TimeSpan(6, 39, 0),
    new TimeSpan(7, 38, 0),
    new TimeSpan(7, 42, 0),
    new TimeSpan(8, 30, 0),
};


TimeSpan[] sunset = new TimeSpan[]
{
    new TimeSpan(16, 11, 0),
    new TimeSpan(17, 15, 0),
    new TimeSpan(18, 16, 0),
    new TimeSpan(20, 16, 0),
    new TimeSpan(21, 16, 0),
    new TimeSpan(21, 57, 0),
    new TimeSpan(21, 46, 0),
    new TimeSpan(20, 48, 0),
    new TimeSpan(19, 30, 0),
    new TimeSpan(18, 12, 0),
    new TimeSpan(16, 6, 0),
    new TimeSpan(15, 39, 0),
};
bool clouds = false;
bool night = false;


DateTime time = DateTime.Now;
time = new DateTime(2021, 12, time.Day, sunset[time.Month - 1].Hours - 2, time.Minute, time.Second);
TemperatureRepository tempRep = new TemperatureRepository();
HumidityRepository humRep = new HumidityRepository();
DioxideCarbonRepository carbonRepository = new DioxideCarbonRepository();
GreenhouseRepository greenhouseRepository = new GreenhouseRepository();

ThresholdRepository thresholdRepository = new ThresholdRepository();
PotRepository potRepository = new PotRepository(thresholdRepository);
MoistureRepository moistureRepository = new MoistureRepository();

ICollection<Core.Models.TemperatureMeasurement> temps = new List<Core.Models.TemperatureMeasurement>();
ICollection<Core.Models.HumidityMeasurement> hums = new List<Core.Models.HumidityMeasurement>();
ICollection<Core.Models.DioxideCarbonMeasurement> carbons = new List<Core.Models.DioxideCarbonMeasurement>();

ICollection<Core.Models.Pot> pots = new List<Core.Models.Pot>();
ICollection<Core.Models.MoistureMeasurement> moistures = new List<Core.Models.MoistureMeasurement>();

for (int i = 0; i < 5; i++)
{
    string greenhouseid = Guid.NewGuid().ToString();
    greenhouseRepository.Add(new Core.Models.Greenhouse() {GreenHouseId = greenhouseid});
    double temp = 30;
    double humidity = 85;
    double co2 = 330;
    time = new DateTime(2021, 12, time.Day, sunset[time.Month - 1].Hours - 2, time.Minute, time.Second);
    double moisture = 18;

    generatePots(greenhouseid, generateThreshold());
    IEnumerable<Pot> currentPots = potRepository.GetAll(greenhouseid, 0, 25);
    List<Pot> potList = currentPots.ToList();

    while (time < DateTime.Now)
    {
        double newTemp = generateTemp(temp, time);
        double newHumidity = generateHumidity(newTemp, humidity, temp);
        int newCo2 = (int) Math.Ceiling(generateCo2(co2));

        temps.Add(new Core.Models.TemperatureMeasurement()
        {
            GreenHouseId = greenhouseid,
            Temperature = (float) newTemp,
            Time = time
        });
        hums.Add(new Core.Models.HumidityMeasurement()
        {
            GreenHouseId = greenhouseid,
            Humidity = (float) newHumidity,
            Time = time
        });
        carbons.Add(new Core.Models.DioxideCarbonMeasurement()
        {
            GreenHouseId = greenhouseid,
            Co2Measurement = newCo2,
            Time = time
        });

        for (int potLoop = 0; potLoop <= currentPots.Count() - 1; potLoop++)
        {
            double newMoisture = generateMoisture(newTemp, moisture, temp);
            moisture = newMoisture;
            if (checkMoisture(potList[potLoop].moistureThreshold, newMoisture))
            {
                var diff = potList[potLoop].moistureThreshold.LowerThreshold - newMoisture;
                if (diff > 7.5)
                {
                    newMoisture = diff + 2.5;
                }
            }

            moistures.Add(new Core.Models.MoistureMeasurement()
            {
                GreenHouseId = greenhouseid,
                Moisture = newMoisture,
                Time = time,
                PotId = potList[potLoop].Id
            });
        }

        Console.WriteLine(
            $"{time.ToString("dd/MM/yyyy")} {time.ToString("HH:mm:ss")} temp: {newTemp} humidity: {newHumidity} co2: {newCo2}");
        time = time.Add(new TimeSpan(0, 5, 0));
        temp = newTemp;
        humidity = newHumidity;
        co2 = newCo2;
        if (temps.Count >= 1000)
        {
            
            tempRep.AddBulk(temps);
            humRep.AddBulk(hums);
            carbonRepository.AddBulk(carbons);
            moistureRepository.AddBulk(moistures);
            temps.Clear();
            hums.Clear();
            carbons.Clear();
            moistures.Clear();
            Console.WriteLine("Added 1000 elements");
        }
        //Thread.Sleep(50);
    }
    Console.WriteLine("Added 1000 elements");
    tempRep.AddBulk(temps);
    humRep.AddBulk(hums);
    carbonRepository.AddBulk(carbons);
    moistureRepository.AddBulk(moistures);
    temps.Clear();
    hums.Clear();
    carbons.Clear();
    moistures.Clear();
    potList.Clear();
}

double generateCo2(double oldCo2)
{
    if (clouds)
    {
        return getRandomNumber(oldCo2, (450 - oldCo2) / 50 + oldCo2);
    }
    else
    {
        return getRandomNumber((oldCo2 - (oldCo2 - 200) / 50), oldCo2);
    }
    //var rnd = getRandomNumber(0.005, 0.1);
    //return clouds ? oldCo2 + (oldCo2 * rnd) : oldCo2 - (oldCo2 * rnd);
}

double generateHumidity(double temp, double humidity, double oldTemp)
{
    Random rnd = new Random();

    var Diff = ((temp - oldTemp) * rnd.NextDouble()) * 0.25;
    if (Diff > 5)
    {
        Diff = 5;
    }

    if (Diff < -5)
    {
        Diff = -5;
    }

    return Diff + humidity > 100 ? 100 : Diff + humidity;
}

double generateTemp(double prev, DateTime time)
{
    if (time.TimeOfDay > sunrise[time.Month - 1] &&
        time.TimeOfDay < sunset[time.Month - 1].Subtract(new TimeSpan(4, 0, 0)))
    {
        if (night)
        {
            night = false;
            clouds = SetClouds();
        }

        return prev
               + getRandomForUpTemperature(prev);
    }

    if (!night)
    {
        night = true;
        clouds = SetClouds();
    }

    return prev - getRandomForDownTemperature(prev);
}

double getRandomNumber(double minimum, double maximum)
{
    Random random = new Random();
    return random.NextDouble() * (maximum - minimum) + minimum;
}

double getRandomForDownTemperature(double prev)
{
    Random rnd = new Random();
    double down;
    if (clouds)
    {
        down = (prev * rnd.NextDouble()) * 0.025;
    }
    else
    {
        down = (prev * rnd.NextDouble()) * 0.07;
    }

    return down;
}

double getRandomForUpTemperature(double prev)
{
    Random rnd = new Random();
    double up;
    if (clouds)
    {
        up = rnd.NextDouble() / (prev > 1 ? prev * 0.15 : 2);
    }
    else
    {
        up = rnd.NextDouble() / (prev > 1 ? prev * 0.07 : 2);
    }

    return up;
}

//get random number between 0 and 1
bool SetClouds()
{
    Random rnd = new Random();
    var cloudChance = rnd.Next(0, 100);
    if (cloudChance < 50)
    {
        clouds = true;
    }
    else
    {
        clouds = false;
    }

    return clouds;
}

double generateMoisture(double temp, double moisture, double oldTemp)
{
    Random rnd = new Random();
    var Diff = ((temp - oldTemp) * rnd.NextDouble()) * 2.23;
    if (Diff > 5)
    {
        Diff = 5;
    }

    if (Diff < -5)
    {
        Diff = -5;
    }

    return Diff + moisture > 30 ? 30 : Diff + moisture;
}

void generatePots(string greenhouseId, Threshold threshold)
{
    string[] nameOfPlants =
    {
        "Bamboo", "Beans", "ABELIA", "ABELMOSCHUS", "ABYSSINIAN BANANA",
        "ACACIA ACALYPHA", "ACHIMENES ACONITE", "MONKSHOOD", "AECHMEA",
        "AEONIUM", "AFRICAN CORN", "GARDENIA", "GARLIC", "GAS PLANT",
        "GAURA LINDHEIMERI", "GAZANIA DAISY", "GENTIAN", " GERANIUM",
        "GERBERA DAISY TRANSVAAL DAISY", "GERMAN STATICE",
        "GERMANDER", "GIANT REED", "GINGER", "GINGER LILY",
        "GLADIOLUS", "GLOBE AMARANTH", "GLOBE THISTLE", "GLOBEFLOWER"
    };
    Random rnd = new Random();
    var randomPlant = rnd.Next(0, nameOfPlants.Length);
    for (int i = 0; i < 6; i++)
    {
        pots.Add(new Core.Models.Pot()
        {
            GreenHouseId = greenhouseId,
            Name = nameOfPlants[randomPlant],
            moistureThreshold = threshold
        });
    }

    potRepository.AddBulk(pots);
    pots.Clear();
}

Threshold generateThreshold()
{
    Random rnd = new Random();
    var rndNo = rnd.NextDouble() * (30 - 15) + 15;
    return new Threshold()
    {
        Type = ThresholdType.Moisture,
        LowerThreshold = rndNo,
    };
}


bool checkMoisture(Threshold threshold, double currentMoisture)
{
    return currentMoisture < threshold.LowerThreshold;
}