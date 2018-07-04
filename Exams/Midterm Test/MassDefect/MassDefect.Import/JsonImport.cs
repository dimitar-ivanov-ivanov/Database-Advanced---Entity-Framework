namespace MassDefect.Import
{
    using MassDefect.Dtos;
    using MassDefect.Stores;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;

    public static class JsonImport
    {
        public static void ImportSolarSystems()
        {
            var json = File.ReadAllText("../imports/solar-systems.json");
            var solarSystemDtos = JsonConvert.DeserializeObject<ICollection<SolarSystemDto>>(json);
            SolarSystemStore.AddSolarSystems(solarSystemDtos);
        }

        public static void ImportStars()
        {
            var json = File.ReadAllText("../imports/stars.json");
            var starDtos = JsonConvert.DeserializeObject<ICollection<StarDto>>(json);
            StarStore.AddStars(starDtos);
        }

        public static void ImportPlanets()
        {
            var json = File.ReadAllText("../imports/planets.json");
            var planetDtos = JsonConvert.DeserializeObject<ICollection<PlanetDto>>(json);
            PlanetStore.AddPlanets(planetDtos);
        }

        public static void ImportPersons()
        {
            var json = File.ReadAllText("../imports/persons.json");
            var personDtos = JsonConvert.DeserializeObject<ICollection<PersonDto>>(json);
            PersonStore.AddPersons(personDtos);
        }

        public static void ImportAnomalies()
        {
            var json = File.ReadAllText("../imports/anomalies.json");
            var anomalyDtos = JsonConvert.DeserializeObject<ICollection<AnomalyDto>>(json);
            AnomalyStore.AddAnomalies(anomalyDtos);
        }

        public static void ImportAnomalyVictims()
        {
            var json = File.ReadAllText("../imports/anomaly-victims.json");
            var anomalyVictimDtos = JsonConvert.DeserializeObject<ICollection<AnomalyVictimDto>>(json);
            AnomalyVictimStore.AddAnomalyVictims(anomalyVictimDtos);
        }
    }
}
