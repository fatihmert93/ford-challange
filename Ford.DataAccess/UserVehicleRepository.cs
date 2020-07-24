using Ford.Core.Abstracts;
using Ford.Models;

namespace Ford.DataAccess
{
    public class UserVehicleRepository : EfRepositoryBase<UserVehicle>, IUserVehicleRepository
    {
        public UserVehicleRepository(FordDbContext entityContext) : base(entityContext)
        {
        }
    }
}