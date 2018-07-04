namespace Instagraph.Import
{
    using System.Collections.Generic;
    using Instagraph.Data.Stores;
    using Instagraph.Data.Dtos.Import;
    using Newtonsoft.Json;
    using System.IO;

    public class JsonImport
    {
        public static void ImportPictures()
        {
            var json = File.ReadAllText("../files/json/input/pictures.json");
            var pictureDtos = JsonConvert.DeserializeObject<ICollection<PictureDto>>(json);
            PictureStore.AddPictures(pictureDtos);
        }

        public static void ImportUsers()
        {
            var json = File.ReadAllText("../files/json/input/users.json");
            var userDtos = JsonConvert.DeserializeObject<ICollection<UserDto>>(json);
            UserStore.AddUsers(userDtos);
        }

        public static void ImportUserFollowers()
        {
            var json = File.ReadAllText("../files/json/input/users_followers.json");
            var userFollowerDtos = JsonConvert.DeserializeObject<ICollection<UserFollowerDto>>(json);
            UserStore.AddUserFollowers(userFollowerDtos);
        }
    }
}