using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Exceptions
{
    public class ProductNotFoundExceptions(int id) : NotFoundException($"prodect with id {id} not found")
    {

    }
    }
