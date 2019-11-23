namespace Server.Models
{
    public enum projectTypes { APLokalisatie, Pozyx }
    public class Tag
    {
        public long Id { get; set; }
        public projectTypes ProjecType { get; set; }
        public string Mac { get; set; }
        public string Description { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int ZPos { get; set; }
    }
}
