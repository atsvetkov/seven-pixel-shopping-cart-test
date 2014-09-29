namespace _7Pixel.ShoppingCart.Contracts
{
	public sealed class ShoppingCartItem
	{
		public ShoppingCartItem(string name, ProductType productType, ProductCategory productCategory, decimal price, int amount)
		{
			Name = name;
			ProductType = productType;
			ProductCategory = productCategory;
			Price = price;
			Amount = amount;
		}

		public decimal Price { get; private set; }
		public int Amount { get; private set; }
		public string Name { get; private set; }
		public ProductType ProductType { get; private set; }
		public ProductCategory ProductCategory { get; private set; }
	}
}