// See https://aka.ms/new-console-template for more information
using MongoDB.Driver;

Console.WriteLine("Hello, World!");
var client = GetClient();

BasicOperations.ListDataBases(client);
BasicOperations.ListCollections(client, "test_stuff");
BasicOperations.InsertUntyped(client, "test_stuff", "items");
BasicOperations.ListItems(client, "test_stuff", "items");


static MongoClient GetClient()
{
    // Remember that your ip must be added to the authorized list.
    // the password may have been changed   
    var pass = "sCVOKAhHd1o4E00z";
    var user = "ltlombardi";
    var connString = $"mongodb+srv://{user}:{pass}@cluster0.vj4ysgh.mongodb.net/?retryWrites=true&w=majority";
    var way = 2;
    if (way == 1)
    {
        // Atlas Connect example
        var settings = MongoClientSettings.FromConnectionString(connString);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        return new MongoClient(settings);
    }
    else
    {
        // This is from MongoDb University course Lesson 2. Both seems to work
        var mongoURL = new MongoUrl(connString);
        return new MongoClient(mongoURL);
    }
}