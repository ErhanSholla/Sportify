namespace Basket.Core.Entities
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public string ProductId { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public string ProductName { get; set; } = default!;
    }
}
