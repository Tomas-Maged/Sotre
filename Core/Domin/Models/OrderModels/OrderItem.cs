namespace Domian.Models.OrderModels
{
    public class OrderItem :  BaseEntity<Guid>
    {
        public OrderItem()
        {
        }
        public OrderItem(proudectInOrderItem proudect, decimal price, int quantity)
        {
            Proudect = proudect;
            Price = price;
            Quantity = quantity;
        }

        public proudectInOrderItem Proudect { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}