using System.Collections.Generic;
using _7Pixel.ShoppingCart.Contracts;

namespace _7Pixel.ShoppingCart.Core
{
	public class Receipt : IReceipt
	{
		private readonly List<ReceiptItem> _items = new List<ReceiptItem>();

		public IEnumerable<ReceiptItem> Items
		{
			get { return _items.AsReadOnly(); }
		}

		public decimal SalesTaxes { get; private set; }
		public decimal Total { get; private set; }

		public void AddItem(ShoppingCartItem shoppingCartItem, decimal tax)
		{
			var finalPrice = shoppingCartItem.Price + tax;
			SalesTaxes += tax * shoppingCartItem.Amount;
			Total += finalPrice * shoppingCartItem.Amount;
			
			_items.Add(new ReceiptItem
			{
				Amount = shoppingCartItem.Amount,
				ItemName = GetReceiptName(shoppingCartItem),
				FinalPrice = finalPrice
			});
		}

		private static string GetReceiptName(ShoppingCartItem shoppingCartItem)
		{
			return shoppingCartItem.Name;
		}
	}
}