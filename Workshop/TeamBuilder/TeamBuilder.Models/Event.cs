namespace TeamBuilder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Event
    {
        public Event()
        {
            this.EventTeams = new HashSet<EventTeam>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(25),Required]
        public string Name { get; set; }
        
        [MaxLength(250)]
        public string Description { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/MM/yyyy HH:mm}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/MM/yyyy HH:mm}")]
        public DateTime EndDate { get; set; }

        public int? CreatorId { get; set; }
        public User Creator { get; set; }

        public ICollection<EventTeam> EventTeams { get; set; }

    }
}
