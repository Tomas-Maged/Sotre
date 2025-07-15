using Domian.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderSpecifications : BaseSpecifications<order, Guid>
    {
        public OrderSpecifications(Guid id) :base(o=>o.Id == id )
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.deliveryMethod);
        }
        public OrderSpecifications(string userEmail) : base(o => o.UserEmail == userEmail)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.deliveryMethod);
            AddOrderByDescending(o => o.orderDate);
        }
    }
}
