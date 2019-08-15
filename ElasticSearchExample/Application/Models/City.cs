namespace ElasticSearchExample.Application.Models
{
    public class City
    {
        public int Id { get; set; }
        public string StateName { get; set; }
        public string Name { get; set; }

        public City(string stateName, string name)
        {
            Id = (stateName + name).GetHashCode();
            StateName = stateName;
            Name = name;
        }
        
    }
}