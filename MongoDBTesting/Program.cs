// See https://aka.ms/new-console-template for more information
using MongoDB.Driver;

Console.WriteLine("Hello, World!");

// This is from Atlas Connect example
var pass = "sCVOKAhHd1o4E00z";
var user = "ltlombardi";
var connString = $"mongodb+srv://{user}:{pass}@cluster0.vj4ysgh.mongodb.net/?retryWrites=true&w=majority";
var settings = MongoClientSettings.FromConnectionString(connString);
settings.ServerApi = new ServerApi(ServerApiVersion.V1);
var client = new MongoClient(settings);

var dbList = client.ListDatabases().ToList();

Console.WriteLine("The list of databases on this server is: ");
foreach (var db in dbList)
{
    Console.WriteLine(db);
}

// This is from MongoDb University course Lesson 2
var mongoURL = new MongoUrl(connString);
client = new MongoClient(mongoURL);

Console.WriteLine("The list of databases on this server is: ");
foreach (var db in dbList)
{
    Console.WriteLine(db);
}