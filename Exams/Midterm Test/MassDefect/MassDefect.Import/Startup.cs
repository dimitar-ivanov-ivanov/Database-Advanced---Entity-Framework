namespace MassDefect.Import
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            //JsonImport.ImportSolarSystems();
            //JsonImport.ImportStars();
            //JsonImport.ImportPlanets();
            //JsonImport.ImportPersons();
            //JsonImport.ImportAnomalies();
            //JsonImport.ImportAnomalyVictims();
            XmlImport.ImportNewAnomalies();
        }
    }
}
