using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorsModeLs
{
    public class ValidationErrorResponse
    {
        public int StadusCode { get; set; } = StatusCodes.Status400BadRequest;
        public string ErrorMessage { get; set; } = "Validation Error";
        public List<ValidationError> Errors { get; set; }


    }
}
