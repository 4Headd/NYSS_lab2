namespace lab_2
{
    public class ShortData
    {
        public string ThreatId { get; set; }

        public string ThreatName { get; set; }

        public ShortData(string id, string threatName)
        {
            ThreatId = id;
            ThreatName = threatName;
        }
    }
}
