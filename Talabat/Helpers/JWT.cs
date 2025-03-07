namespace Talabat.Helpers
{
    public class JWT
    {
        public string SecurityKey { get; set; }
        public string Audience { get; set; }
        public string Issure { get; set; }
        public double DurationInDays { get; set; }
    }
}
