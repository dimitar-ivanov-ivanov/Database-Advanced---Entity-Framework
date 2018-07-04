namespace P01_HospitalDatabase.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PatientMedicament")]
    public class PatientMedicament
    {
        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        public Patient Patient { get; set; }

        [ForeignKey("Medicament")]
        public int MedicamentId { get; set; }

        public Medicament Medicament { get; set; }
    }
}
