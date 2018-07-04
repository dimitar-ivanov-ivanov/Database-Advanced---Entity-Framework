using PhotoShare.Data;
using PhotoShare.Models;
using System;
using System.Linq;

namespace PhotoShare.Client.Core.Commands
{
    public class ShareAlbumCommand
    {
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer

        public string Execute(string[] input)
        {
            using (var context = new PhotoShareContext())
            {
                var albumId = int.Parse(input[0]);
                var username = input[1];
                var permission = input[2];

                Role role;
                bool isValidRole = Enum.TryParse(permission, out role);

                var album = context.Albums.FirstOrDefault(a => a.Id == albumId);
                var user = context.Users.FirstOrDefault(u => u.Username == username);

                if (album == null)
                {
                    throw new ArgumentException($"Album {albumId} not found!");
                }

                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                if (!isValidRole)
                {
                    throw new ArgumentException($"Permission must be either “Owner” or “Viewer”!");
                }

                var albumRole = new AlbumRole()
                {
                    Album = album,
                    Role = role,
                    User = user
                };

                album.AlbumRoles.Add(albumRole);
                context.AlbumRoles.Add(albumRole);
                context.SaveChanges();

                return $"Username {username} added to album {album.Name} ({role})";
            }
        }
    }
}
