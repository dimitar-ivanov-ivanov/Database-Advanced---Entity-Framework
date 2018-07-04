namespace Instagraph.Data.Stores
{
    using System;
    using System.Collections.Generic;
    using Instagraph.Data.Dtos.Import;
    using System.Linq;
    using Instagraph.Models;
    using Instagraph.Data.Dtos.Export;

    public class UserStore
    {
        public static void AddUsers(ICollection<UserDto> userDtos)
        {
            using (var context = new InstagraphContext())
            {
                foreach (var userDto in userDtos)
                {
                    var picture = PictureStore.GetPictureByPath(userDto.Profile_Picture, context);
                    var existingUser = UserStore.GetUserByName(userDto.Username, context);

                    if (userDto.Password == null ||
                       picture == null ||
                       existingUser != null ||
                       userDto.Username == null ||
                       userDto.Profile_Picture == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var user = new User()
                    {
                        Username = userDto.Username,
                        Password = userDto.Password,
                        Picture = picture,
                        PictureId = picture.Id,
                    };

                    context.Users.Add(user);
                    context.SaveChanges();
                    Console.WriteLine($"Successfully imported User {userDto.Username}.");
                }
            }
        }

        public static void AddUserFollowers(ICollection<UserFollowerDto> userFollowerDtos)
        {
            using (var context = new InstagraphContext())
            {
                foreach (var userFollowerDto in userFollowerDtos)
                {
                    var existingUser = UserStore.GetUserByName(userFollowerDto.User, context);
                    var existingFollower = UserStore.GetUserByName(userFollowerDto.Follower, context);

                    var userFollowerExist = UserStore.GetUserFollower(userFollowerDto.User, userFollowerDto.Follower, context);
                    var userFollowingExists = UserStore.GetUserFollowing(userFollowerDto.User, userFollowerDto.Follower, context);

                    if (userFollowerDto.User == null ||
                       userFollowerDto.Follower == null ||
                       existingFollower == null ||
                       existingUser == null ||
                       userFollowerExist != null ||
                       userFollowingExists != null ||
                       userFollowerDto.Follower == userFollowerDto.User)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var userFollower = new UserFollower()
                    {
                        User = existingUser,
                        UserId = existingUser.Id,
                        Follower = existingFollower,
                        FollowerId = existingFollower.Id
                    };

                    var userFollowing = new UserFollowing()
                    {
                        User = existingFollower,
                        UserId = existingFollower.Id,
                        Following = existingUser,
                        FollowingId = existingUser.Id
                    };

                    context.UserFollowers.Add(userFollower);
                    context.UserFollowings.Add(userFollowing);
                    //existingUser.Followers.Add(userFollower);
                    //existingFollower.Following.Add(userFollowing);

                    context.SaveChanges();

                    Console.WriteLine($"Successfully imported Follower {userFollowerDto.Follower} to User {userFollowerDto.User}.”");
                }
            }
        }

        public static User GetUserByName(string username, InstagraphContext context)
        {
            return context.Users.FirstOrDefault(u => u.Username == username);
        }

        public static UserFollower GetUserFollower(string username, String follower, InstagraphContext context)
        {
            return context.UserFollowers.FirstOrDefault(u => u.User.Username
            == username && u.Follower.Username == follower);
        }

        public static UserFollowing GetUserFollowing(string username, String following, InstagraphContext context)
        {
            return context.UserFollowings.FirstOrDefault(u => u.User.Username
            == following && u.Following.Username == username);
        }

        public static ICollection<PopularUserDto> GetPopularUsers()
        {
            using (var context = new InstagraphContext())
            {
                return context.Users
                    .Select(u => new PopularUserDto()
                    {
                        user = u.Username,
                        followers = u.Followers.Count()
                    })
                    .OrderByDescending(u => u.followers)
                    .ToList();
            }
        }
    }
}
