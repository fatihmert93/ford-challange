using Microsoft.AspNetCore.Mvc;

namespace Ford.Api.Models
{
    public class ResponseModel
    {
        public bool Status { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
        public int StatusCode { get; set; }
        public IActionResult ActionResult { get; set; }
        public object Validations { get; set; }
    }
}