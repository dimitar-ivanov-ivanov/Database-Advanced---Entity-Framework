namespace PlanetHunters.DataProcessor.Dtos.Import
{
    using System.ComponentModel.DataAnnotations;

    public class PlanetDto
    {
        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public string Mass { get; set; }

        [Required]
        public string StarSystem { get; set; }
    }
}
