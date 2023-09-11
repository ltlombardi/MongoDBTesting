using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBTesting;
internal class MongoDBService
{
    internal static MongoClient GetClient()
    {
        // Remember that your ip must be added to the authorized list.
        // the password may have been changed   
        var pass = "sCVOKAhHd1o4E00z";
        var user = "ltlombardi";
        var connString = $"mongodb+srv://{user}:{pass}@cluster0.vj4ysgh.mongodb.net/?retryWrites=true&w=majority";
        var way = 4;
        switch (way)
        {
            case 1:
                {
                    //// Atlas Connect example with logging added to it
                    var settings = MongoClientSettings.FromConnectionString(connString);
                    settings.ServerApi = new ServerApi(ServerApiVersion.V1);

                    return new MongoClient(settings);
                }

            case 2:
                {
                    var loggerFactory = LoggerFactory.Create(b =>
                    {
                        b.AddSimpleConsole();
                        b.SetMinimumLevel(LogLevel.Trace);
                    });

                    var mongoLogger = loggerFactory.CreateLogger("MongoDB.Driver");
                    var settings = MongoClientSettings.FromConnectionString(connString);
                    settings.ClusterConfigurator = cb =>
                    {
                        cb.Subscribe<CommandStartedEvent>(e =>
                        {
                            mongoLogger.LogDebug($"MongoDB command started: {e.CommandName} - {e.Command.ToJson()}");
                        });
                    };
                    return new MongoClient(settings);
                }

            case 3:
                {
                    var loggerFactory = LoggerFactory.Create(b =>
                    {
                        b.AddSimpleConsole();
                        b.SetMinimumLevel(LogLevel.Trace);
                    });

                    //// Atlas Connect example with logging added to it
                    var settings = MongoClientSettings.FromConnectionString(connString);
                    settings.LoggingSettings = new LoggingSettings(loggerFactory);
                    settings.ServerApi = new ServerApi(ServerApiVersion.V1);
                    return new MongoClient(settings);

                }
            case 4:
                {
                    var categoriesConfiguration = new Dictionary<string, string>
                        {
                          { "LogLevel:Default", "Trace" }
                        };
                    var config = new ConfigurationBuilder()
                      .AddInMemoryCollection(categoriesConfiguration)
                      .Build();
                    var loggerFactory = LoggerFactory.Create(b =>
                    {
                        b.AddConfiguration(config);
                        b.AddSimpleConsole();
                    });

                    //// Atlas Connect example with logging added to it
                    var settings = MongoClientSettings.FromConnectionString(connString);
                    settings.LoggingSettings = new LoggingSettings(loggerFactory);
                    settings.ServerApi = new ServerApi(ServerApiVersion.V1);
                    return new MongoClient(settings);

                }
            default:
                {
                    // This is from MongoDb University course Lesson 2. Both seems to work
                    var mongoURL = new MongoUrl(connString);
                    return new MongoClient(mongoURL);

                }
        }

    }
}
