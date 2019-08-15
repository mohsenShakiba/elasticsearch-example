using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ElasticSearchExample.Application.Abstractions;
using ElasticSearchExample.Application.Models;
using FlatFiles;
using FlatFiles.TypeMapping;
using Nest;

namespace ElasticSearchExample.Repositories
{
    public class CitiesRepository: ICitiesRepository
    {
        private const string IndexName = "Cities";
        private readonly ElasticClient client;
        
        public CitiesRepository()
        {
            client = new ElasticClient(new Uri("http://elasticsearch:9200"));
        }

        public async Task<IEnumerable<City>> FindAsync(string term)
        {
            var searchResponse = await client.SearchAsync<City>(s => s
                .From(0)
                .Size(10)
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Name)
                        .Field(f => f.StateName)
                        .Fuzziness(Fuzziness.Auto)
                        .FuzzyTranspositions()
                        .Query(term)
                    )
                )
                .Index(IndexName)
            );
            return searchResponse.Documents;
        }

        public async Task InsertAllAsync(string staticFilePath)
        {
            Console.WriteLine($"adding all cities");
            var mapper = SeparatedValueTypeMapper.Define<City>();
            mapper.Property(c => c.Name).ColumnName("name");
            mapper.Property(c => c.StateName).ColumnName("state_name");
            using (var reader = new StreamReader(File.OpenRead(staticFilePath)))
            {
                var options = new SeparatedValueOptions() { IsFirstRecordSchema = true };
                var cites = mapper.Read(reader, options).ToList();
                await client.IndexManyAsync(cites, IndexName);
                Console.WriteLine($"added {cites.Count} documents");
            }
        }
    }
}