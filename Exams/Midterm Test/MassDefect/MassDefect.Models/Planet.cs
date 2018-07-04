namespace MassDefect.Models
{
    using System.Collections.Generic;

    public class Planet
    {
        public Planet()
        {
            this.Persons = new HashSet<Person>();
            this.OriginAnomalies = new HashSet<Anomaly>();
            this.TeleportAnomalies = new HashSet<Anomaly>();
        }

        public int Id { get; set; }
        
        public string Name { get; set; }

        public int SunId { get; set; }
        public Star Sun { get; set; }

        public int SolarSystemId { get; set; }
        public SolarSystem SolarSystem { get; set; }

        public ICollection<Person> Persons { get; set; }

        public ICollection<Anomaly> OriginAnomalies { get; set; }
        public ICollection<Anomaly> TeleportAnomalies { get; set; }

    }
}
