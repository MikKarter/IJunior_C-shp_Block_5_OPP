using System;
using System.Collections.Generic;

internal class Program
{
    static void Main(string[] args)
    {
        TrainPlanner trainPlanner = new TrainPlanner();

        while (true)
        {
            trainPlanner.ShowTrainInfo();

            int choice = trainPlanner.GetChoice();

            switch (choice)
            {
                case 1:
                    trainPlanner.CreateDirection();
                    break;
                case 2:
                    trainPlanner.SellTickets();
                    break;
                case 3:
                    trainPlanner.CreateTrain();
                    break;
                case 4:
                    trainPlanner.SendTrain();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неправильный выбор. Пожалуйста, выберите действие от 1 до 5.");
                    break;
            }

            Console.WriteLine();
        }
    }

    class TrainPlanner
    {
        string direction;
        int passengerCount;
        int trainCapacity;
        bool trainCreated;
        bool trainSent;

        public int GetChoice()
        {
            Console.Write("Выберите действие: ");
            string input = Console.ReadLine();
            int choice;
            int.TryParse(input, out choice);
            return choice;
        }

        public void CreateDirection()
        {
            Console.Write("Введите направление: ");
            direction = Console.ReadLine();
            Console.WriteLine($"Направление '{direction}' успешно создано.");
            ResetState();
        }

        public void SellTickets()
        {
            if (string.IsNullOrEmpty(direction))
            {
                Console.WriteLine("Сначала создайте направление.");
                return;
            }

            Random random = new Random();
            passengerCount = random.Next(1, 100);
            Console.WriteLine($"Продано {passengerCount} билетов на направление '{direction}'.");
        }

        public void CreateTrain()
        {
            if (string.IsNullOrEmpty(direction))
            {
                Console.WriteLine("Сначала создайте направление.");
                return;
            }

            if (passengerCount == 0)
            {
                Console.WriteLine("Сначала продайте билеты.");
                return;
            }

            Console.Write("Введите вместимость поезда: ");
            string input = Console.ReadLine();
            int.TryParse(input, out trainCapacity);

            if (trainCapacity <= 0)
            {
                Console.WriteLine("Неправильная вместимость. Вместимость поезда должна быть положительным числом.");
                return;
            }

            if (trainCapacity >= passengerCount)
            {
                Console.WriteLine("Поезд сформирован успешно.");
                trainCreated = true;
            }
            else
            {
                Console.WriteLine("Вместимость поезда недостаточна для перевозки всех пассажиров. Увеличьте вместимость или продайте меньше билетов.");
            }
        }

        public void SendTrain()
        {
            if (string.IsNullOrEmpty(direction))
            {
                Console.WriteLine("Сначала создайте направление.");
                return;
            }

            if (passengerCount == 0)
            {
                Console.WriteLine("Сначала продайте билеты.");
                return;
            }

            if (!trainCreated)
            {
                Console.WriteLine("Сначала сформируйте поезд.");
                return;
            }

            Console.WriteLine($"Поезд на направлении '{direction}' успешно отправлен с {trainCapacity} вагонами.");
            ResetState();
        }

        public void ResetState()
        {
            passengerCount = 0;
            trainCapacity = 0;
            trainCreated = false;
            trainSent = false;
        }

        public void ShowTrainInfo()
        {
            Console.WriteLine("=== Информация о следующем отправлении:");
            Console.WriteLine($"Направление - {direction}");
            Console.WriteLine($"Продано билетов - {passengerCount}");
            Console.Write($"Поезд готов к отправлению? - ");

            if (trainCreated)
            {
                Console.WriteLine("Да");
            }
            else
            {
                Console.WriteLine("Нет");
            }

            Console.WriteLine("=== Планировщик поезда ===");
            Console.WriteLine("1. Создать направление");
            Console.WriteLine("2. Продать билеты");
            Console.WriteLine("3. Сформировать поезд");
            Console.WriteLine("4. Отправить поезд");
            Console.WriteLine("5. Выйти");
            Console.WriteLine("==========================");
        }
    }
}
