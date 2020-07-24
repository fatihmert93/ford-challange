using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ford.Core.Abstracts;
using Ford.Core.Abstracts.Services;
using Ford.Models;
using Ford.Models.Enums;
using Ford.Security.Bearer;
using Microsoft.EntityFrameworkCore;

namespace Ford.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private readonly IUserVehicleRepository _userVehicleRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public UserService(IUserRepository userRepository, IUserVehicleRepository userVehicleRepository, IVehicleRepository vehicleRepository)
        {
            _userRepository = userRepository;
            _userVehicleRepository = userVehicleRepository;
            _vehicleRepository = vehicleRepository;
        }


        public async Task<JwtToken> Login(string username, string password)
        {
            var user = await _userRepository.Query(v => v.Username == username).FirstOrDefaultAsync();

            if (user != null)
            {
                if (user.Password == password)
                {
                    var token = new JwtTokenBuilder()
                        .AddSecurityKey(JwtSecurityKey.Create("fiver-secret-key"))
                        .AddSubject(username)
                        .AddIssuer("Fiver.Security.Bearer")
                        .AddAudience("Fiver.Security.Bearer")
                        .AddClaim("username", username)
                        .AddExpiry(100)
                        .Build();

                    return token;
                }
                else
                {
                    throw new Exception("password_not_correct");
                }
            }
            else
            {
                throw new Exception("user_not_found");
            }
        }

        public Task<bool> CreateUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> CreateUserVehicle(int userId, string vin)
        {
            try
            {
                var vehicle = await _vehicleRepository.Query(v => v.Vin == vin).FirstOrDefaultAsync();
                UserVehicle uv = new UserVehicle()
                {
                    Status = UserVehicleStatus.Passive,
                    UserId = userId,
                    VehicleId = vehicle.VehicleId
                };

                await _userVehicleRepository.CreateAsync(uv);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public async Task<IEnumerable<string>> GetUserVehicles(int userId)
        {
            var vehicles = await _userVehicleRepository
                .Query(v => v.UserId == userId).Select(v => v.Vehicle.Vin).ToListAsync();
            return vehicles;

        }
    }
}