using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace _6.Task_13
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class CarServise
    {
        Stock stock = new Stock();
        private int _cashAccount;


    }

    class Stock
    {
        private List<ListDetails> _factoryNewDetails = new List<ListDetails>();
        private int _fullStackFactoryNewDetails = 0;
        private int _minFullStackFacntoryNewDetails = 2;
        private int _maxFullStackFacntoryNewDetails = 10;

        public Stock()
        {
            _fullStackFactoryNewDetails = UserUtils.GenerateRandomIntNumber(_minFullStackFacntoryNewDetails, _maxFullStackFacntoryNewDetails);

            for (int i=0; i< _fullStackFactoryNewDetails; i++)
            {
                _factoryNewDetails[i].Create();
            }
        }

        public CarDetail GetDetail(CarDetail detail)
        {
            return detail;
        }

    }

    class Client
    {
        private int _maxMoney = 16500;
        private int _minMoney = 1500;
        public Client(Automobile car)
        {
            Car = car;
            Money = UserUtils.GenerateRandomIntNumber(_minMoney, _maxMoney);
        }

        public Automobile Car { get; private set; }
        public int Money { get; private set; }

        public bool TryPay(int priceToPay)
        {
            if (Money < priceToPay)
            {
                return false;
            }

            return true;
        }

        public void BrokeCar(Automobile car)
        {
            car.SelectDetailForBreak();
            car.BreakDetails(car.CountOfBadDetails());
        }
    }

    class Automobile
    {
        private List<CarDetail> _details = new List<CarDetail>();
        private ListDetails _listDetails = new ListDetails();
        private int _brokenDetaisCount;
        private int _badDetailsCount;


        public Automobile()
        {
            _listDetails.Create();
        }

        public void СountOfBrokenDetails()
        {
            _brokenDetaisCount = UserUtils.GenerateRandomIntNumber(0, _details.Count);
        }

        public int CountOfBadDetails()
        {
            for (int i = 0; i < _brokenDetaisCount; i++)
            {
                _badDetailsCount++;
            }

            return _badDetailsCount;
        }

        public void SelectDetailForBreak()
        {
            while (_badDetailsCount < _brokenDetaisCount)
            {
                for (int i = 0; i < _details.Count; i++)
                {
                    if (_details[i].IsBad == false)
                    {
                        if (UserUtils.GenerateRandomIntNumber(0, 1) > 0)
                        {
                            _details[i].WearOutAPart();
                        }
                    }
                }
            }
        }

        public void BreakDetails(int countOfBrokenDetails)
        {
            for (int i = 0; i < _details.Count; i++)
            {
                if (_details[i].IsBad == true)
                {
                    _details[i].Break();
                }
            }
        }
    }

    class ListDetails
    {
        private List<CarDetail> _details = new List<CarDetail>();
        private int _enginePrice = 5000;
        private int _pistonSystemPrice = 3500;
        private int _wheelsPrice = 1600;
        private int _exhausteSystemPrice = 2400;
        private int _transmissionPrice = 4000;

        public ListDetails()
        {
            Create();
        }
        public void Create()
        {
            _details.Add(new CarDetail("Двигатель", _enginePrice));
            _details.Add(new CarDetail("Поршневая система", _pistonSystemPrice));
            _details.Add(new CarDetail("Колёса", _wheelsPrice));
            _details.Add(new CarDetail("Выхлопная система", _exhausteSystemPrice));
            _details.Add(new CarDetail("Трансмиссия", _transmissionPrice));
        }
    }
    class CarDetail
    {
        public CarDetail(string title, int price)
        {
            Title = title;
            Price = Price;
            IsBroken = false;
            IsBad = false;
        }

        public string Title { get; private set; }
        public int Price { get; private set; }
        public bool IsBroken { get; private set; }
        public bool IsBad { get; private set; }

        public void Break()
        {
            IsBroken = true;
        }

        public void WearOutAPart()
        {
            IsBad = true;
        }
    }

    class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomIntNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }
    }
}
