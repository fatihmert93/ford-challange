namespace Ford.Models.RequestResponseModels
{
    public class UserVehiclesRequestModel
    {
        public int UserId { get; set; }
    }

    public class UserVehiclesResponseModel
    {
        public string[] Vehicles { get; set; }
    }
}