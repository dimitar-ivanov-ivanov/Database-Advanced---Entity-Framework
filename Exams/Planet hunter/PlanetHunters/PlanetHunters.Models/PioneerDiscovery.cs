namespace PlanetHunters.Models
{
    public class PioneerDiscovery
    {
        public Discovery Discovery { get; set; }
        public int DiscoveryId { get; set; }

        public Astronomer Pioneer { get; set; }
        public int PioneerId { get; set; }
    }
}
