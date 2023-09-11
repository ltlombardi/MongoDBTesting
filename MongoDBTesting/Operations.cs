using BenchmarkDotNet.Attributes;
using Bogus;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

internal class Operations
{
    internal static void ListDataBases(MongoClient client)
    {
        var dbList = client.ListDatabases().ToList();

        Console.WriteLine("The list of databases on this server is: ");
        foreach (var db in dbList)
        {
            Console.WriteLine(db);
        }
    }

    internal static void ListCollections(MongoClient client, string databaseName)
    {
        var db = client.GetDatabase(databaseName);

        Console.WriteLine($"The list of collections for database {databaseName} is: ");
        foreach (var collection in db.ListCollectionNames().ToEnumerable())
        {
            Console.WriteLine(collection);
        }
    }

    internal static void InsertUntyped(MongoClient client, string databaseName, string collectionName)
    {
        var collection = client.GetDatabase(databaseName).GetCollection<BsonDocument>(collectionName);
        var document = new BsonDocument
        {
           { "account_id", "MDB829001337" },
           { "account_holder", "Linus Torvalds" },
           { "account_type", "checking" },
           { "balance", 50352434 }
        };
        collection.InsertOne(document);
    }

    //TODO: this is not done
    internal static void InsertTyped(MongoClient client, string databaseName, string collectionName)
    {
        var collection = client.GetDatabase(databaseName).GetCollection<BsonDocument>(collectionName);
        var document = new BsonDocument
        {
           { "account_id", "MDB829001337" },
           { "account_holder", "Linus Torvalds" },
           { "account_type", "checking" },
           { "balance", 50352434 }
        };
        collection.InsertOne(document);
    }

    internal static void ListItems(MongoClient client, string databaseName, string collectionName)
    {
        var collection = client.GetDatabase(databaseName).GetCollection<BsonDocument>(collectionName);

        Console.WriteLine($"The items in the collection {collectionName} are: ");
        foreach (var item in collection.Find(_ => true).ToEnumerable())
        {
            Console.WriteLine(item);
        }
    }

    public static Customer FirstById(IMongoCollection<Customer> collection, int id)
    {
        return collection.Find(Builders<Customer>.Filter.Eq("_id", id)).FirstOrDefault();
    }

    public static Customer SingleById(IMongoCollection<Customer> collection, int id)
    {
        return collection.Find(Builders<Customer>.Filter.Eq("_id", id)).SingleOrDefault();
    }
}

