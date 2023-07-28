using System;
using System.Collections.Generic;

internal class Program
{
    static void Main(string[] args)
    {
        TrainPlanner planner = new TrainPlanner();
        planner.Work();
    }
}

class TrainPlanner
{
    public void Work()
    {
        const string StartProgramCommand = "1";
        const string EndProgramCommand = "2";

        bool isWorking = true;

        while (isWorking)
        {
            Console.WriteLine($"Добро пожаловать в конфигуратор поездов.\nВам доступны следующие команды:\n{StartProgramCommand}" +
                              $" - Создать и отправить новый поезд.\n{EndProgramCommand} - Выход из программы.");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case StartProgramCommand:
                    CreateTrain();
                    break;

                case EndProgramCommand:
                    isWorking = false;
                    break;

                default:
                    Console.WriteLine("Неверно введена команда! Повторите запрос!");
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    private int SellTicket()
    {
        int minTicketsSold = 100;
        int maxTicketsSold = 680;
        int quantityTickets = NumberRandomizer.GenerateRandomNumber(minTicketsSold, maxTicketsSold);

        Console.WriteLine($"Продано {quantityTickets} билетов.");

        return quantityTickets;
    }

    private Direction CreateDirection()
    {
        bool isCreated = false;
        Direction direction = null ;
       
        while (!isCreated)
        {
            Console.WriteLine("Введите пункт отправления:");
            string departureName = Console.ReadLine();
            Console.WriteLine("Введите пункт прибытия:");
            string arriveName = Console.ReadLine();

            if (departureName != arriveName)
            {
                direction = new Direction(departureName, arriveName);
                isCreated = true;                
            }
            else
            {
                Console.WriteLine("Отправная и конечная точки не могут быть одинаковы. Повторите запрос!");             
            }
        }

        return direction;
    }

    private void CreateTrain()
    {
        Direction direction = CreateDirection();
        List <RailwayWagon> railwayWagons = new List<RailwayWagon>();
        Train train = new Train(direction, railwayWagons);
        PassengerBoarding(train);

        Console.WriteLine("Поезд отправлен.");
        Console.ReadKey();
        Console.Clear();
    }

    private void PassengerBoarding(Train train)
    {
        int passengers = SellTicket();

        while (passengers > 0)
        {
            RailwayWagon railwayWagon = new RailwayWagon();
            railwayWagon.TakeSeats(passengers);
            train.AddWagon(railwayWagon);
            passengers -= railwayWagon.PassengersNumber;
        }

        train.ShowWagons();
    }
}

class Direction
{
    private string _departureName;
    private string _arriveName;

    public Direction(string departureName, string arriveName)
    {
        _departureName = departureName;
        _arriveName = arriveName;
    }

    public void ShowDirectionInfo()
    {
        Console.WriteLine($"Создано направление поезда: {_departureName} - {_arriveName}.");
    }
}

class Train
{
    private List<RailwayWagon> _railwayWagons;
    private Direction _direction;
    
    public Train(Direction direction, List<RailwayWagon> railwayWagons)
    {
        _railwayWagons = railwayWagons;
        _direction = direction;
    }

    public void ShowWagons()
    {
        for (int i = 0; i < _railwayWagons.Count; i++)
        {
            Console.WriteLine($"Вагон {i + 1}: заполнено {_railwayWagons[i].PassengersNumber} мест из {_railwayWagons[i].Capacity}");
        }
    }

    public void AddWagon(RailwayWagon railwayWagon)
    {
        _railwayWagons.Add(railwayWagon);
    } 
}

class RailwayWagon
{
    private int _minCapacity = 12;
    private int _maxCapacity = 54;

    public RailwayWagon()
    {
        Capacity = NumberRandomizer.GenerateRandomNumber(_minCapacity, _maxCapacity);
    }

    public int Capacity { get; private set; }
    public int PassengersNumber { get; private set; }

    public void TakeSeats(int passengers)
    {
        PassengersNumber = Math.Min(passengers, Capacity);
    }
}

class NumberRandomizer
{
    private static Random _random = new Random();

    public static int GenerateRandomNumber(int min, int max)
    {
        return _random.Next(min, max);
    }
}
