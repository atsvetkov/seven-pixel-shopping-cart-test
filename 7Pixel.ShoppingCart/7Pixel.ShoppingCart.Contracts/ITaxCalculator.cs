namespace _7Pixel.ShoppingCart.Contracts
{
	public interface ITaxCalculator
	{
		decimal GetSalesTaxPerItem(ShoppingCartItem shoppingCartItem);
	}
}