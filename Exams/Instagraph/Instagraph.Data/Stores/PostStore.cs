namespace Instagraph.Data.Stores
{
    using System;
    using System.Collections.Generic;
    using Instagraph.Data.Dtos.Import;
    using Instagraph.Data.Dtos.Export;
    using System.Linq;
    using Instagraph.Models;

    public class PostStore
    {
        public static void AddPosts(ICollection<PostDto> postDtos)
        {
            using (var context = new InstagraphContext())
            {
                foreach (var postDto in postDtos)
                {
                    var user = UserStore.GetUserByName(postDto.User, context);
                    var picture = PictureStore.GetPictureByPath(postDto.Picture, context);

                    if (postDto.Caption == null ||
                        postDto.User == null ||
                        postDto.Picture == null ||
                        user == null ||
                        picture == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var post = new Post()
                    {
                        Picture = picture,
                        PictureId = picture.Id,
                        User = user,
                        UserId = user.Id,
                        Caption = postDto.Caption,
                    };

                    Console.WriteLine($"Successfully imported Post {post.Caption}");
                    context.Posts.Add(post);
                    context.SaveChanges();
                    user.Posts.Add(post);
                    picture.Posts.Add(post);
                }
            }
        }

        public static Post GetPostById(int id, InstagraphContext context)
        {
            return context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public static ICollection<UcommentedPostDto> GetUcommentedPosts()
        {
            using (var context = new InstagraphContext())
            {
                return context.Posts
                    .Where(p => p.Comments.Count == 0)
                    .Select(p => new UcommentedPostDto()
                    {
                        id = p.Id,
                        picture = p.Picture.Path,
                        user = p.User.Username
                    })
                    .ToList();
             }
        }
    }
}