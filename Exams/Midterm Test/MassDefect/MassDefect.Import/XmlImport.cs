namespace MassDefect.Import
{
    using MassDefect.Dtos;
    using MassDefect.Stores;
    using System.Linq;
    using System.Xml.Linq;

    public static class XmlImport
    {
        public static void ImportNewAnomalies()
        {
            var xml = XDocument.Load("../imports/new-anomalies.xml");
            var anomalies = xml.Root.Elements()
                .Select(a => new NewAnomalyDto()
                {
                    OriginPlanet = a.Attribute("origin-planet")?.Value,
                    TeleportPlanet = a.Attribute("teleport-planet")?.Value,
                    Victims = a.Element("victims")?.Elements()
                    .Select(v => new VictimDto()
                    {
                        Name = v.Attribute("name")?.Value
                    })
                    .ToList()
                })
                .ToList();

            AnomalyStore.AddNewAnomalies(anomalies);
        }
    }
}
