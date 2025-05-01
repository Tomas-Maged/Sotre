using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ValidationException(IEnumerable<string> Errors) : Exception("Validation Error")
    {
        public IEnumerable<string> Errors { get; } = Errors;
    }
}
