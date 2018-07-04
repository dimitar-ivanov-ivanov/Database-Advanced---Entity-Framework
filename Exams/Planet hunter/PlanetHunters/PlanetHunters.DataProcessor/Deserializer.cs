namespace PlanetHunters.DataProcessor
{
    using Newtonsoft.Json;
    using PlanetHunters.Data;
    using PlanetHunters.DataProcessor.Dtos.Import;
    using PlanetHunters.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessfulMessage = "Record {0} successfully imported.";

        public static string ImportAstronomers(PlanetHuntersContext context, string json)
        {
            var builder = new StringBuilder();
            var astronomers = JsonConvert.DeserializeObject<ICollection<Astronomer>>(json);
            var validAstronomers = new List<Astronomer>();

            foreach (var ast in astronomers)
            {
                if (!isValid(ast))
                {
                    builder.AppendLine(FailureMessage);
                    continue;
                }

                validAstronomers.Add(ast);
                builder.AppendLine(string.Format(SuccessfulMessage, ast.FirstName + " " + ast.LastName));
            }

            context.Astronomers.AddRange(validAstronomers);
            context.SaveChanges();

            return builder.ToString();
        }

        public static string ImportTelescopes(PlanetHuntersContext context, string json)
        {
            var builder = new StringBuilder();
            var telescopes = JsonConvert.DeserializeObject<ICollection<TelescopeDto>>(json);
            var validTelescopes = new List<Telescope>();

            foreach (var tel in telescopes)
            {
                if (!isValid(tel))
                {
                    builder.AppendLine(FailureMessage);
                    continue;
                }

                var mirrorDiameter = decimal.Parse(tel.MirrorDiameter);
                if (mirrorDiameter <= 0)
                {
                    builder.AppendLine(FailureMessage);
                    continue;
                }

                validTelescopes.Add(new Telescope()
                {
                    MirrorDiameter = mirrorDiameter,
                    Location = tel.Location,
                    Name = tel.Name
                });
                builder.AppendLine(String.Format(SuccessfulMessage, tel.Name));
            }

            context.Telescopes.AddRange(validTelescopes);
            context.SaveChanges();
            return builder.ToString();
        }

        public static string ImportPlanets(PlanetHuntersContext context, string json)
        {
            var builder = new StringBuilder();
            var planets = JsonConvert.DeserializeObject<ICollection<PlanetDto>>(json);
            var validPlanets = new List<Planet>();

            foreach (var pl in planets)
            {
                if (!isValid(pl))
                {
                    builder.AppendLine(FailureMessage);
                    continue;
                }

                var mass = decimal.Parse(pl.Mass);

                if (mass <= 0)
                {
                    builder.AppendLine(FailureMessage);
                    continue;
                }

                var starSystem = context.StarSystems.FirstOrDefault(ss => ss.Name == pl.StarSystem);
                var planet = new Planet()
                {
                    Mass = mass,
                    Name = pl.Name
                };

                if (starSystem == null)
                {
                    starSystem = new StarSystem()
                    {
                        Name = pl.StarSystem,
                    };
                    starSystem.Planets.Add(planet);
                }

                planet.StarSystemId = starSystem.Id;
                validPlanets.Add(planet);
            }

            context.Planets.AddRange(validPlanets);
            context.SaveChanges();
            return builder.ToString();
        }

        public static string ImportStars(PlanetHuntersContext context, string xml)
        {
            var builder = new StringBuilder();
            return builder.ToString();
        }

        public static string ImportDiscoveries(PlanetHuntersContext context, string json)
        {
            var builder = new StringBuilder();
            return builder.ToString();
        }

        public static bool isValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var res = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(obj, validationContext, res, true);
            return isValid;
        }
    }
}
