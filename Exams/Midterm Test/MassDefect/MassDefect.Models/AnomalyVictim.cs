namespace MassDefect.Models
{
    public class AnomalyVictim
    {
        public int AnomalyId { get; set; }
        public Anomaly Anomaly { get; set; }

        public int VictimId { get; set; }
        public Person Victim { get; set; }
    }
}
