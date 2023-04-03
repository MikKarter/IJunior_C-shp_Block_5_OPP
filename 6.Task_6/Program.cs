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
            const int BuyItemCommand = 2;
            const int ShowInventoryCommand = 3;
            const int AddProductToSellCommand = 4;
            const int ExitCommand = 5;

            bool isTrade = true;
            Seller seller = new Seller(1000);
            Player player = new Player(500);

            while (isTrade)
            {
                Console.WriteLine($" {ShowAllProductCommand} - Show All items for buying");
                Console.WriteLine($" {BuyItemCommand} - Buy item from Seller");
                Console.WriteLine($" {ShowInventoryCommand} - show you inventory");
                Console.WriteLine($" {AddProductToSellCommand} - add procudt to seller list");
                Console.WriteLine($" {ExitCommand} - FINISH TRADE");
                Console.WriteLine($"Деньги игрока - {player.GetMoney()}");

                int.TryParse(Console.ReadLine(), out int key);

                switch (key)
                {
                    case ShowAllProductCommand:
                        seller.ShowAllProducts();
                        break;
                    case BuyItemCommand:
                        seller.SellProduct(player, Convert.ToInt32(Console.ReadLine()));
                        break;
                    case ShowInventoryCommand:
                        player.ShowProducts();
                        break;
                    case AddProductToSellCommand:
                        seller.AddProduct();
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
        private string _name;
        private int _price;
        public Product(string name, int price)
        {
            _name = name;
            _price = price;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetPrice()
        {
            return _price;
        }
    }

    class Seller
    {
        private List<Product> _products = new List<Product>();
        private int _money;

        public Seller(int money)
        {
            _money = money;
            _products = new List<Product>();
        }

        public void AddProduct()
        {            
            Console.WriteLine("Enter the name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the price:");
            int.TryParse(Console.ReadLine(), out int resault);
            _products.Add(new Product(name, resault));
        }

        public void ShowAllProducts()
        {
            int i = 1;

            foreach (var product in _products)
            {                
                Console.Write($"{i}. {product.GetName()}");
                Console.WriteLine($"({product.GetPrice()})");
                i++;
            }
        }

        public void SellProduct(Player player, int index)
        {
            if (index-1 >= 0 && index <= _products.Count)
            {
                Product productForSell = _products[index-1];
                if (player.BuyProduct(productForSell.GetPrice()))
                {
                    _products.RemoveAt(index-1);
                    player.AddProduct(productForSell);
                    _money += productForSell.GetPrice();
                    Console.WriteLine("Sold: " + productForSell.GetName());
                }
                else
                {
                    Console.WriteLine("Not enaugh money for buying " + productForSell.GetName());
                }
            }
            else
            {
                Console.WriteLine("Incorrect product number!");
            }
        }

        public int GetMoney()
        {
            return _money;
        }
    }

    class Player
    {
        private List<Product> _products;
        private int _money;

        public Player(int money)
        {
            _products = new List<Product>();
            _money = money;
        }

        public bool BuyProduct(int price)
        {
            if (_money >= price)
            {
                _money -= price;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public void ShowProducts()
        {
            foreach (Product product in _products)
            {
                int i = 1;
                Console.WriteLine(i + ". " + product.GetName() + " - " + product.GetPrice());
            }
        }

        public int GetMoney()
        {
            return _money;
        }
    }
}
