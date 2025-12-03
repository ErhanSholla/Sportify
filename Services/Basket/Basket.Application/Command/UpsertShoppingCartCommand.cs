using Basket.Application.Responses;
using MediatR;

namespace Basket.Application.Command
{
    public class UpsertShoppingCartCommand : IRequest<ShoppingCartResponse>
    {
        public string UserName { get; set; }
        public List<ShoppingCartItemRequest> Items { get; set; }

        public UpsertShoppingCartCommand(string userName, List<ShoppingCartItemRequest> items)
        {
            UserName = userName;
            Items = items ?? new();
        }
    }
}
