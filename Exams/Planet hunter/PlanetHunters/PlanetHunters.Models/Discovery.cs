namespace PlanetHunters.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Discovery
    {
        public Discovery()
        {
            this.Stars = new HashSet<Star>();
            this.Planets = new HashSet<Planet>();
            this.Pioneers = new HashSet<PioneerDiscovery>();
            this.Observers = new HashSet<ObserverDiscovery>();
        }

        public int Id { get; set; }

        [Required]
        public DateTime DateMade { get; set; }

        public int TelescopeId { get; set; }
        [Required]
        public Telescope Telescope { get; set; }

        public ICollection<Star> Stars { get; set; }
        public ICollection<Planet> Planets { get; set; }

        public ICollection<PioneerDiscovery> Pioneers { get; set; }
        public ICollection<ObserverDiscovery> Observers { get; set; }
    }
}
