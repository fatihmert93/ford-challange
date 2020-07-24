using System.ComponentModel.DataAnnotations.Schema;
using Ford.Models.Enums;

namespace Ford.Models
{
    
    public class UserVehicle : EntityBase
    {
        public int UserVehicleId { get; set; }
        public int UserId { get; set; }
        public int VehicleId { get; set; }
        public UserVehicleStatus Status { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }
        
    }
}