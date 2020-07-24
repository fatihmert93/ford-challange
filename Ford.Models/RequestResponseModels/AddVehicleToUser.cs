namespace Ford.Models.RequestResponseModels
{
    public class AddVehicleToUserRequestModel
    {
        public int UserId { get; set; }
        public string  Vin { get; set; }
    }

    public class AddVehicleToUserResponseModel
    {
        public string Message { get; set; }
    }
}