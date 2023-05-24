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

            Shop shop = new Shop();

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
                        shop.TransferProduct();
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

        public void Sell(Product product)
        {
            GetMoney(product.Price);
            Products.Remove(product);
        }     

        public void GetMoney(int money)
        {
            Money += money;
        }

        public bool TryGetProduct (out Product product)
        {
            Console.WriteLine("Input product number:");
            int.TryParse(Console.ReadLine(), out int number);

            if (number>0&& number<=Products.Count)
            {
                product = Products[number-1];
                return true;
            }
            else 
            {
                product = null;                
                return false; 
            }
        }
    }

    class Player : Merchants
    {        
        public Player(int money) : base(money)
        {
            Money = money;            
        }

        public void Buy(Product product)
        {
            Products.Add(product);
            Money -= product.Price;
        }

        public bool CanPay(int price)
        {
            return Money >= price;
        }
    }

    class Shop
    {
        private Seller _seller;
        private Player _player;

        public Shop ()
        {
            _seller = new Seller (0);
            _player = new Player(5000);
        }

        public void ShowSellerProductList ()
        {
            _seller.ShowAllProducts();
        }

        public void ShowPlayerInventoryList()
        {
            _player.ShowAllProducts();
        }

        public void TransferProduct()
        {
            if (_seller.TryGetProduct(out Product product)==false)
            {
                Console.WriteLine("Incorrect number!");
                return;
            }

            if (_player.CanPay(product.Price) == false) 
            {
                Console.WriteLine("Not enough money!");
                return;
            }

            _player.Buy(product);            
            _seller.Sell(product);         
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
