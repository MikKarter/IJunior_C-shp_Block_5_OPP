using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.Task_7
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class RaliwayCarriage
    {
        public int NumberOfPassengers { get; private set; }
        public int MaxPlace{ get; private set; }
        public int FreePlace { get; private set; }

        public RaliwayCarriage (int numberOfPassengers, int maxPlace, int freePlace)
        {
            NumberOfPassengers = numberOfPassengers;
            MaxPlace = maxPlace;
            FreePlace = maxPlace - numberOfPassengers;
        }
    }

    class TicketTrade
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

    class Train
    {        
        public List<RaliwayCarriage> RaliwayCarriages { get; private set; }
        public Dictionary<string, int> Trains { get; private set; }
    }
}
