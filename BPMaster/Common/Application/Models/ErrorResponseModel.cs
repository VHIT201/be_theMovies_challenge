using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Models
{
    public class ErrorResponseModel
    {
        public HttpStatusCode HttpStatus { get; set; } = HttpStatusCode.InternalServerError;
        public string ErrorCode { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = "System Error";
    }
}
