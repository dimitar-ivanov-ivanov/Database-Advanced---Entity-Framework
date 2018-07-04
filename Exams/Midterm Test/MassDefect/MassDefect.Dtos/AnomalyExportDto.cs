namespace MassDefect.Dtos
{
    public class AnomalyExportDto
    {
        public int id { get; set; }
        public PlanetExportDto originPlanet { get; set; }
        public PlanetExportDto teleportPlanet { get; set; }
        public int victimsCount { get; set; }
    }
}
