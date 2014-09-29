namespace _7Pixel.ShoppingCart.Contracts
{
	public interface IShoppingCart
	{
		void AddProduct(ShoppingCartItem shoppingCartItem);
		IReceipt GetReceipt();
	}
}