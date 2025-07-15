using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ORderDTO
{
    public class OrderResultDto
    {
        public Guid ID { get; set; }
        public string UserEmail { get; set; }

        //Adderess
        public AddressDTO ShippingAdress { get; set; }
        //order Item
        public ICollection<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>(); //navigation property for OrderItems

        //Delivery Method
        public string deliveryMethod { get; set; } 

        //Payment Stutas
        public string PaymentStutas { get; set; } 

        //SubTotal
        public decimal SubTotal { get; set; }

        //orderDate
        public DateTimeOffset orderDate { get; set; } = DateTimeOffset.Now;

        //paymentId
        public string Paymentlnteptld { get; set; } = string.Empty;

        public Decimal Total { get; set; }

    }
}
