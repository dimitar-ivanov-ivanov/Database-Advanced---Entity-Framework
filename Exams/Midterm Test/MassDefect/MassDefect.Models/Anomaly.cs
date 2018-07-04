namespace MassDefect.Models
{
    using System.Collections.Generic;

    public class Anomaly
    {
        public Anomaly()
        {
            this.AnomalyVictims = new HashSet<AnomalyVictim>();
        }

        public int Id { get; set; }

        public int HomePlanetId { get; set; }
        public Planet HomePlanet { get; set; }

        public int TeleportPlanetId { get; set; }
        public Planet TeleportPlanet { get; set; }

        public ICollection<AnomalyVictim> AnomalyVictims { get; set; }
    }
}
