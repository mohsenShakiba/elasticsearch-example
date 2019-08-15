using System.Collections.Generic;
using System.Threading.Tasks;
using ElasticSearchExample.Application.Abstractions;
using ElasticSearchExample.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearchExample.Controllers
{
    [Route("/api/v1/[controller]")]
    public class CitiesController: Controller
    {
        private readonly ICitiesRepository citiesRepository;

        public CitiesController(ICitiesRepository citiesRepository)
        {
            this.citiesRepository = citiesRepository;
        }

        [HttpGet("{query}")]
        public async Task<IEnumerable<City>> Search([FromRoute] string query)
        {
            return await citiesRepository.FindAsync(query);
        }
        
        [HttpPost("insert/all/{path}")]
        public async Task<bool> InsertAll()
        {
            await citiesRepository.InsertAllAsync(@"/app/Static/cities.csv");
            return true;
        }
    }
}