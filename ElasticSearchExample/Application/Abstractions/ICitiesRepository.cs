using System.Collections.Generic;
using System.Threading.Tasks;
using ElasticSearchExample.Application.Models;

namespace ElasticSearchExample.Application.Abstractions
{
    public interface ICitiesRepository
    {
        Task<IEnumerable<City>> FindAsync(string term);
        Task InsertAllAsync(string staticFilePath);
    }
}