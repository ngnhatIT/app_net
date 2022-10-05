using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace apiapp.Persistence;

public static class MongoDbPersistence
{
    public static void Configure()
    {
        //BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;
        var pack = new ConventionPack
                {
                    new IgnoreExtraElementsConvention(true),
                    new IgnoreIfDefaultConvention(true)
                };
        ConventionRegistry.Register("My Solution Conventions", pack, t => true);
    }
}
