using System.Threading.Tasks;
using Ford.Models;

namespace Ford.Core.Abstracts.Services
{
    public interface IVehicleService
    {
        Task<Vehicle> CreateVehicle(Vehicle vehicle);
    }
}