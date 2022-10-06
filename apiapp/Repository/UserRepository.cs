using apiapp.Interfaces;
using apiapp.Model;

namespace apiapp.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IMongoContext context) : base(context)
        {
        }
    }
}