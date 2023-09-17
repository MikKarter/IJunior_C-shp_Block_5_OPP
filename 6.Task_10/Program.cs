using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _6.Task_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Battlefield battlefield = new Battlefield();
            battlefield.ArmyAttack();
        }
    }

    class Battlefield
    {
        private Platoon _platoon1;
        private Platoon _platoon2;
        private int _flagCountryOne = 1;
        private int _flagCountryTwo = 2;


        public Battlefield()
        {
            _platoon1 = new Platoon(_flagCountryOne);
            _platoon2 = new Platoon(_flagCountryTwo);
        }

        public void Battle()
        {
            Console.WriteLine($"У страны {_flagCountryOne} на поле боя присутствуют:");
            _platoon1.ShowArmy();
            Console.WriteLine($"У страны {_flagCountryTwo} на поле боя присутствуют:");
            _platoon2.ShowArmy();
        }

        public void ArmyAttack()
        {
            Battle();
            Console.WriteLine("Начинается сражение!");
            Soldier soldier1 = _platoon1.GetSoldierFromPlatoon();
            Soldier soldier2 = _platoon2.GetSoldierFromPlatoon();

            while (_platoon1.ShowSoldierCount() > 0 && _platoon2.ShowSoldierCount() > 0)
            {
                soldier1.Attack(soldier2);

                if (soldier1.IsAlive == false)
                {
                    Console.WriteLine($"Солдат страны {_flagCountryOne} - проиграл!");
                    _platoon1.RemoveSoldierFromPlatoon(soldier1);
                    _platoon1.GetSoldierFromPlatoon();
                    Console.WriteLine($"У страны {_flagCountryOne} осталось {_platoon1.ShowSoldierCount()} бойцов!");

                    if (_platoon1.ShowSoldierCount() <= 0)
                    {
                        Console.WriteLine($"Страна {_flagCountryOne} проиграла");
                    }
                }

                soldier2.Attack(soldier1);

                if (soldier2.IsAlive == false)
                {
                    Console.WriteLine($"Солдат страны {_flagCountryTwo} - проиграл");
                    _platoon2.RemoveSoldierFromPlatoon(soldier2);
                    _platoon2.GetSoldierFromPlatoon();
                    Console.WriteLine($"У страны {_flagCountryTwo} осталось {_platoon2.ShowSoldierCount()} бойцов!");

                    if (_platoon1.ShowSoldierCount() <= 0)
                    {
                        Console.WriteLine($"Страна {_flagCountryTwo} проиграла");
                    }
                }
            }

            Console.WriteLine("Сражение окончено!");
        }
    }

    class Platoon
    {
        private int _size = 20;
        private int _minRandomValue = 0;
        private int maxRandomValue = 21;
        private int _countShieldbearer;
        private int _countArcher;
        private int _countSwordman;
        private List<Soldier> _soldiers;

        public Platoon(int countryFlag)
        {
            _soldiers = new List<Soldier>(_size);
            FormArmy(_size);
            CreateArmy(_countShieldbearer, _countArcher, _countSwordman);
            CountryFlag = countryFlag;
        }

        public int CountSoldiers { get; private set; }
        public int CountryFlag { get; private set; }

        private void FormArmy(int size)
        {
            const int Shieldbearer = 1;
            const int Archer = 2;
            const int Swordman = 3;

            int minValueChoiseSoldier = 1;
            int maxValueChoiseSoldier = 4;

            CountSoldiers = 0;
            _countShieldbearer = 0;
            _countArcher = 0;
            _countSwordman = 0;

            while (CountSoldiers < _size)
            {
                int randomSoldierChoise = UserUtils.GenerateRandomNumber(minValueChoiseSoldier, maxValueChoiseSoldier);

                switch (randomSoldierChoise)
                {
                    case Shieldbearer:
                        _countShieldbearer++;
                        CountSoldiers++;
                        break;
                    case Archer:
                        _countArcher++;
                        CountSoldiers++;
                        break;
                    case Swordman:
                        _countSwordman++;
                        CountSoldiers++;
                        break;
                }
            }
        }

        private void CreateArmy(int _countShieldbearer, int _countArcher, int _countSwordman)
        {
            if (_countArcher + _countShieldbearer + _countSwordman == _size)
            {

                while (_countShieldbearer > 0)
                {
                    _soldiers.Add(new Shieldbearer());
                    _countShieldbearer--;
                }

                while (_countArcher > 0)
                {
                    _soldiers.Add(new Archer());
                    _countArcher--;
                }

                while (_countSwordman > 0)
                {
                    _soldiers.Add(new Swordman());
                    _countSwordman--;
                }
            }
            else Console.WriteLine("Something Wrong");
        }

        public void ShowArmy()
        {
            int swordmanCount = 0;
            int archerCount = 0;
            int shieldbearerCount = 0;

            for (int i = 0; i < _soldiers.Count; i++)
            {
                if (_soldiers[i] is Shieldbearer)
                {
                    shieldbearerCount++;
                }
                else if (_soldiers[i] is Archer)
                {
                    archerCount++;
                }
                else if (_soldiers[i] is Swordman)
                {
                    swordmanCount++;
                }
            }

            Console.WriteLine($"Мечники - {swordmanCount} человек. \n Лучники - {archerCount} человек. \nЩитоносцы - {shieldbearerCount} человек.");
        }

        public void RemoveSoldierFromPlatoon(Soldier soldier)
        {
            _soldiers.Remove(soldier);
        }

        public Soldier GetSoldierFromPlatoon()
        {
            return _soldiers[UserUtils.GenerateRandomNumber(0, _soldiers.Count)];
        }

        public int ShowSoldierCount()
        {
            return _soldiers.Count();
        }

    }

    abstract class Soldier
    {
        protected int Health;
        protected int Armor;
        protected int Damage;

        public Soldier()
        {
            Health = 100;
            Armor = 10;
            Damage = 80;
        }

        public bool IsAlive => Health > 0;

        public void TakeDamage(int Damage)
        {
            Health -= Damage - Armor;
        }

        public abstract Soldier Clone();

        public abstract void Attack(Soldier enemy);
    }

    class Shieldbearer : Soldier
    {
        private int _shieldArmor = 20;
        public Shieldbearer() : base()
        {
            Armor += _shieldArmor;
        }

        public override Soldier Clone()
        {
            return new Shieldbearer();
        }

        public override void Attack(Soldier enemy)
        {
            int damage = Damage;

            enemy.TakeDamage(damage);
        }
    }

    class Archer : Soldier
    {
        private int _arrowDamage = 40;
        public Archer() : base()
        {
            Damage += _arrowDamage;
        }

        public override Soldier Clone()
        {
            return new Archer();
        }

        public override void Attack(Soldier enemy)
        {
            int damage = Damage;

            enemy.TakeDamage(damage);
        }
    }

    class Swordman : Soldier
    {
        private int _extraHealth = 100;
        public Swordman() : base()
        {
            Health += _extraHealth;
        }

        public override Soldier Clone()
        {
            return new Swordman();
        }

        public override void Attack(Soldier enemy)
        {
            int damage = Damage;

            enemy.TakeDamage(damage);
        }
    }

    class UserUtils
    {
        private static Random random = new Random();

        public static int GenerateRandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
