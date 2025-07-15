using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Exceptions
{
    public class OrderCreateBadRequestException() : NotFoundException("Invalid Operation When Create Order !")
    {
    }
}
