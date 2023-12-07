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
        private List<Detail> _details;

        private Queue<Car> _cars;

        private Stock _stock;

        private int _money;

        public CarService(List<Detail> details, Queue<Car> cars)
        {
            _details = details;

            _cars = cars;

            _stock = new Stock(details);
        }

        public void Work()
        {
            while (_cars.Count > 0)
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

            if (TryChooseDetail(brokenDetail, out selectedDetail) && _stock.HaveDetail(selectedDetail))
            {
                ShowTotalPrice(selectedDetail);

                _stock.SpendDetail(selectedDetail);

                Repair(brokenDetail, selectedDetail, car);

                if (car.HasBrokenDetails() == false)
                {
                    Console.WriteLine("Починка прошла успешно!");
                    GetMoney(selectedDetail);
                }
                else
                {
                    Console.WriteLine("Вы заменили не ту деталь. Придётся заплатить штраф.");
                    PayFine();
                }
            }
        }

        private void GetMoney(Detail detail)
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
                    if (_stock.HaveDetail(detail))
                    {
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("На складе нет нужной детали. Плати компенсацию.");
                        PayCompensation();
                    }
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
        private Dictionary<Detail, int> _boxes;

        public Stock(List<Detail> details)
        {
            _boxes = Fill(details);
        }

        public void SpendDetail(Detail detail)
        {
            if (_boxes[detail] > 0)
            {
                _boxes[detail]--;
            }
        }

        public bool HaveDetail(Detail detail)
        {
            if (_boxes[detail] > 0)
            {
                return true;
            }

            return false;
        }

        public void ShowInfo()
        {
            int positionX = 75;
            int positionY = 0;
            int number = 0;

            Console.SetCursorPosition(positionX, positionY);
            Console.WriteLine("Склад деталей:");

            foreach (var box in _boxes)
            {
                positionY++;
                number++;

                Console.SetCursorPosition(positionX, positionY);

                Console.WriteLine($"{number}) Деталь: {box.Key.Name}. Осталось {box.Value} шт.");
            }
        }

        public bool TryGetDetail(string detailName, out Detail detail)
        {
            detail = null;

            foreach (var box in _boxes)
            {
                if (box.Key.Name.ToLower() == detailName.ToLower())
                {
                    detail = box.Key;

                    return true;
                }
            }

            return false;
        }

        private Dictionary<Detail, int> Fill(List<Detail> details)
        {
            Dictionary<Detail, int> boxes = new Dictionary<Detail, int>();

            int randomMinValue = 1;
            int randomMaxValue = 6;

            for (int i = 0; i < details.Count; i++)
            {
                boxes.Add(details[i], UserUtils.GetRandomNumber(randomMinValue, randomMaxValue));
            }

            return boxes;
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
                if (detail is Engine && detail.IsBroken)
                {
                    Console.WriteLine(BreakingsDescription.EngineBreaks);
                }
                else if (detail is Brakes && detail.IsBroken)
                {
                    Console.WriteLine(BreakingsDescription.BrakesBreaks);
                }
                else if (detail is Rudder && detail.IsBroken)
                {
                    Console.WriteLine(BreakingsDescription.RudderBreaks);
                }
                else if (detail is Suspension && detail.IsBroken)
                {
                    Console.WriteLine(BreakingsDescription.SuspensionBreaks);
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

    public class BreakingsDescription
    {
        public const string EngineBreaks = "Машина не заводится";
        public const string BrakesBreaks = "Машину заносит на поворотах";
        public const string RudderBreaks = "Машина не поворачивает";
        public const string SuspensionBreaks = "При езде слышны стуки снизу";
    }
}
