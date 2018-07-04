﻿namespace P01_HospitalDatabase.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Visitations")]
    public class Visitation
    {
        [Key,Column("VisitationID")]
        public int VisitationId { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(250)]
        public string Comments { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        public Patient Patient { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; }
    }
}