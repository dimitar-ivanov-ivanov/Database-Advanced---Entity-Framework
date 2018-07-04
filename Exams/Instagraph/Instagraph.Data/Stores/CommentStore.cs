namespace Instagraph.Data.Stores
{
    using System;
    using System.Collections.Generic;
    using Instagraph.Data.Dtos.Import;
    using Instagraph.Models;

    public class CommentStore
    {
        public static void AddComment(ICollection<CommentDto> commentDtos)
        {
            using (var context = new InstagraphContext())
            {
                foreach (var commentDto in commentDtos)
                {
                    if (commentDto.Content == null ||
                       commentDto.PostId == null ||
                       commentDto.User == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var user = UserStore.GetUserByName(commentDto.User, context);
                    var post = PostStore.GetPostById(int.Parse(commentDto.PostId), context);

                    if (user == null || post == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var comment = new Comment()
                    {
                        Post = post,
                        PostId = post.Id,
                        User = user,
                        UserId = user.Id,
                        Content = commentDto.Content
                    };

                    context.Comments.Add(comment);
                    context.SaveChanges();
                    user.Comments.Add(comment);
                    post.Comments.Add(comment);

                    Console.WriteLine($"Successfully imported Comment {commentDto.Content}");
                }
            }
        }
    }
}