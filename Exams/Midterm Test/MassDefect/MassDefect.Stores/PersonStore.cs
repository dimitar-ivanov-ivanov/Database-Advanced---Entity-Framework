namespace MassDefect.Stores
{
    using MassDefect.Data;
    using MassDefect.Dtos;
    using MassDefect.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PersonStore
    {
        public static void AddPersons(ICollection<PersonDto> personDtos)
        {
            using(var context = new MassDefectContext())
            {
                foreach (var personDto in personDtos)
                {
                    var planet = PlanetStore.GetPlanetByName(personDto.HomePlanet, context);
                    if(planet == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var person = new Person()
                    {
                        Name = personDto.Name,
                        HomePlanet = planet,
                        HomePlanetId = planet.Id
                    };

                    Console.WriteLine($"Successfully added {person.Name}");
                    context.Persons.Add(person);
                }
                context.SaveChanges();
            }
        }

        public static ICollection<PersonExportDto> PeopleWhichHaveNotBeenVictimsOfAnomalies()
        {
            using(var context = new MassDefectContext())
            {
                var people = context.Persons
                    .Where(p => !p.AnomalyVictims.Any())
                    .Select(p => new PersonExportDto()
                    {
                        name = p.Name,
                        homePlanet = new PlanetExportDto()
                        {
                            Name = p.HomePlanet.Name
                        }
                    })
                    .ToList();
                return people;
            }
        }

        public static Person GetPersonByName(string name, MassDefectContext context)
        {
            return context.Persons.FirstOrDefault(p => p.Name == name);
        }
    }
}
