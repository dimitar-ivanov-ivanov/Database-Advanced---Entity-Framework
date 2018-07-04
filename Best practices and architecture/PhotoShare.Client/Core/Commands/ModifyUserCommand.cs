namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class ModifyUserCommand
    {
        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public string Execute(string[] data)
        {
            var username = data[0];
            var property = data[1];
            var newVal = data[2];

            using (var context = new PhotoShareContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                switch (property)
                {
                    case "Password":
                        ModifyPassword(user,newVal,context);
                        break;
                    case "BornTown":
                        ModifyBornTown(user, newVal, context);
                        break;
                    case "CurrentTown":
                        ModifyCurrentTown(user, newVal, context);
                        break;
                    default:
                        throw new ArgumentException($"Property {property} not supported!");
                }

                context.SaveChanges();
                return $"User {username} {property} is {newVal}.";
            }
        }

        private void ModifyPassword(User user, string newVal,PhotoShareContext context)
        {
            //[Password(6, 50, ContainsDigit = true, ContainsLowercase = true, ErrorMessage = "Invalid password")]

            if (newVal.Length < 6 || newVal.Length > 50 ||
                newVal.Where(v=>v >='a' && v <='z').Count() == 0 ||
                newVal.Where(v => v >= '0' && v <= '9').Count() == 0)
            {
                throw new ArgumentException($"Invalid Password");
            }
            user.Password = newVal;
        }

        private void ModifyBornTown(User user,string newVal,PhotoShareContext context)
        {
            var town = context.Towns.FirstOrDefault(t => t.Name == newVal);
            if(town == null)
            {
                throw new ArgumentException($"Town {newVal} not found!");
            }

            user.BornTown = town;
        }

        private void ModifyCurrentTown(User user, string newVal, PhotoShareContext context)
        {
            var town = context.Towns.FirstOrDefault(t => t.Name == newVal);
            if (town == null)
            {
                throw new ArgumentException($"Town {newVal} not found!");
            }

            user.CurrentTown = town;
        }
    }
}
