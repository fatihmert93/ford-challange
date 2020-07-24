using System;

namespace Ford.Models.RequestResponseModels
{
    public class LoginRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseModel
    {
        public string Message { get; set; }
        public object Data { get; set; }
        public string Token { get; set; }
        public bool Status { get; set; }
        public DateTime ExpiryTime { get; set; }
        public string ErrorMessage { get; set; }
    }
}