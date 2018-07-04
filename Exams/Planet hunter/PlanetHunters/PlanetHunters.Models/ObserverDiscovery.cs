namespace PlanetHunters.Models
{
    public class ObserverDiscovery
    {
        public Discovery Discovery { get; set; }
        public int DiscoveryId { get; set; }

        public Astronomer Observer { get; set; }
        public int ObserverId { get; set; }
    }
}
