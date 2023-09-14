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

    public SuperMarket()
    {
        for (int i = 0; i < UserUtils.GenerateRandomNumber(2, 11); i++)
        {
            _queue.Enqueue(new Buyer());
        }

        Console.WriteLine($"В очереди на покупку: {_queue.Count} человек");
    }

    public void ServeQueue()
    {
        Buyer buyer = new Buyer();

        while (_queue.Count >= 0)
        {
            buyer = _queue.Dequeue();
        }

        ServeBuyer(buyer);
    }

    public void ServeBuyer(Buyer buyer)
    {
        int sum = buyer.GetAmount();
        bool isSuccess = false;

        while (isSuccess = false)
        {
            if (sum <= buyer.Money)
            {
                Console.WriteLine($"К оплате {sum}");
                Console.WriteLine($"Спасибо за покупку");
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
        foreach (Product showCaseProduct in _showCase)
        {
            Console.WriteLine("На витрине представлены следующие продукты:");
        }
        Product product = ;
        return product;
    }
}

class ShowCase
{
    private List<Product> _products;

    public ShowCase()
    {
        _products = new List<Product>();
        {
            new Product("Огурец", 70);
            new Product("Яблоко", 12);
            new Product("Банан", 14);
            new Product("Молоко", 20);
            new Product("Картофель", 45);
            new Product("Лук", 18);
            new Product("Хурма", 62);
            new Product("Минералка", 33);
            new Product("Энергетик", 48);
            new Product("Пиво", 50);
            new Product("Водка", 130);
            new Product("Хлеб", 21);
        }
    }

    public Product PrepareProduct()
    {
        Product product = _products.ElementAt(UserUtils.GenerateRandomNumber(0, _products.Count));
        return product;
    }
}

class Buyer
{
    private List<Product> _purchasedProducts;
    public List<Product> _buyerCart = new List<Product>();

    public Buyer()
    {
        Money = UserUtils.GenerateRandomNumber(100, 3000);
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

    public void Pay(int cost)
    {
        if (Money >= cost)
        {
            Money -= cost;
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
