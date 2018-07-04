namespace Instagraph.Export
{
    using Instagraph.Data.Stores;
    using Newtonsoft.Json;
    using System.IO;

    public class JsonExport
    {
        public static void ExportUncommentedPosts()
        {
            var posts = PostStore.GetUcommentedPosts();
            var json = JsonConvert.SerializeObject(posts, Formatting.Indented);
            File.WriteAllText("../files/json/output/uncommented-posts.json",json);
        }

        public static void ExportPopularUsers()
        {
            var posts = UserStore.GetPopularUsers();
            var json = JsonConvert.SerializeObject(posts, Formatting.Indented);
            File.WriteAllText("../files/json/output/popular-users.json", json);
        }
    }
}
