namespace MassDefect.Models
{
    using System.Collections.Generic;

    public class Star
    {
        public Star()
        {
            this.Planets = new HashSet<Planet>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int SolarSystemId { get; set; }
        public SolarSystem SolarSystem { get; set; }

        public ICollection<Planet> Planets { get; set; }
    }
}
