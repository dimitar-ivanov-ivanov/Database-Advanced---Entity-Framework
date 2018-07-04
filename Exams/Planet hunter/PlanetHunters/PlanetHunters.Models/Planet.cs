namespace PlanetHunters.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Planet
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required, Range(typeof(decimal), "0", "79228162514264337593543950335M")]
        public decimal Mass { get; set; }

        public int StarSystemId { get; set; }
        [Required]
        public StarSystem StarSystem { get; set; }

        public int? DiscoveryId { get; set; }
        public Discovery Discovery { get; set; }
    }
}
