namespace PlanetHunters.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Star
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public int Temperature { get; set; }

        public int StarSystemId { get; set; }
        [Required]
        public StarSystem StarSystem { get; set; }

        public int? DiscoveryId { get; set; }
        public Discovery Discovery { get; set; }
    }
}
