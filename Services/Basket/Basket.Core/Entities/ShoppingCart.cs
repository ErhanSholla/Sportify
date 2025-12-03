namespace Basket.Core.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; } = default!;
        public List<ShoppingCartItem> Items = new();
        public decimal TotalSum => Items.Sum(i => i.Quantity * i.Price);
        public ShoppingCart()
        {

        }

        public ShoppingCart(string username)
        {
            UserName = username;
        }
    }
}
