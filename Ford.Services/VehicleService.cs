using System.Threading.Tasks;
using Ford.Core.Abstracts;
using Ford.Core.Abstracts.Services;
using Ford.Models;

namespace Ford.Services
{
    public class VehicleService : IVehicleService
    {
        private IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }


        public Task<Vehicle> CreateVehicle(Vehicle vehicle)
        {
            throw new System.NotImplementedException();
        }
    }
}