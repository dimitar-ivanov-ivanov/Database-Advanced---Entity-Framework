namespace MassDefect.Stores
{
    using System;
    using System.Collections.Generic;
    using MassDefect.Dtos;
    using MassDefect.Data;
    using MassDefect.Models;
    using System.Linq;

    public static class SolarSystemStore
    {
        public static void AddSolarSystems(ICollection<SolarSystemDto> solarSystemDtos)
        {
            using (var context = new MassDefectContext())
            {
                foreach (var solarSystemDto in solarSystemDtos)
                {
                    if (solarSystemDto.Name == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    context.SolarSystems.Add(new SolarSystem()
                    {
                        Name = solarSystemDto.Name
                    });

                    Console.WriteLine($"Successfully imported Solar System {solarSystemDto.Name}.");
                }
                context.SaveChanges();
            }
        }

        public static SolarSystem GetSolarSystemByName(string name, MassDefectContext context)
        {
            return context.SolarSystems.FirstOrDefault(ss => ss.Name == name);
        }
    }
}
