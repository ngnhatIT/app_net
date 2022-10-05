using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiapp.Model;
using MongoDB.Bson.Serialization;

namespace apiapp.Persistence;

public class ProductMap
{
    public static void Configure()
    {
        BsonClassMap.RegisterClassMap<Product>(map =>
        {
            map.AutoMap();
            map.SetIgnoreExtraElements(true);
            map.MapIdMember(x => x.Id);
            map.MapMember(x => x.Description).SetIsRequired(true);
        });
    }
}
