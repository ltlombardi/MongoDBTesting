
using MongoDB.Bson;

internal class KeyValuePair
{
    public string Key { get; set; }
    public BsonDocument Value { get; set; }
    public string Description { get; set; }
}

