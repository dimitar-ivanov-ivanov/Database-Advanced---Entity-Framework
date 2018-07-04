namespace TeamBuilder.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Team
    {
        public Team()
        {
            this.EventTeams = new HashSet<EventTeam>();
            this.Invitations = new HashSet<Invitation>();
            this.UserTeams = new HashSet<UserTeam>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(25),Required]
        public string Name { get; set; }

        [MaxLength(32)]
        public string Description { get; set; }

        [StringLength(3),Required]
        public string Acronym { get; set; }

        public int CreatorId { get; set; }
        public User Creator { get; set; }

        public ICollection<EventTeam> EventTeams { get; set; }
        public ICollection<Invitation> Invitations { get; set; }
        public ICollection<UserTeam> UserTeams { get; set; }

    }
}
