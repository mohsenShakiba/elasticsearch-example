namespace ElasticSearchExample.Application.Models
{
    public class City
    {
        public int Id => (StateName + Name).GetHashCode();
        public string StateName { get; set; }
        public string Name { get; set; }
    }
}