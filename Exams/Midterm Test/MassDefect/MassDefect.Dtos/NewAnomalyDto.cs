namespace MassDefect.Dtos
{
    using System.Collections.Generic;

    public class NewAnomalyDto
    {
        public string OriginPlanet { get; set; }
        public string TeleportPlanet { get; set; }
        public ICollection<VictimDto> Victims { get; set; }
    }
}
