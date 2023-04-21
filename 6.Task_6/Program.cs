using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _6.Task_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int ShowAllProductCommand = 1;
            const int ShowInventoryCommand = 2;
            const int BuyItemCommand = 3;
            const int SellItemCommand = 4;
            const int AddProductToSellCommand = 5;
            const int ExitCommand = 6;

            bool isTrade = true;
            int userInput;
            int money;
            Shop shop = new Shop(1000,5000);

            while (isTrade)
            {
                Console.WriteLine($" {ShowAllProductCommand} - Show All items for buying");                
                Console.WriteLine($" {ShowInventoryCommand} - show you inventory");
                Console.WriteLine($" {BuyItemCommand} - Buy item from Seller");
                Console.WriteLine($" {SellItemCommand} - Sell item from inventory");
                Console.WriteLine($" {AddProductToSellCommand} - add procudt to seller list");
                Console.WriteLine($" {ExitCommand} - FINISH TRADE");
                money = shop.ShowPlayerMoney();
                Console.WriteLine($"Деньги игрока - { money }");

                int.TryParse(Console.ReadLine(), out int key);

                switch (key)
                {
                    case ShowAllProductCommand:
                        shop.ShowSellerProductList();
                        break;
                    case ShowInventoryCommand:
                        shop.ShowPlayerInventoryList();
                        break;
                    case BuyItemCommand:
                        Console.WriteLine("Input product number:");
                        int.TryParse(Console.ReadLine(), out userInput);
                        shop.SellSellerProduct(userInput);
                        break;
                    case SellItemCommand:
                        Console.WriteLine("Input product number:");
                        int.TryParse(Console.ReadLine(), out userInput);
                        shop.SellPlayerProduct(userInput);
                        break;
                    case AddProductToSellCommand:
                        shop.CreateProductForSell();
                        break;
                    case ExitCommand:
                        isTrade = false;
                        break;
                    default:
                        Console.WriteLine("Enter corret number!");
                        break;
                }
            }
        }
    }

    class Product
    {
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }
        public int Price { get; private set; }
    }

    class Merchants
    {
        protected List<Product> products = new List<Product>();
                
        public Merchants(int money)
        {
            this.Money = money;
            products = new List<Product>();
        }

        public int Money { get; protected set; }

        public void ShowAllProducts()
        {
            int i = 1;

            foreach (var product in products)
            {
                Console.Write($"{i}. {product.Name}");
                Console.WriteLine($"({product.Price})");
                i++;
            }
        }

        public void SellProduct(int index)
        {
            products.RemoveAt(index);
        }

        public int ShowProductPrice(int index)
        {
            Product product = products[index];            
            return product.Price;
        }

        public string ShowProductName(int index)
        {
            Product product = products[index];
            return product.Name;
        }

        public void GiveMoney(int money)
        {
            this.Money += money;
        }

        public void TakeMoney(int money)
        {
            this.Money -= money;
        }

        public void AddProduct(string name, int price)
        {
            products.Add(new Product(name, price));
        }

    }

    class Seller : Merchants
    {
        public Seller(int money) : base(money)
        {
            this.Money = money;
        }       

        public void CreateProduct()
        {
            Console.WriteLine("Enter the name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the price:");
            int.TryParse(Console.ReadLine(), out int result);
            products.Add(new Product(name, result));
        }
    }

    class Player : Merchants
    {        
        public Player(int money) : base(money)
        {
            this.Money = money;            
        }
    }

    class Shop
    {
        public Shop (int moneySeller, int moneyPlayer)        
        {
            Seller seller = new Seller(moneySeller);
            Player player = new Player(moneyPlayer);
        }


        public void ShowSellerProductList ()
        {
            seller.ShowAllProducts();
        }

        public void ShowPlayerInventoryList()
        {
            player.ShowAllProducts();
        }

        public void SellSellerProduct(int index)
        {
            index--;

            if (seller.ShowProductPrice(index)<=player.Money)
            {                
                player.AddProduct(seller.ShowProductName(index), seller.ShowProductPrice(index));
                player.TakeMoney(seller.ShowProductPrice(index));
                seller.GiveMoney(seller.ShowProductPrice(index));
                seller.SellProduct(index);
            }
            else
            {
                Console.WriteLine("Not enough money!");
            }
        }

        public void SellPlayerProduct(int index)
        {
            index--;

            if (player.ShowProductPrice(index) <= seller.Money)
            {                
                seller.AddProduct(player.ShowProductName(index), player.ShowProductPrice(index));
                seller.TakeMoney(player.ShowProductPrice(index));
                player.GiveMoney(player.ShowProductPrice(index));
                player.SellProduct(index);
            }
            else
            {
                Console.WriteLine("Not enough money!");
            }
        }

        public void CreateProductForSell()
        {
            seller.CreateProduct();
        }

        public int ShowPlayerMoney()
        {
            return player.Money;
        }
    }
}
