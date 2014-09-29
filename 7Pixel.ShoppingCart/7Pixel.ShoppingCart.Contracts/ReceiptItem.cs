namespace _7Pixel.ShoppingCart.Contracts
{
	public sealed class ReceiptItem
	{
		public int Amount { get; set; }
		public string ItemName { get; set; }
		public decimal FinalPrice { get; set; }
	}
}