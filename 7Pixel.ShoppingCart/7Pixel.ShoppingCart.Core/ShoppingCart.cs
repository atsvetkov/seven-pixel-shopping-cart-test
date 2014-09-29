using System;
using System.Collections.Generic;
using _7Pixel.ShoppingCart.Contracts;

namespace _7Pixel.ShoppingCart.Core
{
    public sealed class ShoppingCart : IShoppingCart
    {
	    private readonly ITaxCalculator _taxCalculator;
		
	    public ShoppingCart(ITaxCalculator taxCalculator)
	    {
		    _taxCalculator = taxCalculator;
	    }

	    private readonly IList<ShoppingCartItem> _items = new List<ShoppingCartItem>();

	    public void AddProduct(ShoppingCartItem shoppingCartItem)
	    {
		    if (shoppingCartItem == null)
		    {
			    throw new ArgumentNullException("shoppingCartItem");
		    }

		    _items.Add(shoppingCartItem);
	    }

	    public IReceipt GetReceipt()
	    {
			var receipt = new Receipt();
		    foreach (var shoppingCartItem in _items)
		    {
			    receipt.AddItem(shoppingCartItem, _taxCalculator.GetSalesTaxPerItem(shoppingCartItem));
		    }

		    return receipt;
	    }
    }
}
