namespace P01_HospitalDatabase.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Diagnoses")]
    public class Diagnose
    {
        [Key,Column("DiagnoseID")]
        public int DiagnoseId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Comments { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        public Patient Patient { get; set; }
    }
}
