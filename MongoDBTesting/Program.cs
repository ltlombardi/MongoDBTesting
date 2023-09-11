// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Logging;
using MongoDBTesting;

Console.WriteLine("Hello, World!");

var client = MongoDBService.GetClient();

//Operations.ListDataBases(client);
//Operations.ListCollections(client, "test_stuff");
//Operations.InsertUntyped(client, "test_stuff", "items");
//Operations.ListItems(client, "test_stuff", "items");

var databaseName = "FirstVsSingle";
var collectionName = "Test";
var collection = client.GetDatabase(databaseName).GetCollection<Customer>(collectionName);
Operations.SingleById(collection,5000);
Operations.FirstById(collection, 5000);
//Operations.FirstById(collection,9999);

//BenchmarkRunner.Run<Benchmarks>();
