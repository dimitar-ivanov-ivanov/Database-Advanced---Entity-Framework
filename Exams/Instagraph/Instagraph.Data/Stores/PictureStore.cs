namespace Instagraph.Data.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Instagraph.Data.Dtos.Import;
    using Instagraph.Models;

    public class PictureStore
    {
        public static void AddPictures(ICollection<PictureDto> pictureDtos)
        {
            using (var context = new InstagraphContext())
            {
                foreach (var pictureDto in pictureDtos)
                {
                    if (pictureDto.Path == null || pictureDto.Size == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var picture = new Picture()
                    {
                        Size = (decimal)pictureDto.Size,
                        Path = pictureDto.Path,
                    };

                    context.Pictures.Add(picture);
                    Console.WriteLine($"Successfully imported Picture {picture.Path}.");
                }

                context.SaveChanges();
            }
        }

        public static Picture GetPictureByPath(string path,InstagraphContext context)
        {
            return context.Pictures.FirstOrDefault(p=>p.Path == path);
        }

        public static Picture GetPictureById(int id, InstagraphContext context)
        {
            return context.Pictures.FirstOrDefault(p => p.Id == id);
        }
    }
}