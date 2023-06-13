namespace EaglesTMS.Models
{
    public class Nationalities :BaseEntity
    {
        public string iso { get; set; }
        public string name { get; set; }
        public string nicename { get; set; }
        public string iso3 { get; set; }
        public short numcode { get; set; }
        public int phonecode { get; set; }
    }
}
