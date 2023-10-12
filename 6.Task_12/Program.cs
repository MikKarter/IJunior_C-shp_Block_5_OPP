using System;
using System.Collections.Generic;

namespace _6.Task_12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();
            zoo.ManageZoo();
        }
    }

    class Zoo
    {
        private int _minAviaryCount = 4;
        private int _maxAviaryCount = 10;
        private List<Aviary> _aviarys = new List<Aviary>();
        private int _aviaryCount = 0;

        public Zoo()
        {
            CalculateAviaryQuantity();
        }

        public void ShowInfo()
        {
            Console.WriteLine($"In Zoo your see {_aviarys.Count} aviarys");
        }

        public void ShowInfo(int index)
        {
            _aviarys[index].ShowInfo();
        }

        public void CalculateAviaryQuantity()
        {
            _aviaryCount = UserUtils.GenerateRandomIntNumber(_minAviaryCount,_maxAviaryCount);
        }

        public void ManageZoo()
        {
            bool isOpen = true;

            for (int i = 0; i < _minAviaryCount; i++)
            {
                _aviarys.Add(new Aviary());
            }

            for (int i = _minAviaryCount; i < _aviaryCount; i++)
            {
                _aviarys.Add(new Aviary());
            }

            Console.WriteLine("Welcom to Zoo");

            while (isOpen)
            {
                ShowInfo();
                Console.WriteLine("Please, select to number aviary to get closer");
                int.TryParse(Console.ReadLine(), out int userInput);

                if (userInput <= _aviarys.Count & userInput > 0)
                {
                    ShowInfo(userInput-1);
                }
                else
                {
                    Console.WriteLine("Error! Please select correct number");
                }
            }
        }
    }

    class Aviary
    {
        private List<Animal> _animals = new List<Animal>();
        private int _minCapacity = 1;
        private int _maxCapacity = 6;
        private int _id;

        public Aviary()
        {            
            _id=UserUtils.CreateID();
            FillTheAnimals(UserUtils.GenerateRandomIntNumber(_minCapacity, _maxCapacity));
        }

        public int Number { get; private set; }

        public void FillTheAnimals(int numberOfAnimals)
        {
            int minValue = 1;
            int maxValue = 5;

            for (int i = 0; i < numberOfAnimals; i++)
            {
                _animals.Add(new Animal(UserUtils.GenerateRandomIntNumber(minValue, maxValue)));
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Into this aviary №{_id}:");

            foreach (var animal in _animals)
            {
                Console.WriteLine($"{animal.Name}, voice: {animal.Voice}. Gender - {animal.Gender}");
            }
        }

    }

    class Animal
    {
        private const int Bear = 1;
        private const int Dog = 2;
        private const int Wolf = 3;
        private const int Cow = 4;

        private int _genredMale = 2;
        private int _genderFemale = 0;

        public Animal(int id)
        {
            switch (id)
            {
                case Bear:
                    Name = "Bear";
                    Voice = "Rooar";
                    break;
                case Dog:
                    Name = "Dog";
                    Voice = "Barking";
                    break;
                case Wolf:
                    Name = "Wolf";
                    Voice = "Howl";
                    break;
                case Cow:
                    Name = "Cow";
                    Voice = "Moo";
                    break;
            }           

            if (UserUtils.GenerateRandomIntNumber(_genderFemale,_genredMale) > 0 )
            {
                Gender = "male";
            }
            else
            {
                Gender = "female";
            }
        }

        public string Gender { get; private set; }
        public string Name { get; private set; }
        public string Voice { get; private set; }
    }

    class UserUtils
    {
        private static Random s_random = new Random();
        public static int s_id { get; private set; } = 1;

        public static int GenerateRandomIntNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }

        public static int CreateID()
        {
            return s_id++;
        }
    }
}
