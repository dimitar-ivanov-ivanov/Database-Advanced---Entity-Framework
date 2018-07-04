using PhotoShare.Data;
using PhotoShare.Models;
using System;
using System.Linq;

namespace PhotoShare.Client.Core.Commands
{
    public class UploadPictureCommand
    {
        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public string Execute(string[] data)
        {
            using (var context = new PhotoShareContext())
            {
                var albumName = data[0];
                var pictureTitle = data[1];
                var pictureFilePath = data[2];

                var album = context.Albums.FirstOrDefault(a => a.Name == albumName);

                if (album == null)
                {
                    throw new ArgumentException($"Album {albumName} not found!");
                }

                var picture = new Picture()
                {
                    Album = album,
                    Title = pictureTitle,
                    Path = pictureFilePath
                };

                album.Pictures.Add(picture);
                context.Pictures.Add(picture);
                context.SaveChanges();
                return $"Picture {pictureTitle} added to {pictureFilePath}!";
            }
        }
    }
}
