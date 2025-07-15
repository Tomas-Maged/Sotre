using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Models.OrderModels
{
    public class order : BaseEntity<Guid>
    {
        public order()
        {
            
        }
        public order(string userEmail, Address shippingAdress, ICollection<OrderItem> orderItems, DeliveryMethod deliveryMethod, decimal subTotal, string paymentlnteptld)
        {
            Id = Guid.NewGuid(); 
            UserEmail = userEmail;
            ShippingAdress = shippingAdress;
            OrderItems = orderItems;
            this.deliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            Paymentlnteptld = paymentlnteptld;
        }

        public String UserEmail { get; set; }

        //Adderess
        public Address ShippingAdress { get; set; }
        //order Item
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); //navigation property for OrderItems

        //Delivery Method
        public DeliveryMethod deliveryMethod { get; set; } //navigation property for DeliveryMethod
        public int? deliveryMethodId { get; set; } //FK

        //Payment Stutas
        public OrderPaymentStutas PaymentStutas { get; set; } = OrderPaymentStutas.Pending;

        //SubTotal
        public Decimal SubTotal { get; set; }

        //orderDate
        public DateTimeOffset orderDate { get; set; } = DateTimeOffset.Now;

        //paymentId
        public string Paymentlnteptld { get; set; }



    }
}
