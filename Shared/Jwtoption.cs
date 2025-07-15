using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Jwtoption
    {
        public string issuer { get; set; }
        public string Audience { get; set; }
        public string SecuretyKey { get; set; }
        public Double DurationInDays { get; set; }
    }
}
