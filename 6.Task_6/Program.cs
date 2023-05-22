using System;
using System.Collections.Generic;

namespace _6.Task_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int ShowAllProductCommand = 1;
            const int ShowInventoryCommand = 2;
            const int BuyItemCommand = 3;            
            const int AddProductToSellCommand = 4;
            const int ExitCommand = 5;

            bool isTrade = true;            
            int money;
            Seller seller = new Seller(1000);
            Player player = new Player(5000);
            Shop shop = new Shop(seller, player);

            while (isTrade)
            {
                Console.WriteLine($" {ShowAllProductCommand} - Show All items for buying");                
                Console.WriteLine($" {ShowInventoryCommand} - show you inventory");
                Console.WriteLine($" {BuyItemCommand} - Buy item from Seller");                
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
                        shop.SellSellerProduct();
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
        protected List<Product> Products = new List<Product>();

        public Merchants(int money)
        {
            Money = money;
            Products = new List<Product>();
        }

        public int Money { get; protected set; }

        public void ShowAllProducts()
        {
            int i = 1;

            foreach (var product in Products)
            {
                Console.Write($"{i}. {product.Name}");
                Console.WriteLine($"({product.Price})");
                i++;
            }
        }        
    }

    class Seller : Merchants
    {
        public Seller(int money) : base(money)
        {
            Money = money;
        }       

        public void CreateProduct()
        {
            Console.WriteLine("Enter the name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the price:");
            int.TryParse(Console.ReadLine(), out int result);
            Products.Add(new Product(name, result));
        }

        public Product GetProduct(int index)
        {
            return Products[index];
        }

        public void SellProduct(Product product)
        {
            Products.Remove(product);
        }

        public int GetProductCount()
        {
            return Products.Count;
        }

        public int GetProductPrice(int index)
        {
            if (Products.Count > index)
            {
                Product product = Products[index];
                return product.Price;
            }
            else
            {
                return 0;
            }
        }

        public void GetMoney(int money)
        {
            Money += money;
        }

        public string GetProductName(int index)
        {
            Product product = Products[index];
            return product.Name;
        }
    }

    class Player : Merchants
    {        
        public Player(int money) : base(money)
        {
            Money = money;            
        }

        public void AddProduct(string name, int price)
        {
            Products.Add(new Product(name, price));
        }

        public void TakeMoney(int money)
        {
            Money -= money;
        }
    }

    class Shop
    {
        public Shop (Seller seller, Player player)
        {
            _seller = seller;
            _player = player;
        }

        public Seller _seller { get; private set; }
        public Player _player { get; private set; }

        public void ShowSellerProductList ()
        {
            _seller.ShowAllProducts();
        }

        public void ShowPlayerInventoryList()
        {
            _player.ShowAllProducts();
        }

        public void SellSellerProduct()
        {
            Console.WriteLine("Input product number:");
            int.TryParse(Console.ReadLine(), out int index);
            index--;

            if (index<_seller.GetProductCount() && index>0)
            {
                if (_seller.GetProductPrice(index) <= _player.Money)
                {
                    _player.AddProduct(_seller.GetProductName(index), _seller.GetProductPrice(index));
                    _player.TakeMoney(_seller.GetProductPrice(index));
                    _seller.GetMoney(_seller.GetProductPrice(index));
                    _seller.SellProduct(_seller.GetProduct(index));
                }
                else
                {
                    Console.WriteLine("Not enough money!");
                }
            }
            else
            {
                Console.WriteLine("This product does not exist");
            }
        }       
        
        public void CreateProductForSell()
        {
            _seller.CreateProduct();
        }

        public int ShowPlayerMoney()
        {
            return _player.Money;        
        }
    }
}
