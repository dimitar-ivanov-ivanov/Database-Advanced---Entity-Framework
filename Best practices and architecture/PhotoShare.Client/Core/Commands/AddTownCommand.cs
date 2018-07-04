namespace PhotoShare.Client.Core.Commands
{
    using System.Linq;
    using System;
    using PhotoShare.Data;
    using PhotoShare.Models;

    public class AddTownCommand
    {
        // AddTown <townName> <countryName>
        public string Execute(string[] data)
        {
            string townName = data[0];
            string country = data[1];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                Town town = new Town
                {
                    Name = townName,
                    Country = country
                };

                if(context.Towns.FirstOrDefault(t=>t.Name == town.Name) != null)
                {
                    throw new ArgumentException($"Town {town.Name} was already added!");
                }

                context.Towns.Add(town);
                context.SaveChanges();

                return townName + " was added to database!";
            }
        }
    }
}
