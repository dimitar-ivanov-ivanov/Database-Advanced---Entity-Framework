namespace Instagraph.Models
{
    public class UserFollowing
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int FollowingId { get; set; }
        public User Following { get; set; }
    }
}
