using System;
using System.Collections.Generic;
using System.Globalization;

namespace _6.Task_12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();
            zoo.Manage();
        }
    }

    class Zoo
    {

        private List<Aviary> _aviarys = new List<Aviary>();
        private int _aviaryCount = 0;

        public Zoo()
        {
            int _minAviaryCount = 4;
            int _maxAviaryCount = 10;
            _aviaryCount = UserUtils.GenerateRandomIntNumber(_minAviaryCount, _maxAviaryCount);            
        }

        public void Manage()
        {
            bool isOpen = true;            

            for (int i = 0; i < _aviaryCount; i++)
            {
                _aviarys.Add(new Aviary());
            }

            Console.WriteLine("Welcom to Zoo");

            while (isOpen)
            {
                ShowAviarysQuantity();
                Console.WriteLine("Please, select to number aviary to get closer");
                int.TryParse(Console.ReadLine(), out int userInput);

                if (userInput <= _aviarys.Count & userInput > 0)
                {
                    Console.WriteLine($"Into №{userInput} aviary you see:");
                    ShowAnimalInfo(userInput - 1);
                }
                else
                {
                    Console.WriteLine("Error! Please select correct number");
                }
            }
        }

        private void ShowAviarysQuantity()
        {
            Console.WriteLine($"In Zoo your see {_aviarys.Count} aviarys");
        }

        private void ShowAnimalInfo(int index)
        {
            _aviarys[index].ShowInfo();
        }
    }

    class Aviary
    {
        private List<Animal> _animals = new List<Animal>();
        private int _minCapacity = 2;
        private int _maxCapacity = 6;

        public Aviary()
        {
            FillAnimals(UserUtils.GenerateRandomIntNumber(_minCapacity, _maxCapacity));
        }

        public int Number { get; private set; }

        public void FillAnimals(int numberOfAnimals)
        {
            List<Animal> _animalsList = new List<Animal>()
            {
            new Animal("Bear", "Rooar"),
            new Animal("Dog", "Barking"),
            new Animal("Wolf", "Howl"),
            new Animal("Cow", "Moo"),
            };

            int randomIndexAnimal = UserUtils.GenerateRandomIntNumber(0, _animalsList.Count);

            for (int i = 0; i < numberOfAnimals; i++)
            {
                Animal tempAnimal = _animalsList[randomIndexAnimal].CLone();
                _animals.Add(tempAnimal);
            }
        }

        public void ShowInfo()
        {
            foreach (var animal in _animals)
            {
                animal.ShowInfo(animal);
            }
        }
    }

    class Animal
    {
        private string _gender;
        private string _name;
        private string _voice;

        public Animal(string name, string voice)
        {
            _name = name;
            _voice = voice;
            DefineGender();
        }

        public void DefineGender()
        {
            List<string> gender = new List<string>
            {
                "Male",
                "Female"
            };

            _gender = gender[UserUtils.GenerateRandomIntNumber(0, gender.Count)];
        }

        public Animal CLone()
        {
            return new Animal(_name, _voice);
        }

        public void ShowInfo(Animal animal)
        {
            Console.WriteLine($"{animal._name}, voice: {animal._voice}. Gender - {animal._gender}");
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
