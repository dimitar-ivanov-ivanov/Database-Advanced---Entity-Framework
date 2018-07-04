namespace MassDefect.Stores
{
    using MassDefect.Data;
    using MassDefect.Dtos;
    using MassDefect.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PlanetStore
    {
        public static void AddPlanets(ICollection<PlanetDto> planetDtos)
        {
            using (var context = new MassDefectContext())
            {
                foreach (var planetDto in planetDtos)
                {
                    var sun = StarStore.GetStarByName(planetDto.Sun, context);
                    var solarSystem = SolarSystemStore.GetSolarSystemByName(planetDto.SolarSystem, context);

                    if(sun == null || solarSystem == null ||
                       planetDto.Name == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var planet = new Planet()
                    {
                        Name = planetDto.Name,
                        Sun = sun,
                        SolarSystem = solarSystem
                    };

                    Console.WriteLine($"Successfully imported planet {planet.Name}");
                    context.Planets.Add(planet);
                }

                context.SaveChanges();
            }
        }

        public static Planet GetPlanetByName(string name,MassDefectContext context)
        {
            return context.Planets.FirstOrDefault(p => p.Name == name);
        }

        public static ICollection<PlanetExportDto> ExportPlanetsWhichAreNotAnomalyOrigins()
        {
            using(var context = new MassDefectContext())
            {
                var planets = context.Planets
                    .Where(p => !p.OriginAnomalies.Any())
                    .Select(p => new PlanetExportDto()
                    {
                        Name = p.Name
                    })
                    .ToList();

                return planets;
            }
        }
    }
}
