using BenchmarkDotNet.Attributes;
using Bogus;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBTesting;

[MemoryDiagnoser(false)]
public class Benchmarks
{
    /* When calling mongoDB to get an item by Id, should I use First instead of Single? 
     * Although semantically makes more sense to use Single, performance wise First is better because we know ID is unique
     * due to DB constraints.
     * But I believe that due to the way the DB is constructed, Single would have almost the same performance because the
     * DB will use the indexes and return a single item, and the Single will not have to search the whole collection.
     * So using First only makes sense when using a List, or something like that.
     * Based on https://www.youtube.com/watch?v=ZTWl2s8ScMc
     */

    [Params(0, 5000, 9999)]
    public int Id { get; set; }

    public IMongoCollection<Customer> Collection { get; set; }

    public  Benchmarks()
    {
        var databaseName = "FirstVsSingle";
        var collectionName = "Test";
        Collection = MongoDBService.GetClient().GetDatabase(databaseName).GetCollection<Customer>(collectionName);
        if (Collection.CountDocuments(Builders<Customer>.Filter.Empty) == 0)
        {
            Randomizer.Seed = new Random(42); // to make the faker always generate same data.
            var faker = new Faker<Customer>()
                .RuleFor(x => x.Id, faker => faker.IndexFaker)
                .RuleFor(x => x.FullName, faker => faker.Person.FullName)
                .RuleFor(x => x.Email, faker => faker.Person.Email)
                .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth);
            Collection.InsertMany(faker.Generate(10000));
        }
    }

    [Benchmark]
    public Customer FirstById()
    {
        return Operations.FirstById(Collection,Id);    
    }

    [Benchmark]
    public Customer SingleById()
    {
        return Operations.SingleById(Collection, Id);
    }
}
