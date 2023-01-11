// See https://aka.ms/new-console-template for more information
using MongoDB.Driver;

Console.WriteLine("Hello, World!");

// This is from Atlas Connect example
// Remember that your ip must be added to the authorized list.
// the password may have been changed   

var pass = "sCVOKAhHd1o4E00z";
var user = "ltlombardi";
var connString = $"mongodb+srv://{user}:{pass}@cluster0.vj4ysgh.mongodb.net/?retryWrites=true&w=majority";
var settings = MongoClientSettings.FromConnectionString(connString);
settings.ServerApi = new ServerApi(ServerApiVersion.V1);
var client = new MongoClient(settings);

BasicOperations.ListDataBases(client);
BasicOperations.ListCollections(client, "test_stuff");
BasicOperations.InsertUntyped(client, "test_stuff", "items");
BasicOperations.ListItems(client, "test_stuff", "items");

// This is from MongoDb University course Lesson 2. Both seems to work
var mongoURL = new MongoUrl(connString);
client = new MongoClient(mongoURL);
