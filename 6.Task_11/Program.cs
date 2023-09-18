using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _6.Task_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Room room = new Room();
            room.SimulationLive();
        }
    }

    class Room
    {
        private const int AddFish = 1;
        private const int RemoveFish = 2;
        private const int SkipTime = 3;
        private const int ShowInfo = 4;
        private const int Exit = 5;

        private Aquarium _aquarium = new Aquarium();
        private bool isWorking = true;

        public Room()
        {
            _aquarium = new Aquarium();
        }

        private void AquariumManagement(int number)
        {
            switch (number)
            {
                case AddFish:
                    _aquarium.AddFish();
                    break;
                case RemoveFish:
                    _aquarium.RemoveFish();
                    break;
                case SkipTime:
                    _aquarium.SkipTime();
                    break;
                case ShowInfo:
                    _aquarium.ShowInfo();
                    break;
                case Exit:
                    StoppedSimulation(ref isWorking);
                    break;
                default:
                    Console.WriteLine("Такой команды не существует, выберите повторно");
                    break;
            }
        }

        public void SimulationLive()
        {
            Console.WriteLine("Добро пожаловать в симуляцию аквариума");

            while (isWorking == true)
            {
                Console.WriteLine("Вам доступны следующие действия:");
                Console.WriteLine($"{AddFish} - добавить рыбу");
                Console.WriteLine($"{RemoveFish} - удалить рыбу из аквариума");
                Console.WriteLine($"{SkipTime} - Пропустить немного времени");
                Console.WriteLine($"{ShowInfo} - Проверить аквариум");
                Console.WriteLine($"{Exit} - Остановить симуляцию");
                int.TryParse(Console.ReadLine(), out int number);
                AquariumManagement(number);
            }
        }

        private void StoppedSimulation(ref bool isWoking)
        {
            isWoking = false;
        }
    }

    class Aquarium
    {
        private int _maxCountFish = 5;
        private List<Fish> _fishs;
        private List<Fish> _fishList;

        public Aquarium()
        {
            _fishs = new List<Fish>(_maxCountFish);
            _fishList = new List<Fish>();
            _fishList.Add(new Angelfish());
            _fishList.Add(new Cockerel());
            _fishList.Add(new Clownfish());
            _fishList.Add(new Guppy());
            _fishList.Add(new Barbus());
        }

        public void ShowInfo()
        {
            int tempNumber = _fishList.Count;

            if (_fishs.Count == 0)
            {
                Console.WriteLine("Сейчас в вашем аквариуме пусто!");
            }
            else
            {
                Console.WriteLine($"Сейчас в вашем аквариуме {_fishs.Count} рыб:");

                for (int i = _fishs.Count-1; i >= 0; i--)
                {
                    Console.Write(i+1 + ". ");
                    _fishs[i].ShowInfo();
                }
            }
        }

        public void AddFish()
        {
            ShowFishList();
            Console.WriteLine("Выберите рыбку чтобы поместить её в аквариум");
            int.TryParse(Console.ReadLine(), out int numberForAdd);
            _fishs.Add(_fishList[numberForAdd - 1]);
        }

        public void RemoveFish()
        {
            ShowInfo();
            Console.WriteLine("Выберите рыбку чтобы удалить её из аквариума");
            int.TryParse(Console.ReadLine(), out int numberForRemove);
            _fishs.Remove(_fishs[numberForRemove-1]);
        }

        public void SkipTime()
        {

            Console.WriteLine("спустя немного времени...");

            foreach (Fish fish in _fishs)
            {
                fish.AddAge(UserUtils.GenerateRandomFloatNumber());
            }

            for (int i = _fishs.Count - 1; i >= 0; i--)
            {
                if (_fishs[i].IsAlive == false)
                {
                    Console.WriteLine($"Рыбка {_fishs[i].Species} умерла");
                }

                _fishs.Remove(_fishs[i]);
            }

            ShowInfo();
            Console.WriteLine("------------------------------------------------------------");


        }

        public void ShowFishList()
        {
            Console.WriteLine("Для выбора доступны:");

            for (int i = 0; i < _fishList.Count; i++)
            {
                Console.Write(i + 1 + ". ");
                _fishList[i].ShowSpecies();
            }
        }
    }

    abstract class Fish
    {
        protected float Age;
        protected float MaxAge;
        public bool IsAlive => Age > MaxAge;

        public Fish()
        {
            Age = 0.0f;
        }

        public string Species { get; protected set; }

        public void AddAge(float age)
        {
            Age += age;
        }

        protected float CreateAge()
        {
            Age += UserUtils.GenerateRandomFloatNumber();
            return Age;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Рыбка {Species}, возраст - {Age}");
        }

        public abstract void ShowSpecies();
    }

    class Angelfish : Fish
    {
        private int _min = 9;
        private int _max = 12;

        public Angelfish() : base()
        {
            Species = "Скалярия";
            Age = CreateAge();
            MaxAge = (float)UserUtils.GenerateRandomIntNumber(_min, _max);
        }

        public override void ShowSpecies()
        {
            Console.WriteLine($"Эта рыбка породы {Species}.");
        }
    }

    class Cockerel : Fish
    {
        private int _min = 2;
        private int _max = 5;

        public Cockerel() : base()
        {
            Species = "Петушок";
            Age = CreateAge();
            MaxAge = (float)UserUtils.GenerateRandomIntNumber(_min, _max);
        }

        public override void ShowSpecies()
        {
            Console.WriteLine($"Эта рыбка породы {Species}.");
        }
    }

    class Clownfish : Fish
    {
        private int _min = 10;
        private int _max = 20;

        public Clownfish() : base()
        {
            Species = "Рыба-клоун";
            Age = CreateAge();
            MaxAge = (float)UserUtils.GenerateRandomIntNumber(_min, _max);
        }

        public override void ShowSpecies()
        {
            Console.WriteLine($"Эта рыбка породы {Species}.");
        }
    }

    class Guppy : Fish
    {
        private int _min = 2;
        private int _max = 4;

        public Guppy() : base()
        {
            Species = "Гуппи";
            Age = CreateAge();
            MaxAge = (float)UserUtils.GenerateRandomIntNumber(_min, _max);
        }

        public override void ShowSpecies()
        {
            Console.WriteLine($"Эта рыбка породы {Species}.");
        }
    }

    class Barbus : Fish
    {
        private int _min = 4;
        private int _max = 5;

        public Barbus() : base()
        {
            Species = "Барбус";
            Age = CreateAge();
            MaxAge = (float)UserUtils.GenerateRandomIntNumber(_min, _max);
        }

        public override void ShowSpecies()
        {
            Console.WriteLine($"Эта рыбка породы {Species}.");
        }
    }

    class UserUtils
    {
        private static Random randomFloat = new Random();
        private static Random randomInt = new Random();

        public static float GenerateRandomFloatNumber()
        {
            return (float)randomFloat.NextDouble();
        }

        public static int GenerateRandomIntNumber(int min, int max)
        {
            return (int)randomInt.Next(min, max);
        }
    }





}
