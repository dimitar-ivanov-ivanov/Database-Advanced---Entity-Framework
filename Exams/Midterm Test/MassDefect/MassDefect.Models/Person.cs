namespace MassDefect.Models
{
    using System.Collections.Generic;

    public class Person
    {
        public Person()
        {
            this.AnomalyVictims = new HashSet<AnomalyVictim>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int HomePlanetId { get; set; }
        public Planet HomePlanet { get; set; }

        public ICollection<AnomalyVictim> AnomalyVictims { get; set; }
    }
}
