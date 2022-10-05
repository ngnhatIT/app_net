using apiapp.Interfaces;
using MongoDB.Driver;

namespace apiapp.Context;

public class MongoContext : IMongoContext
{
    private IMongoDatabase Database { get; set; } = null!;
    public IClientSessionHandle Session { get; set; } = null!;
    public MongoClient MongoClient { get; set; } = null!;
    private readonly List<Func<Task>> _commands;
    private readonly IConfiguration _configuration;

    public MongoContext(IConfiguration configuration)
    {
        _configuration = configuration;

        _commands = new List<Func<Task>>();
    }

    public void AddCommand(Func<Task> func)
    {
        _commands.Add(func);
    }

    public void Dispose()
    {
        Session?.Dispose();
        GC.SuppressFinalize(this);
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        ConfigureMongo();

        return Database.GetCollection<T>(name);
    }

    public async Task<int> SaveChanges()
    {
        ConfigureMongo();

        using (Session = await MongoClient.StartSessionAsync())
        {
            Session.StartTransaction();

            var commandTasks = _commands.Select(c => c());

            await Task.WhenAll(commandTasks);

            await Session.CommitTransactionAsync();
        }

        return _commands.Count;
    }

    private void ConfigureMongo()
    {
        if (MongoClient != null)
        {
            return;
        }

        // Configure mongo (You can inject the config, just to simplify)
        MongoClient = new MongoClient(_configuration["MongoSettings:Connection"]);

        Database = MongoClient.GetDatabase(_configuration["MongoSettings:DatabaseName"]);
    }
}