using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Exceptions
{
    public class basketcreateOrUpdateBadRequestException() :
        BadRequestException("Invalid Operation When Create Or Update Basket !")
    {
    }
}
