using Ford.Core.Abstracts;
using Ford.Models;

namespace Ford.DataAccess
{
    public class VehicleRepository : EfRepositoryBase<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(FordDbContext entityContext) : base(entityContext)
        {
        }
    }
}