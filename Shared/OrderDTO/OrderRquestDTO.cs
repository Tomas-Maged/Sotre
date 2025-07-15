using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ORderDTO
{
    public class OrderRquestDTO
    {
        public string BasketId { get; set; }
        public AddressDTO ShippToAddress { get; set; }
        public int DeliveryMethodId { get; set; }

    }
}
