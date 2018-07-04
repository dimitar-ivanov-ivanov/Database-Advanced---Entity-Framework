using PhotoShare.Client.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoShare.Client.Core
{
    public class CommandDispatcher
    {
        private HashSet<string> LoggedUsers;

        public CommandDispatcher()
        {
            LoggedUsers = new HashSet<string>();
        }

        public string DispatchCommand(string[] commandParameters)
        {
            var command = commandParameters[0];
            commandParameters = commandParameters.Skip(1).ToArray();

            switch (command)
            {
                case "RegisterUser":
                case "ListFriends":
                case "Login":
                    var username = commandParameters[0];
                    if (LoggedUsers.Contains(username))
                    {
                        throw new InvalidOperationException("Invalid credentials!");
                    }
                    break;
                case "AddTown":
                case "ModifyUser":
                case "DeleteUser":
                case "AddTag":
                case "CreateAlbum":
                case "AddTagTo":
                case "AddFriend":
                case "AcceptFriend":
                case "ShareAlbum":
                case "UploadPicture":
                case "Logout":
                     username = commandParameters[0];
                    if (!LoggedUsers.Contains(username))
                    {
                        throw new InvalidOperationException("Invalid credentials!");
                    }
                    break;
                default:
                    break;
            }

            switch (command)
            {
                case "RegisterUser":
                    return new RegisterUserCommand().Execute(commandParameters);
                case "AddTown":
                    return new AddTownCommand().Execute(commandParameters);
                case "ModifyUser":
                    return new ModifyUserCommand().Execute(commandParameters);
                case "DeleteUser":
                    return new DeleteUser().Execute(commandParameters);
                case "AddTag":
                    return new AddTagCommand().Execute(commandParameters);
                case "CreateAlbum":
                    return new CreateAlbumCommand().Execute(commandParameters);
                case "AddTagTo":
                    return new AddTagToCommand().Execute(commandParameters);
                case "AddFriend":
                    return new AddFriendCommand().Execute(commandParameters);
                case "AcceptFriend":
                    return new AcceptFriendCommand().Execute(commandParameters);
                case "ListFriends":
                    return new PrintFriendsListCommand().Execute(commandParameters);
                case "ShareAlbum":
                    return new ShareAlbumCommand().Execute(commandParameters);
                case "UploadPicture":
                    return new UploadPictureCommand().Execute(commandParameters);
                case "Login":
                    break;
                case "Logout":
                    break;
                default:
                    return new ExitCommand().Execute();
            }

            return string.Empty;
        }
    }
}
