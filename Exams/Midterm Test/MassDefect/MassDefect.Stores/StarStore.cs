namespace MassDefect.Stores
{
    using MassDefect.Data;
    using MassDefect.Dtos;
    using MassDefect.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class StarStore
    {
        public static void AddStars(ICollection<StarDto> starDtos)
        {
            using (var context = new MassDefectContext())
            {
                foreach (var starDto in starDtos)
                {
                    var solarSystem = SolarSystemStore.GetSolarSystemByName(starDto.SolarSystem, context);
                    if (starDto.Name == null || solarSystem == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var star = new Star()
                    {
                        Name = starDto.Name,
                        SolarSystem = solarSystem
                    };

                    context.Stars.Add(star);
                    Console.WriteLine($"Successfully imported Star {star.Name}.");
                }
                context.SaveChanges();
            }
        }

        public static Star GetStarByName(string name,MassDefectContext context)
        {
            return context.Stars.FirstOrDefault(s=>s.Name == name);
        }
    }
}
