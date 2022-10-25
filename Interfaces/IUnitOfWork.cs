namespace apiapp.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<bool> Commit();
}
