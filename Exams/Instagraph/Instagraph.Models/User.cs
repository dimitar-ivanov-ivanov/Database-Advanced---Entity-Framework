namespace Instagraph.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User()
        {
            this.Followers = new HashSet<UserFollower>();
            this.Following = new HashSet<UserFollowing>();
            this.Posts = new HashSet<Post>();
            this.Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public int PictureId { get; set; }
        public Picture Picture { get; set; }

        public ICollection<UserFollower> Followers { get; set; }
        public ICollection<UserFollowing> Following { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
