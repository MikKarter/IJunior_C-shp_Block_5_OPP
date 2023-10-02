using System;
using System.Collections.Generic;
using System.Linq;

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
        private bool _isWorking = true;

        public Room()
        {
            _aquarium = new Aquarium();
        }

        public void SimulationLive()
        {
            Console.WriteLine("Добро пожаловать в симуляцию аквариума");

            while (_isWorking == true)
            {
                Console.WriteLine("Вам доступны следующие действия:");
                Console.WriteLine($"{AddFish} - добавить рыбу");
                Console.WriteLine($"{RemoveFish} - удалить рыбу из аквариума");
                Console.WriteLine($"{SkipTime} - Пропустить немного времени");
                Console.WriteLine($"{ShowInfo} - Проверить аквариум");
                Console.WriteLine($"{Exit} - Остановить симуляцию");
                int.TryParse(Console.ReadLine(), out int number);
                ManagementAquarium(number);
            }
        }

        private void ManagementAquarium(int number)
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
                    _isWorking = false;
                    break;
                default:
                    Console.WriteLine("Такой команды не существует, выберите повторно");
                    break;
            }
        }
    }

    class Aquarium
    {
        private int _maxCountFish = 5;
        private List<Fish> _fishs;
        private List<Fish> _baseFishes;
        private Fish _tepmFish;

        public Aquarium()
        {
            _fishs = new List<Fish>(_maxCountFish);
            _baseFishes = new List<Fish>();
            _baseFishes.Add(new Angelfish());
            _baseFishes.Add(new Cockerel());
            _baseFishes.Add(new Clownfish());
            _baseFishes.Add(new Guppy());
            _baseFishes.Add(new Barbus());
        }

        public void ShowInfo()
        {
            if (_fishs.Count == 0)
            {
                Console.WriteLine("Сейчас в вашем аквариуме пусто!");
            }
            else
            {
                Console.WriteLine($"Сейчас в вашем аквариуме {_fishs.Count} рыб:");

                for (int i = _fishs.Count - 1; i >= 0; i--)
                {
                    Console.Write(i + 1 + ". ");
                    _fishs[i].ShowInfo();                    
                }
            }
        }

        public void AddFish()
        {
            ShowFishList();
            Console.WriteLine("Выберите рыбку чтобы поместить её в аквариум");
            int.TryParse(Console.ReadLine(), out int numberForAdd);

            if (_baseFishes.Count >= numberForAdd && numberForAdd > 0)
            {                
                _tepmFish = _baseFishes.ElementAt(numberForAdd - 1);
                _fishs.Add(_tepmFish.Clone());
            }
            else
            {
                Console.WriteLine("Такой рыбки не существует");
            }
        }

        public void RemoveFish()
        {
            ShowInfo();
            Console.WriteLine("Выберите рыбку чтобы удалить её из аквариума");
            int.TryParse(Console.ReadLine(), out int numberForRemove);

            if (_fishs.Count >= numberForRemove && numberForRemove > 0)
            {
                _fishs.Remove(_fishs[numberForRemove - 1]);
            }
            else
            {
                Console.WriteLine("Такой рыбки не существует");
            }
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
                    _fishs[i].ShowDead();
                    RemoveDead(_fishs[i]);
                }
            }                

            ShowInfo();
            Console.WriteLine("------------------------------------------------------------");
        }

        public void ShowFishList()
        {
            Console.WriteLine("Для выбора доступны:");

            for (int i = 0; i < _baseFishes.Count; i++)
            {
                Console.Write(i + 1 + ". ");
                _baseFishes[i].ShowSpecies();
            }
        }

        private void RemoveDead(Fish fish)
        {
            _fishs.Remove(fish);
        }
    }

    abstract class Fish
    {
        protected float Age;
        protected float MaxAge;

        public Fish()
        {
            Age = CreateAge();
        }

        public string Species { get; protected set; }
        public bool IsAlive => Age <= MaxAge;

        public void AddAge(float age)
        {
            Age += age;
        }

        public abstract Fish Clone();

        protected float CreateAge()
        {
            return UserUtils.GenerateRandomFloatNumber();            
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Рыбка {Species}, возраст - {Age}");
        }

        public void ShowSpecies()
        {
            Console.WriteLine($"Эта рыбка породы {Species}.");
        }

        public void ShowDead()
        {
                Console.WriteLine($"{Species} мертва");
        }
    }

    class Angelfish : Fish
    {
        private int _min = 9;
        private int _max = 12;

        public Angelfish() : base()
        {
            Species = "Скалярия";
            MaxAge = (float)UserUtils.GenerateRandomIntNumber(_min, _max);
        }

        public override Fish Clone()
        {
            return new Angelfish();
        }
    }

    class Cockerel : Fish
    {
        private int _min = 2;
        private int _max = 5;

        public Cockerel() : base()
        {
            Species = "Петушок";            
            MaxAge = (float)UserUtils.GenerateRandomIntNumber(_min, _max);
        }

        public override Fish Clone()
        {
            return new Cockerel();
        }
    }

    class Clownfish : Fish
    {
        private int _min = 10;
        private int _max = 20;

        public Clownfish() : base()
        {
            Species = "Рыба-клоун";            
            MaxAge = (float)UserUtils.GenerateRandomIntNumber(_min, _max);
        }

        public override Fish Clone()
        {
            return new Clownfish();
        }
    }

    class Guppy : Fish
    {
        private int _min = 2;
        private int _max = 4;

        public Guppy() : base()
        {
            Species = "Гуппи";            
            MaxAge = (float)UserUtils.GenerateRandomIntNumber(_min, _max);
        }

        public override Fish Clone()
        {
            return new Guppy();
        }
    }

    class Barbus : Fish
    {
        private int _min = 4;
        private int _max = 5;

        public Barbus() : base()
        {
            Species = "Барбус";
            MaxAge = (float)UserUtils.GenerateRandomIntNumber(_min, _max);
        }

        public override Fish Clone()
        {
            return new Barbus();
        }
    }

    class UserUtils
    {
        private static Random s_random = new Random();        

        public static float GenerateRandomFloatNumber()
        {
            return (float)s_random.NextDouble();
        }

        public static int GenerateRandomIntNumber(int min, int max)
        {
            return (int)s_random.Next(min, max);
        }
    }
}
