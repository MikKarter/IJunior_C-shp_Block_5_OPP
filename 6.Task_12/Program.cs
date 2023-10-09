using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public Zoo()
        {
            for (int i = _minAviaryCount; i < UserUtils.GenerateRandomIntNumber(_minAviaryCount, _maxAviaryCount); i++)
            {
                _aviarys.Add(new Aviary());
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine($"In Zoo your see {_aviarys.Count} aviarys");
        }

        public void ShowStatus(int number)
        {
            _aviarys[number].ShowInfo();
        }

        public void ManageZoo()
        {
            bool isOpen = true;

            Console.WriteLine("Welcom to Zoo");


            while (isOpen)
            {
                ShowInfo();
                Console.WriteLine("Please, select to number aviary to get closer");
                int.TryParse(Console.ReadLine(), out int userInput);

                if (userInput <= _aviarys.Count & userInput > 0)
                {
                    ShowStatus(userInput-1);
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

        public Aviary()
        {
            _id++;
            FillTheAnimals(UserUtils.GenerateRandomIntNumber(_minCapacity, _maxCapacity));
        }

        public int _id { get; private set; } = 0;
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
                Console.WriteLine($"{animal.Name}, voice: {animal.Voice}");
            }
        }

    }

    class Animal
    {

        public Animal(int id)
        {
            Volier = UserUtils.CreateID();

            switch (id)
            {
                case 1:
                    Name = "Bear";
                    Voice = "Rooar";
                    break;
                case 2:
                    Name = "Dog";
                    Voice = "Barking";
                    break;
                case 3:
                    Name = "Wolf";
                    Voice = "Howl";
                    break;
                case 4:
                    Name = "Cow";
                    Voice = "Moo";
                    break;
            }
        }

        public int Volier { get; private set; }
        public string Name { get; private set; }
        public string Voice { get; private set; }

    }



    class UserUtils
    {
        private static Random s_random = new Random();
        public static int s_id { get; private set; }

        public static int GenerateRandomIntNumber(int min, int max)
        {
            return (int)s_random.Next(min, max);
        }

        public static int CreateID()
        {
            return s_id++;
        }
    }
}
