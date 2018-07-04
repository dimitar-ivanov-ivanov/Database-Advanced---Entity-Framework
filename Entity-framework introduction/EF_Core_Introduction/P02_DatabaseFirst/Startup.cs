namespace P02_DatabaseFirst
{
    using P02_DatabaseFirst.Data;
    using System;
    using System.Linq;

    public class Startup
    {
        public static void Main(string[] args)
        {
            using (var context = new SoftUniContext())
            {
                var departments = context.Departments
                     .Where(d => d.Employees.Count > 5)
                     .OrderBy(d=>d.Employees.Count)
                     .ThenBy(d=>d.Name);

                foreach (var d in departments)
                {
                    var manager = context.Employees.Find(d.ManagerId);
                    Console.WriteLine($"{d.Name} - {manager.FirstName} {manager.LastName}");
                    foreach (var e in d.Employees
                        .OrderBy(e=>e.FirstName)
                        .ThenBy(e=>e.LastName))
                    {
                        Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                    }
                    Console.WriteLine("----------");
                }    
            }
        }
    }
}
