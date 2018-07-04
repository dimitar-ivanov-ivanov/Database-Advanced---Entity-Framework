namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CreateAlbumCommand
    {
        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(string[] input)
        {
            var user = input[0];
            var albumTitle = input[1];
            var bgColor = input[2];
            var tags = input.Skip(3).ToArray();

            using (var context = new PhotoShareContext())
            {
                if (context.Users.FirstOrDefault(ps => ps.Username == user) == null)
                {
                    throw new ArgumentException($"User {user} not found!");
                }

                if (context.Albums.FirstOrDefault(a => a.Name == albumTitle) != null)
                {
                    throw new ArgumentException($"Album {albumTitle} exists!");
                }

                if (!Enum.IsDefined(typeof(Color), bgColor))
                {
                    throw new ArgumentException($"Color {bgColor} not found!");
                }

                if (context.Tags.Any(t => tags.Contains("#" + t.Name)))
                {
                    throw new ArgumentException("Invalid tags!");
                }

                var albumTags = new List<AlbumTag>();
                var albumRoles = new List<AlbumTag>();

                var album = new Album()
                {
                    Name = albumTitle,
                    BackgroundColor = (Color)Enum.Parse(typeof(Color), bgColor),
                };
                
                for (int i = 0; i < tags.Length; i++)
                {
                    var tag = context.Tags.FirstOrDefault(t => t.Name == "#" + tags[i]);
                    if (tag != null)
                    {
                        albumTags.Add(new AlbumTag()
                        {
                            Album = album,
                            Tag = tag
                        });
                    }
                }

                if(albumTags.Count == 0)
                {
                    throw new ArgumentException("Invalid tags!");
                }

                var contextUser = context.Users.FirstOrDefault(u => u.Username == user);
                var albumRole = new AlbumRole()
                {
                    Album = album,
                    User = contextUser,
                    Role = Role.Viewer
                };

                context.Albums.Add(album);
                context.AlbumRoles.Add(albumRole);
                album.AlbumRoles.Add(albumRole);
                album.AlbumTags = albumTags;
                context.SaveChanges();
            }

            return $"Album {albumTitle} successfully created!";
        }
    }
}
