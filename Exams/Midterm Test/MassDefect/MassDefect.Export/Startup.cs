namespace MassDefect.Export
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            //JsonExport.ExportPlanetsWhichAreNotAnomalyOrigins();
            //JsonExport.PeopleWhichHaveNotBeenVictimsOfAnomalies();
            JsonExport.AnomalyWhichAffectedTheMostPeople();
        }
    }
}
