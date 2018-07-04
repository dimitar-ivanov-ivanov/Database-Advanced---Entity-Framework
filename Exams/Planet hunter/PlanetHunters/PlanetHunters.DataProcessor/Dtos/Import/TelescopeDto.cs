namespace PlanetHunters.DataProcessor.Dtos.Import
{
    using System.ComponentModel.DataAnnotations;

    public class TelescopeDto
    {
        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required, MaxLength(255)]
        public string Location { get; set; }

        [Required]
        public string MirrorDiameter { get; set; }
    }
}
