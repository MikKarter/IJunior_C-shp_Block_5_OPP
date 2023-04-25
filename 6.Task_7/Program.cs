using System;
using System.Collections.Generic;

namespace _6.Task_7
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class RailwayStantion
    {
        private TicketTradeOffice Cashier = new TicketTradeOffice();

    }

    class RaliwayWagon
    {
        public int NumberOfRaliwayCarriage { get; private set; }
        public int MaxPlace { get; private set; }
        public int FreePlace { get; private set; }

        public RaliwayWagon(int maxPlace)
        {
            MaxPlace = maxPlace;
            FreePlace = MaxPlace;
        }

        public void CreateRailwayWagon()
        {
            Console.WriteLine("How many railway wagon will be on the train?");
            int.TryParse(Console.ReadLine(), out int userInput);
            NumberOfRaliwayCarriage = userInput;
        }

        public void ReduceFreePlace(int purchasedTickets)
        {
            if (FreePlace > purchasedTickets)
            {
                FreePlace = MaxPlace - purchasedTickets;
            }
            else
            {
                Console.WriteLine("Not enough place in this railway wagon!");
            }
        }        
    }

    class Train
    {
        public List<RaliwayWagon> RaliwayCarriages { get; private set; }
        //public Dictionary<string, int> Trains { get; private set; }
        public Train ()
        {
            RaliwayCarriages = new List<RaliwayWagon>();
        }

        public void CreateTrain()
        {

        }
    }

    class Direction
    {
        public string DeparturePlace { get; private set; }
        public string ArrivePlace { get; private set; }

        public Direction(string departurePlace, string arrivePlace)
        {
            DeparturePlace = departurePlace;
            ArrivePlace = arrivePlace;
        }
    }
    class TicketTradeOffice
    {
        private Random _random = new Random();
        private int _ticketSoldMin = 0;
        private int _ticketSoldMax = 51;
        private int _countSoldTicket;

        public int Sell()
        {
            _countSoldTicket = _random.Next(_ticketSoldMin, _ticketSoldMax);
            Console.WriteLine($"Продано {_countSoldTicket} билетов");
            return _countSoldTicket;
        }
    }
}
