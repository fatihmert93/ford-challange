using Ford.Core.Abstracts;
using Ford.Models;

namespace Ford.DataAccess
{
    public class UserRepository : EfRepositoryBase<User>, IUserRepository
    {
        public UserRepository(FordDbContext entityContext) : base(entityContext)
        {
        }
    }
}