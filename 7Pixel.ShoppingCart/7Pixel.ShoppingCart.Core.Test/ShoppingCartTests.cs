using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _7Pixel.ShoppingCart.Contracts;

namespace _7Pixel.ShoppingCart.Core.Test
{
	[TestClass]
	public class ShoppingCartTests
	{
		[TestMethod]
		public void ShouldCalculateTaxesForNonImportedProducts()
		{
			// arrange
			ITaxCalculator taxCalculator = new DefaultTaxCalculator();
			IShoppingCart cart = new ShoppingCart(taxCalculator);

			cart.AddProduct(new ShoppingCartItem("Book", ProductType.General, ProductCategory.Book, 12.49m, 1));
			cart.AddProduct(new ShoppingCartItem("CD", ProductType.General, ProductCategory.Other, 14.99m, 1));
			cart.AddProduct(new ShoppingCartItem("ChocolateBar", ProductType.General, ProductCategory.Food, 0.85m, 1));

			// act
			IReceipt receipt = cart.GetReceipt();
			
			// assert
			Assert.IsNotNull(receipt);

			var receiptItems = receipt.Items.ToArray();
			Assert.IsNotNull(receipt.Items);

			Assert.AreEqual(3, receiptItems.Length);

			Assert.AreEqual("Book", receiptItems[0].ItemName);
			Assert.AreEqual(1, receiptItems[0].Amount);
			Assert.AreEqual(12.49m, receiptItems[0].FinalPrice);

			Assert.AreEqual("CD", receiptItems[1].ItemName);
			Assert.AreEqual(1, receiptItems[1].Amount);
			Assert.AreEqual(16.49m, receiptItems[1].FinalPrice);

			Assert.AreEqual("ChocolateBar", receiptItems[2].ItemName);
			Assert.AreEqual(1, receiptItems[2].Amount);
			Assert.AreEqual(0.85m, receiptItems[2].FinalPrice);

			Assert.AreEqual(1.5m, receipt.SalesTaxes);
			Assert.AreEqual(29.83m, receipt.Total);
		}

		[TestMethod]
		public void ShouldCalculateTaxesForImportedProducts()
		{
			// arrange
			ITaxCalculator taxCalculator = new DefaultTaxCalculator();
			IShoppingCart cart = new ShoppingCart(taxCalculator);

			cart.AddProduct(new ShoppingCartItem("BoxOfChocolates", ProductType.Imported, ProductCategory.Food, 10m, 1));
			cart.AddProduct(new ShoppingCartItem("BottleOfPerfume", ProductType.Imported, ProductCategory.Perfume, 47.5m, 1));

			// act
			IReceipt receipt = cart.GetReceipt();

			// assert
			Assert.IsNotNull(receipt);

			var receiptItems = receipt.Items.ToArray();
			Assert.IsNotNull(receipt.Items);

			Assert.AreEqual(2, receiptItems.Length);

			Assert.AreEqual("BoxOfChocolates", receiptItems[0].ItemName);
			Assert.AreEqual(1, receiptItems[0].Amount);
			Assert.AreEqual(10.5m, receiptItems[0].FinalPrice);

			Assert.AreEqual("BottleOfPerfume", receiptItems[1].ItemName);
			Assert.AreEqual(1, receiptItems[1].Amount);
			Assert.AreEqual(54.65m, receiptItems[1].FinalPrice);

			Assert.AreEqual(7.65m, receipt.SalesTaxes);
			Assert.AreEqual(65.15m, receipt.Total);
		}

		[TestMethod]
		public void ShouldCalculateTaxesForMixedProducts()
		{
			// arrange
			ITaxCalculator taxCalculator = new DefaultTaxCalculator();
			IShoppingCart cart = new ShoppingCart(taxCalculator);

			cart.AddProduct(new ShoppingCartItem("ImportedBottleOfPerfume", ProductType.Imported, ProductCategory.Perfume, 27.99m, 1));
			cart.AddProduct(new ShoppingCartItem("BottleOfPerfume", ProductType.General, ProductCategory.Perfume, 18.99m, 1));
			cart.AddProduct(new ShoppingCartItem("PacketOfHeadachePills", ProductType.General, ProductCategory.Medicine, 9.75m, 1));
			cart.AddProduct(new ShoppingCartItem("ImportedBoxOfChocolates", ProductType.Imported, ProductCategory.Food, 11.25m, 1));

			// act
			IReceipt receipt = cart.GetReceipt();

			// assert
			Assert.IsNotNull(receipt);

			var receiptItems = receipt.Items.ToArray();
			Assert.IsNotNull(receipt.Items);

			Assert.AreEqual(4, receiptItems.Length);

			Assert.AreEqual("ImportedBottleOfPerfume", receiptItems[0].ItemName);
			Assert.AreEqual(1, receiptItems[0].Amount);
			Assert.AreEqual(32.19m, receiptItems[0].FinalPrice);

			Assert.AreEqual("BottleOfPerfume", receiptItems[1].ItemName);
			Assert.AreEqual(1, receiptItems[1].Amount);
			Assert.AreEqual(20.89m, receiptItems[1].FinalPrice);

			Assert.AreEqual("PacketOfHeadachePills", receiptItems[2].ItemName);
			Assert.AreEqual(1, receiptItems[2].Amount);
			Assert.AreEqual(9.75m, receiptItems[2].FinalPrice);

			Assert.AreEqual("ImportedBoxOfChocolates", receiptItems[3].ItemName);
			Assert.AreEqual(1, receiptItems[3].Amount);
			Assert.AreEqual(11.8m, receiptItems[3].FinalPrice);

			Assert.AreEqual(6.65m, receipt.SalesTaxes);
			Assert.AreEqual(74.63m, receipt.Total);
		}
	}
}
