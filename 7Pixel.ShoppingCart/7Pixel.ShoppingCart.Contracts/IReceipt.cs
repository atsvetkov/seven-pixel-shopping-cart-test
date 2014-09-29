using System.Collections.Generic;

namespace _7Pixel.ShoppingCart.Contracts
{
	public interface IReceipt
	{
		IEnumerable<ReceiptItem> Items { get; }
		decimal SalesTaxes { get; }
		decimal Total { get; }
		void AddItem(ShoppingCartItem shoppingCartItem, decimal tax);
	}
}