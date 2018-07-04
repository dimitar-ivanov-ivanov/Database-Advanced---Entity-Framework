namespace PlanetHunters.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Astronomer
    {
        public Astronomer()
        {
            this.PioneeringDiscoveries = new HashSet<PioneerDiscovery>();
            this.ObserverDiscoveries = new HashSet<ObserverDiscovery>();
        }

        public int Id { get; set; }
        [Required,MaxLength(50)]
        public string FirstName { get; set; }

        [Required,MaxLength(50)]
        public string LastName { get; set; }

        public ICollection<PioneerDiscovery> PioneeringDiscoveries { get; set; }
        public ICollection<ObserverDiscovery> ObserverDiscoveries { get; set; }
    }
}
