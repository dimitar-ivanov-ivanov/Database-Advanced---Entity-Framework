namespace MassDefect.Export
{
    using MassDefect.Stores;
    using Newtonsoft.Json;
    using System.IO;

    public static class JsonExport
    {
        public static void ExportPlanetsWhichAreNotAnomalyOrigins()
        {
            var planets = PlanetStore.ExportPlanetsWhichAreNotAnomalyOrigins();
            var planetsJson = JsonConvert.SerializeObject(planets, Formatting.Indented);
            File.WriteAllText("../exports/planets.json", planetsJson);
        }

        public static void PeopleWhichHaveNotBeenVictimsOfAnomalies()
        {
            var persons = PersonStore.PeopleWhichHaveNotBeenVictimsOfAnomalies();
            var personsJson = JsonConvert.SerializeObject(persons, Formatting.Indented);
            File.WriteAllText("../exports/people.json", personsJson);
        }

        public static void AnomalyWhichAffectedTheMostPeople()
        {
            var anomaly = AnomalyStore.AnomalyWhichAffectedTheMostPeople();
            var anomalyJson = JsonConvert.SerializeObject(anomaly, Formatting.Indented);
            File.WriteAllText("../exports/anomaly.json", anomalyJson);
        }
    }
}
