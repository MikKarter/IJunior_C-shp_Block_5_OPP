using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace _6.Task_13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Factory factory = new Factory();

            factory.CreateCars();
            factory.Start();
        }
    }
    class Factory
    {
        private DetailCreator _creator;

        private Queue<Car> _cars;

        private CarService _carService;

        public Factory()
        {
            _creator = new DetailCreator();
            _cars = new Queue<Car>();
            _carService = new CarService(_creator.CreateDetails(), _cars);
        }

        public void CreateCars()
        {
            int count = 5;

            for (int i = 0; i < count; i++)
            {
                _cars.Enqueue(new Car(_creator.CreateDetails()));
            }
        }

        public void Start()
        {
            _carService.Work();
        }
    }

    class CarService
    {
        private Queue<Car> _cars;

        private Stock _stock;

        private int _money;

        public CarService(List<Detail> details, Queue<Car> cars)
        {
            _cars = cars;

            _stock = new Stock(details);
        }

        public void Work()
        {
            while (_cars.Count > 0 && _money > 0)
            {
                _stock.ShowInfo();
                ShowMoney();

                ServeCar();

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ServeCar()
        {
            Detail brokenDetail;
            Detail selectedDetail;

            Car car = _cars.Dequeue();

            if (car.TryGetBrokenDetail(out brokenDetail))
            {
                car.ReportBreaking();
            }

            if (TryChooseDetail(brokenDetail, out selectedDetail))
            {
                ShowTotalPrice(selectedDetail);

                _stock.SpendDetail(selectedDetail);

                Repair(brokenDetail, selectedDetail, car);

                if (car.HasBrokenDetails() == false)
                {
                    Console.WriteLine("Починка прошла успешно!");
                    AddMoney(selectedDetail);
                }
                else
                {
                    Console.WriteLine("Вы заменили не ту деталь. Придётся заплатить штраф.");
                    PayFine();
                }
            }
        }

        private void AddMoney(Detail detail)
        {
            _money += detail.TotalPrice;
        }

        private void ShowMoney()
        {
            int positionX = 75;
            int positionY = 7;

            Console.SetCursorPosition(positionX, positionY);
            Console.WriteLine($"Выручка автосервиса: {_money}");
        }

        private void ShowTotalPrice(Detail detail)
        {
            Console.WriteLine($"Цена за починку: {detail.TotalPrice} руб.");
        }

        private bool TryChooseDetail(Detail brokenDetail, out Detail detail)
        {
            string userInput;

            Console.WriteLine("Выберите деталь, которую нужно заменить.");

            userInput = Console.ReadLine();

            if (_stock.TryGetDetail(userInput, out detail))
            {
                if (brokenDetail.Name == detail.Name)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Вы выбрали не ту деталь. Платите штраф.");
                    PayFine();
                }
            }
            else
            {
                Console.WriteLine("Такой детали нет на складе.");
                Console.WriteLine("На складе нет нужной детали. Плати компенсацию.");
                PayCompensation();
            }

            return false;
        }

        private void Repair(Detail brokenDetail, Detail selectedDetail, Car car)
        {
            car.ReplaceDetail(brokenDetail, selectedDetail);
        }

        private void PayFine()
        {
            int fine = 500;

            _money -= fine;
        }

        private void PayCompensation()
        {
            int compensation = 250;

            _money -= compensation;
        }
    }

    class Stock
    {
        private List<Slot> _boxes;

        public Stock(List<Detail> details)
        {
            _boxes = Fill(details);
        }

        public void SpendDetail(Detail detail)
        {
            foreach (var slot in _boxes)
            {
                if (slot.Detail.Name == detail.Name)
                    slot.Remove();
            }
        }

        public void ShowInfo()
        {
            int positionX = 75;
            int positionY = 0;
            int number = 0;

            Console.SetCursorPosition(positionX, positionY);
            Console.WriteLine("Склад деталей:");

            foreach (var slot in _boxes)
            {
                positionY++;
                number++;

                Console.SetCursorPosition(positionX, positionY);

                Console.WriteLine($"{number}) Деталь: {slot.Detail.Name}. Осталось {slot.Count} шт.");
            }
        }

        public bool TryGetDetail(string detailName, out Detail detail)
        {
            detail = null;

            foreach (var slot in _boxes)
            {
                if (slot.Detail.Name.ToLower() == detailName.ToLower())
                {
                    if (slot.HasDetails)
                    {
                        detail = slot.Detail;
                        return true;
                    }
                }
            }

            return false;
        }

        private List<Slot> Fill(List<Detail> details)
        {
            List<Slot> boxes = new List<Slot>();

            int randomMinValue = 1;
            int randomMaxValue = 6;

            for (int i = 0; i < details.Count; i++)
            {
                boxes.Add(new Slot(details[i], UserUtils.GetRandomNumber(randomMinValue, randomMaxValue)));
            }

            return boxes;
        }
    }

    class Slot
    {
        public Slot(Detail detail, int count)
        {
            Detail = detail;
            Count = count;
        }

        public Detail Detail { get; }
        public int Count { get; private set; }
        
        public bool HasDetails => Count > 0;

        public void Add(int amount = 1)
        {
            Count += amount;
        }

        public void Remove(int amount = 1)
        {
            if (Count >= Count - amount)
                Count -= amount;

            if (HasDetails == false)
                Console.WriteLine($"Детали {Detail.Name} кончились");
        }
    }

    class Car
    {
        private List<Detail> _details;

        public Car(List<Detail> details)
        {
            _details = details;

            BreakRandomDetail();
        }

        public void ReportBreaking()
        {
            int positionX = 0;
            int positionY = 0;

            Console.SetCursorPosition(positionX, positionY);

            Console.WriteLine("Описание проблемы клиента:");

            foreach (var detail in _details)
            {
                if (detail.IsBroken)
                {
                    Console.WriteLine(detail.ReasonBrekadown);
                }
            }
        }

        public void ReplaceDetail(Detail brokenDetail, Detail selectedDetail)
        {
            _details.Remove(brokenDetail);
            _details.Add(selectedDetail);
        }

        public bool TryGetBrokenDetail(out Detail brokenDetail)
        {
            brokenDetail = null;

            for (int i = 0; i < _details.Count; i++)
            {
                if (_details[i].IsBroken)
                {
                    brokenDetail = _details[i];

                    return true;
                }
            }

            return false;
        }

        public bool HasBrokenDetails()
        {
            foreach (var detail in _details)
            {
                if (detail.IsBroken)
                {
                    return true;
                }
            }

            return false;
        }

        private void BreakRandomDetail()
        {
            int index = UserUtils.GetRandomNumber(0, _details.Count);

            _details[index].Break();
        }
    }

    abstract class Detail
    {
        public string Name { get; protected set; }
        public int SelfPrice { get; protected set; }
        public int WorkPrice { get; protected set; }
        public bool IsBroken { get; protected set; } = false;
        public string ReasonBrekadown { get; protected set; }
        public int TotalPrice => SelfPrice + WorkPrice;

        public abstract Detail Clone();

        public void Break()
        {
            IsBroken = true;
        }

        public void Repair()
        {
            Console.WriteLine("Деталь заменена на новую.");
        }
    }

    class Engine : Detail
    {
        public Engine()
        {
            Name = "Двигатель";
            SelfPrice = 5000;
            WorkPrice = 3500;
            ReasonBrekadown = "Машина не заводится";
        }

        public override Detail Clone()
        {
            return new Engine();
        }
    }

    class Brakes : Detail
    {
        public Brakes()
        {
            Name = "Тормоза";
            SelfPrice = 3000;
            WorkPrice = 2000;
            ReasonBrekadown = "Машину заносит на поворотах";
        }

        public override Detail Clone()
        {
            return new Brakes();
        }
    }

    class Rudder : Detail
    {
        public Rudder()
        {
            Name = "Руль";
            SelfPrice = 2000;
            WorkPrice = 1000;
            ReasonBrekadown = "Машина не поворачивает";
        }

        public override Detail Clone()
        {
            return new Rudder();
        }
    }

    class Suspension : Detail
    {
        public Suspension()
        {
            Name = "Подвеска";
            SelfPrice = 4000;
            WorkPrice = 2500;
            ReasonBrekadown = "При езде слышны стуки снизу";
        }

        public override Detail Clone()
        {
            return new Suspension();
        }
    }

    class DetailCreator
    {
        public List<Detail> CreateDetails()
        {
            return new List<Detail>
            {
                {new Engine() },
                {new Brakes() },
                {new Rudder() },
                {new Suspension() }
            };
        }
    }

    public class UserUtils
    {
        private static Random s_random = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }
    }
}
