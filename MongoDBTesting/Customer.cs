using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }

}