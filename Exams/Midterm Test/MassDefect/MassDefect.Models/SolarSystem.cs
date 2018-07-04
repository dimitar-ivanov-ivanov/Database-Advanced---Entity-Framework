namespace MassDefect.Models
{
    using System.Collections.Generic;

    public class SolarSystem
    {
        public SolarSystem()
        {
            this.Stars = new HashSet<Star>();
            this.Planets = new HashSet<Planet>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public ICollection<Star> Stars { get; set; }
        public ICollection<Planet> Planets { get; set; }

    }
}
