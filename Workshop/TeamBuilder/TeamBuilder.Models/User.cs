namespace TeamBuilder.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User()
        {
            this.Events = new HashSet<Event>();
            this.Invitations = new HashSet<Invitation>();
            this.UserTeams = new HashSet<UserTeam>();
            this.CreatedTeams = new HashSet<Team>();

            this.IsDeleted = false;
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(25),MinLength(3),Required]
        public string Username { get; set; }

        [MaxLength(25)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        [MinLength(6),MaxLength(30),Required]
        public string Password { get; set; }

        public Gender Gender { get; set; }

        public int? Age { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Event> Events { get; set; }
        public ICollection<Invitation> Invitations { get; set; }
        public ICollection<UserTeam> UserTeams { get; set; }
        public ICollection<Team> CreatedTeams { get; set; }

    }
}
