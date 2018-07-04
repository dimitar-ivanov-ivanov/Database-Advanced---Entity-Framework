namespace MassDefect.Stores
{
    using MassDefect.Data;
    using MassDefect.Dtos;
    using MassDefect.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AnomalyStore
    {
        public static void AddAnomalies(ICollection<AnomalyDto> anomalyDtos)
        {
            using (var context = new MassDefectContext())
            {
                foreach (var anomalyDto in anomalyDtos)
                {
                    var originPlanet = PlanetStore.GetPlanetByName(anomalyDto.OriginPlanet, context);
                    var teleportPlanet = PlanetStore.GetPlanetByName(anomalyDto.TeleportPlanet, context);

                    if (originPlanet == null || teleportPlanet == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var anomaly = new Anomaly()
                    {
                        HomePlanet = originPlanet,
                        TeleportPlanet = teleportPlanet
                    };

                    context.Anomalies.Add(anomaly);
                    Console.WriteLine($"Successfuly added anomaly from {originPlanet.Name} to {teleportPlanet.Name}");
                }

                context.SaveChanges();
            }
        }

        public static void AddNewAnomalies(ICollection<NewAnomalyDto> anomalyDtos)
        {
            using (var context = new MassDefectContext())
            {
                foreach (var anomalyDto in anomalyDtos)
                {
                    var originPlanet = PlanetStore.GetPlanetByName(anomalyDto.OriginPlanet, context);
                    var teleportPlanet = PlanetStore.GetPlanetByName(anomalyDto.TeleportPlanet, context);

                    if (originPlanet == null || teleportPlanet == null ||
                        anomalyDto.Victims == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    if (anomalyDto.Victims.Count == 0)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var anomaly = new Anomaly()
                    {
                        HomePlanet = originPlanet,
                        TeleportPlanet = teleportPlanet
                    };

                    foreach (var victimDto in anomalyDto.Victims)
                    {
                        if (victimDto != null)
                        {
                            var victim = PersonStore.GetPersonByName(victimDto.Name, context);
                            var anomalyVictim = AnomalyVictimStore.GetAnomalyVictimByIds(anomaly.Id, victim.Id, context);

                            if (anomalyVictim == null)
                            {
                                anomalyVictim = new AnomalyVictim()
                                {
                                    Anomaly = anomaly,
                                    Victim = victim
                                };
                                context.AnomalyVictims.Add(anomalyVictim);
                            }

                            anomaly.AnomalyVictims.Add(anomalyVictim);
                        }
                    }

                    Console.WriteLine("Successfully imported anomaly.");
                    context.Anomalies.Add(anomaly);
                }

                context.SaveChanges();
            }
        }
        
        public static AnomalyExportDto AnomalyWhichAffectedTheMostPeople()
        {
            using (var context = new MassDefectContext())
            {
                var anomaly = context.Anomalies
                    .OrderByDescending(a => a.AnomalyVictims.Select(av => av.VictimId)
                    .ToHashSet().Count)
                    .FirstOrDefault();

                var res = new AnomalyExportDto()
                {
                    id = anomaly.Id,
                    teleportPlanet = new PlanetExportDto()
                    {
                        Name = anomaly.TeleportPlanet?.Name
                    },
                    originPlanet = new PlanetExportDto()
                    {
                        Name = anomaly.HomePlanet?.Name
                    },
                    victimsCount = anomaly.AnomalyVictims.Count
                };

                return res;
            }
        }

        public static Anomaly GetAnomalyById(int id, MassDefectContext context)
        {
            return context.Anomalies.Find(id);
        }
    }
}
