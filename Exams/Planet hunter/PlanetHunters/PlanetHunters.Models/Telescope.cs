namespace PlanetHunters.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Telescope
    {
        public Telescope()
        {
            this.Discoveries = new HashSet<Discovery>();
        }

        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required, MaxLength(255)]
        public string Location { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335M")]
        public decimal MirrorDiameter { get; set; }

        public ICollection<Discovery> Discoveries { get; set; }
    }
}
