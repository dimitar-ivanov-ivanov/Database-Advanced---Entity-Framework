namespace TeamBuilder.App.Core
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Core.Commands;

    public class CommandDispatcher
    {
        public string Dispatch(string input)
        {
            var result = string.Empty;
            var args = input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            var command = args.Length > 0 ? args[0] : string.Empty;
            args = args.Skip(1).ToArray();

            switch (command)
            {
                case "Exit":
                    var exit = new ExitCommand();
                    result = exit.Execute(args);
                    break;
                case "RegisterUser":
                    var registerUserCommand = new RegisterUserCommand();
                    result = registerUserCommand.Execute(args);
                    break;
                case "Login":
                    var login = new LoginCommand();
                    result = login.Execute(args);
                    break;
                case "Logout":
                    var logout = new LogoutCommand();
                    result = logout.Execute(args);
                    break;
                case "DeleteUser":
                    var delete = new DeleteUserCommand();
                    result = delete.Execute(args);
                    break;
                case "CreateEvent":
                    var createEvent = new CreateEventCommand();
                    result = createEvent.Execute(args);
                    break;
                case "CreateTeam":
                    var createTeam = new CreateTeamCommand();
                    result = createTeam.Execute(args);
                    break;
                case "InviteToTeam":
                    var inviteToTeam = new InviteToTeamCommand();
                    result = inviteToTeam.Execute(args);
                    break;
                case "AcceptInvite":
                    var acceptInvite = new AcceptInviteCommand();
                    result = acceptInvite.Execute(args);
                    break;
                case "DeclineInvite":
                    var declineInvite = new DeclineInviteCommand();
                    result = declineInvite.Execute(args);
                    break;
                case "KickMember":
                    var kickMember = new KickMemberCommand();
                    result = kickMember.Execute(args);
                    break;
                case "Disband":
                    var disband = new DisbandCommand();
                    result = disband.Execute(args);
                    break;
                case "AddTeamTo":
                    var addTeamTo = new AddTeamToCommand();
                    result = addTeamTo.Execute(args);
                    break;
                case "ShowEvent":
                    var showEvent = new ShowEventCommand();
                    result = showEvent.Execute(args);
                    break;
                case "ShowTeam":
                    var showTeam = new ShowTeamCommand();
                    result = showTeam.Execute(args);
                    break;
                default:
                    throw new NotSupportedException($"Command {command} not supported!");
            }

            return result;
        }
    }
}
