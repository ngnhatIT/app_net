using MongoDB.Bson.Serialization.Conventions;

namespace apiapp.Persistence;

public static class MongoDbPersistence
{
    public static void Configure()
    {
        var pack = new ConventionPack
                {
                    new IgnoreExtraElementsConvention(true),
                    new IgnoreIfDefaultConvention(true)
                };
        ConventionRegistry.Register("My Solution Conventions", pack, t => true);
    }
}
