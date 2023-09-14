using System;


internal class Program
{
    static void Main(string[] args)
    {
        SuperMarket superMarket = new SuperMarket();

        superMarket.ServeQueue();
    }
}

class SuperMarket
{
    private Queue<Buyer> _queue = new Queue<Buyer>();
    private ShowCase _showCase = new ShowCase();
    private int _minQueueCount = 1;
    private int _maxQueueCount = 6;

    public SuperMarket()
    {
        for (int i = 0; i < UserUtils.GenerateRandomNumber(_minQueueCount, _maxQueueCount); i++)
        {
            _queue.Enqueue(new Buyer());
        }        
    }

    public int GetCount()
    {
        return _queue.Count;
    }

    public void ServeQueue()
    {
        Buyer buyer = new Buyer();

        while (_queue.Count > 0)
        {
            Console.WriteLine($"В очереди на покупку: {GetCount()} человек");
            buyer = _queue.Dequeue();
            FillCart(buyer);
            ServeBuyer(buyer);
        }
    }

    public void FillCart(Buyer buyer)
    {
        const int Yes = 1;
        const int No = 2;

        bool isFinished = false;

        _showCase.ShowProducts();

        while (isFinished == false)
        {
            buyer.AddProduct(TakeProduct());
            Console.WriteLine("Хотите взять что то ещё? \n1 - Да, 2 - Нет.");
            int.TryParse(Console.ReadLine(), out int input);

            switch (input)
            {
                case Yes:
                    break;
                case No:
                    isFinished = true;
                    break;
            }
        }
    }

    public void ServeBuyer(Buyer buyer)
    {
        int sum = buyer.GetAmount();
        bool isSuccess = false;

        while (isSuccess == false)
        {
            if (sum <= buyer.Money)
            {
                Console.WriteLine($"К оплате {sum}");
                Console.WriteLine($"Спасибо за покупку");
                buyer.Pay(sum);
                isSuccess = true;
            }
            else
            {
                Product product = buyer.ChooseProduct();
                Console.WriteLine($"Недостаточно денег, из вашей корзины будет удалён {product.Name} товар");
                buyer.DropProduct(product);
            }
        }
    }

    public Product TakeProduct()
    {
        Console.WriteLine("Выберите номер продукта, который хотите положить в корзину.");
        int.TryParse(Console.ReadLine(), out int input); ;
        Product product = _showCase.PickProduct(input);
        return product;
    }
}

class ShowCase
{
    private List<Product> _products;

    public ShowCase()
    {
        _products = new List<Product>
        {
            new Product("Огурец", 70),
            new Product("Яблоко", 12),
            new Product("Банан", 14),
            new Product("Молоко", 20),
            new Product("Картофель", 45),
            new Product("Лук", 18),
            new Product("Хурма", 62),
            new Product("Минералка", 33),
            new Product("Энергетик", 48),
            new Product("Пиво", 50),
            new Product("Водка", 130),
            new Product("Хлеб", 21)
        };
    }

    public void ShowProducts()
    {
        int i = 1;

        Console.WriteLine($"На витрине представлены следующие товары:");

        foreach (Product product in _products)
        {
            Console.WriteLine($"{i}.{product.Name} - {product.Cost}");
            i++;
        }
    }

    public Product PickProduct(int number)
    {
        Product product = _products.ElementAt(number - 1);
        return product;
    }
}

class Buyer
{
    private List<Product> _purchasedProducts;
    private List<Product> _buyerCart = new List<Product>();
    private int _minMoney = 10;
    private int _maxMoney = 500;

    public Buyer()
    {
        Money = UserUtils.GenerateRandomNumber(_minMoney, _maxMoney);
        _purchasedProducts = new List<Product>();
    }

    public int Money { get; private set; }

    public int GetAmount()
    {
        int sum = 0;

        foreach (Product product in _buyerCart)
        {
            sum += product.Cost;
        }

        return sum;
    }

    public void Pay(int sum)
    {
        if (Money >= sum)
        {
            Money -= sum;
        }
    }

    public Product ChooseProduct()
    {
        int i = UserUtils.GenerateRandomNumber(0, _buyerCart.Count);
        Product product = _buyerCart[i];
        return product;
    }

    public void DropProduct(Product product)
    {
        _buyerCart.Remove(product);
    }

    public void AddProduct(Product product)
    {
        _buyerCart.Add(product);
    }
}

class Product
{
    public Product(string name, int cost)
    {
        Name = name;
        Cost = cost;
    }

    public string Name { get; private set; }
    public int Cost { get; private set; }
}

class UserUtils
{
    private static Random random = new Random();

    public static int GenerateRandomNumber(int min, int max)
    {
        return random.Next(min, max);
    }
}
