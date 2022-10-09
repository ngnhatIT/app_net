using apiapp.Interfaces;
using MongoDB.Driver;
using ServiceStack;

namespace apiapp.Repository;

public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly IMongoContext Context;
    protected IMongoCollection<TEntity> DbSet;

    protected BaseRepository(IMongoContext context)
    {
        Context = context;

        DbSet = Context.GetCollection<TEntity>(typeof(TEntity).Name);
    }

    public void Add(TEntity obj)
    {
        Context?.AddCommand(() => DbSet!.InsertOneAsync(obj));
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
        return all.ToList();
    }

    public async Task<TEntity> GetById(Guid id)
    {
        var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
        return data.SingleOrDefault();
    }

    public void Remove(Guid id)
    {
        Context?.AddCommand(() => DbSet!.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id)));
    }

    public void Update(TEntity obj)
    {
        Context!.AddCommand(() => DbSet!.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.GetId()), obj));
    }

    public void Dispose()
    {
        Context?.Dispose();
    }
}