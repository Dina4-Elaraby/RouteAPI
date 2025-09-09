using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared_DTOs_.ErrorModels
{
    public class ValidationErrorToReturn
    {
        public int StatusCode { get; set; } = (int)HttpStatusCode.BadRequest;
        public string MessageError { get; set; } = "Validation Field";
        public IEnumerable<ValidationErrors> validationErrors { get; set; } = [];
    }
}
