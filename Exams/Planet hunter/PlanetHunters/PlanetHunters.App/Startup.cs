namespace PlanetHunters.App
{
    using PlanetHunters.Data;
    using PlanetHunters.DataProcessor;
    using System;
    using System.IO;
    using System.Text;

    public class Startup
    {
        public static void Main(string[] args)
        {
            //Mapper.Initialize(options => options.AddProfile<InstagraphProfile>());

 //           Console.WriteLine(ResetDatabase());

            Console.WriteLine(ImportData());

            //ExportData();
        }

        private static string ImportData()
        {
            StringBuilder sb = new StringBuilder();

            using (var context = new PlanetHuntersContext())
           {
            //    string astronomersJson = File.ReadAllText("files/input/astronomers.json");

            //    sb.AppendLine(Deserializer.ImportAstronomers(context, astronomersJson));

            //    string telescopesJson = File.ReadAllText("files/input/telescopes.json");

            //    sb.AppendLine(Deserializer.ImportTelescopes(context, telescopesJson));

                string planetsJson = File.ReadAllText("files/input/planets.json");

                sb.AppendLine(Deserializer.ImportPlanets(context, planetsJson));

                //string starsXml = File.ReadAllText("files/input/stars.xml");

                //sb.AppendLine(Deserializer.ImportStars(context, starsXml));

                //string discoveriesXml = File.ReadAllText("files/input/discoveries.xml");

                //sb.AppendLine(Deserializer.ImportDiscoveries(context, discoveriesXml));
            }

            string result = sb.ToString().Trim();
            return result;
        }

        private static void ExportData()
        {
            using (var context = new PlanetHuntersContext())
            {
                string planets = Serializer.ExportPlanets(context);

                File.WriteAllText("files/output/planets-by-TRAPPIST.json.json", planets);

                string astronomers = Serializer.ExportAstronomers(context);

                File.WriteAllText("files/output/astronomers-of-Alpha Centauri.json", astronomers);

                string stars = Serializer.ExportStars(context);

                File.WriteAllText("files/output/stars.xml", stars);
            }
        }

        private static string ResetDatabase()
        {
            using (var context = new PlanetHuntersContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return $"Database reset succsessfully.";
        }
    }
}
