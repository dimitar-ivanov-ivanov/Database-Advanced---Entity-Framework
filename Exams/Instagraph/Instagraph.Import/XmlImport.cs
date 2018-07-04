namespace Instagraph.Import
{
    using Instagraph.Data.Stores;
    using System.Xml.Linq;
    using System.Linq;
    using Instagraph.Data.Dtos.Import;

    public class XmlImport
    {
        public static void ImportPosts()
        {
            var posts = XDocument.Load("../files/xml/input/posts.xml")
                .Root.Elements()
                .Select(p => new PostDto()
                {
                    Caption = p.Element("caption")?.Value,
                    Picture = p.Element("picture")?.Value,
                    User = p.Element("user")?.Value
                })
                .ToList();

            PostStore.AddPosts(posts);
        }

        public static void ImportComments()
        {
            var comments = XDocument.Load("../files/xml/input/comments.xml")
                .Root.Elements()
                .Select(p => new CommentDto()
                {
                    Content = p.Element("content")?.Value,
                    PostId = p.Element("post")?.Attribute("id")?.Value,
                    User = p.Element("user")?.Value
                })
                .ToList();

            CommentStore.AddComment(comments);
        }
    }
}
