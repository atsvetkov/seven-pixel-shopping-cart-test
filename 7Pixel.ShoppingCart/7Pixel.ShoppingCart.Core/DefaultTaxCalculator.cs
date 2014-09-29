using System;
using _7Pixel.ShoppingCart.Contracts;

namespace _7Pixel.ShoppingCart.Core
{
	public class DefaultTaxCalculator : ITaxCalculator
	{
		public decimal GetSalesTaxPerItem(ShoppingCartItem shoppingCartItem)
		{
			var percentage = GetBaseSalesTax(shoppingCartItem.ProductCategory);

			if (shoppingCartItem.ProductType == ProductType.Imported)
			{
				percentage += 0.05m;
			}

			return Math.Round(percentage * shoppingCartItem.Price * 20, MidpointRounding.AwayFromZero) / 20;
		}

		private decimal GetBaseSalesTax(ProductCategory productCategory)
		{
			if (productCategory == ProductCategory.Book || productCategory == ProductCategory.Food || productCategory == ProductCategory.Medicine)
			{
				return 0;
			}

			return 0.1m;
		}
	}
}