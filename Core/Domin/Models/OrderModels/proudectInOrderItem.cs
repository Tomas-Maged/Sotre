namespace Domian.Models.OrderModels
{
    public class proudectInOrderItem
    {
        public proudectInOrderItem()
        {
            
        }
        public proudectInOrderItem(int productId, string productName, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}