namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    //•	RegisterUser <username> <password> <repeat-password> <firstName> <lastName> <age> <gender>
    public class RegisterUserCommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(7, args);
            var username = args[0];
            var password = args[1];
            var repeatedPassword = args[2];

            var firstName = args[3];
            var lastName = args[4];
            var age = 0;

            var isNumber = int.TryParse(args[5], out age);
            Gender gender;
            var isGenderValid = Enum.TryParse(args[6], out gender);

            if (username.Length < Constants.MinUsernameLength ||
               username.Length > Constants.MaxUsernameLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UsernameNotValid, username));
            }

            if (!password.Any(char.IsDigit) || !password.Any(char.IsUpper))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.PasswordNotValid, password));
            }

            if (password != repeatedPassword)
            {
                throw new ArgumentException(Constants.ErrorMessages.PasswordDoesNotMatch);
            }

            if (!isNumber || age <= 0)
            {
                throw new ArgumentException(Constants.ErrorMessages.AgeNotValid);
            }

            if (!isGenderValid)
            {
                throw new ArgumentException(Constants.ErrorMessages.GenderNotValid);
            }

            if (CommandHelper.IsUserExisting(username))
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.UsernameIsTaken, username));
            }

            this.RegisterUser(username, password, firstName, lastName, age, gender);
            return $"User {username} was registered successfully!";
        }

        private void RegisterUser(string username, string password, string firstName, string lastName, int age, Gender gender)
        {
            using (var context = new TeamBuilderContext())
            {
                var u = new User()
                {
                    Username = username,
                    Password = password,
                    FirstName = firstName,
                    LastName = lastName,
                    Gender = gender,
                    Age = age,
                };
                context.Users.Add(u);
                context.SaveChanges();
            }
        }
    }
}
