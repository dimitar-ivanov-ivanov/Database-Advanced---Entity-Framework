namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AddTagToCommand 
    {
        // AddTagTo <albumName> <tag>
        public string Execute(string[] input)
        {
            var albumName = input[0];
            var tagName = input[1];

            using(var context = new PhotoShareContext())
            {
                var album = context.Albums.FirstOrDefault(a => a.Name == albumName);
                var tag = context.Tags.FirstOrDefault(t => t.Name == tagName);

                if (album == null || tag == null)
                {
                    throw new ArgumentException("Either tag or album do not exist!");
                }

                album.AlbumTags.Add(new AlbumTag()
                {
                    Album = album,
                    Tag = tag
                });

                return $"Tag {tag} added to {album}!";
            }

        }
    }
}
