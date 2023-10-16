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

        private void ShowInfo()
        {
            Console.WriteLine($"In Zoo your see {_aviarys.Count} aviarys");
        }

        private void ShowInfo(int index)
        {
            _aviarys[index].ShowInfo();
        }

        private void CalculateAviaryQuantity()
        {
            _aviaryCount = UserUtils.GenerateRandomIntNumber(_minAviaryCount, _maxAviaryCount);
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
                    Console.WriteLine($"Into №{userInput} aviary you see:");
                    ShowInfo(userInput - 1);
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
        private int _minCapacity = 2;
        private int _maxCapacity = 6;

        public Aviary()
        {
            FillTheAnimals(UserUtils.GenerateRandomIntNumber(_minCapacity, _maxCapacity));
        }

        public int Number { get; private set; }

        public void FillTheAnimals(int numberOfAnimals)
        {
            List<Animal> _animalsList = new List<Animal>()
            {
            new Animal("Bear", "Rooar"),
            new Animal("Dog", "Barking"),
            new Animal("Wolf", "Howl"),
            new Animal("Cow", "Moo"),
            };

            for (int i = 0; i < numberOfAnimals; i++)
            {
                Animal tempAnimal;
                tempAnimal = _animalsList[UserUtils.GenerateRandomIntNumber(0, _animalsList.Count)].CLone();
                tempAnimal.DefineGender();
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
        }       

        public void DefineGender()
        {
            List<string> gender = new List<string> 
            { 
                "Male", 
                "Female" 
            };

            _gender = gender[UserUtils.GenerateRandomIntNumber(0,gender.Count)];
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
