using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

internal class Program
{
    static void Main(string[] args)
    {
        Arena arena = new Arena();

        arena.StartFight();
    }
}

class Arena
{
    private List<Fighter> _fighters = new List<Fighter>();
    private Fighter _fighter1;
    private Fighter _fighter2;

    public Arena()
    {
        CreateFightersList();
    }

    public void ShowFightersInfo()
    {
        int i = 0;

        foreach (Fighter fighter in _fighters)
        {
            i++;
            Console.Write($"{i} ");
            fighter.ShowFighterInfo();
        }
    }

    public void CreateFightersList()
    {
        _fighters = new List<Fighter>()
        {
         new Warrior("Warrior", 100, 10, 5),
         new Archer("Archer", 80, 15, 2),
         new Mage("Mage", 60, 20, 1, 100),
         new Vampire("Vampire", 120, 8, 8),
         new Thief("Thief", 70, 12, 3)
        };
    }

    public void StartFight()
    {
        Console.WriteLine("Доступные бойцы:");

        ShowFightersInfo();

        Console.WriteLine("Выберите двух бойцов для сражения (введите номера):");

        _fighter1 = ChoiceFighter();
        _fighter2 = ChoiceFighter();

        FightProcess(_fighter1, _fighter2);
    }

    public void FightProcess(Fighter fighter1, Fighter fighter2)
    {
        Console.WriteLine($"Бойцы {fighter1.Name} и {fighter2.Name} начинают сражение!");

        while (fighter1.IsAlive == true && fighter2.IsAlive == true)
        {
            fighter1.Attack(fighter2);
            
            if (fighter2.IsAlive == false)
            {
                break;
            }

            fighter2.Attack(fighter1);
        }

        Console.WriteLine($"Сражение окончено!");
        Console.WriteLine($"Боец номер 1 - {fighter1.Name} {(fighter1.IsAlive ? "победил" : "проиграл")}");
        Console.WriteLine($"Боец номер 2 - {fighter2.Name} {(fighter2.IsAlive ? "победил" : "проиграл")}");
    }

    private Fighter ChoiceFighter()
    {
        CreateFightersList();

        Fighter fighter = null;

        while (fighter == null)
        {
            int.TryParse(Console.ReadLine(), out int index);

            if (index > 0 && index <= _fighters.Count)
            {
                fighter = _fighters.ElementAt(index - 1);
            }
            else
            {
                Console.WriteLine("Некорректный номер бойца. Повторите ввод:");
            }
        }

        return fighter;
    }
}

abstract class Fighter
{
    public string Name { get; protected set; }
    public int Health { get; protected set; }
    public int AttackPower { get; protected set; }
    public int Armor { get; protected set; }
    public bool IsAlive { get; protected set; } = true;

    public Fighter(string name, int health, int attackPower, int armor)
    {
        Name = name;
        Health = health;
        AttackPower = attackPower;
        Armor = armor;
    }

    public abstract void Attack(Fighter enemy);

    public void TakeDamage(int damage)
    {
        Health -= (damage - Armor);
        
        if (Health <= 0)
        {
            IsAlive = false;
        }
    }

    public virtual void ShowFighterInfo()
    {
        Console.WriteLine($"{Name}:\n Health:{Health}\n Armor:{Armor}\n Attack Power:{AttackPower}\n");
    }
}

class Warrior : Fighter
{
    private int attackCount = 0;

    public Warrior(string name, int health, int attackPower, int armor) : base(name, health, attackPower, armor)
    {
    }

    public override void Attack(Fighter enemy)
    {
        attackCount++;
        int damage = AttackPower;
        int DamageMultiplier = 5;

        if (GetCriticalDamage())
        {
            damage *= DamageMultiplier;
        }

        enemy.TakeDamage(damage);
        Console.WriteLine($"{Name} наносит {enemy.Name} критический удар - {damage} урона. У {enemy.Name} осталось {enemy.Health} здоровья");
    }

    public bool GetCriticalDamage()
    {
        Random randomCritChance = new Random();
        int minRandomValue = 1;
        int maxRandomValue = 5;
        int rageTreshold = 3;


        if (randomCritChance.Next(minRandomValue, maxRandomValue) > rageTreshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

class Archer : Fighter
{
    private Random random = new Random();

    public Archer(string name, int health, int attackPower, int armor) : base(name, health, attackPower, armor)
    {
    }

    public override void Attack(Fighter enemy)
    {
        int damage = AttackPower;
        int minRandomValue = 1;
        int manRandomValue = 101;
        int agilityTreshold = 30;

        if (random.Next(minRandomValue, manRandomValue) <= agilityTreshold)
        {
            Console.WriteLine($"{Name} увернулся от атаки {enemy.Name}!");
        }
        else
        {
            enemy.TakeDamage(damage);
            Console.WriteLine($"{Name} атаковал {enemy.Name} и нанес {damage} урона.");
        }
    }
}

class Mage : Fighter
{
    private int _mana;

    public Mage(string name, int health, int attackPower, int armor, int mana) : base(name, health, attackPower, armor)
    {
        _mana = mana;
    }

    public override void Attack(Fighter enemy)
    {
        int damageMultiplier = 2;
        int spellFireballCost = 50;

        if (_mana >= spellFireballCost)
        {
            int damage = AttackPower * damageMultiplier;
            enemy.TakeDamage(damage);
            Console.WriteLine($"{Name} использовал огненный шар против {enemy.Name} и нанес {damage} урона.");
            _mana -= spellFireballCost;
        }
        else
        {
            int damage = AttackPower;
            enemy.TakeDamage(damage);
            Console.WriteLine($"{Name} атаковал {enemy.Name} и нанес {damage} урона.");
        }
    }

    public override void ShowFighterInfo()
    {
        Console.WriteLine($"{Name}:\n Health:{Health}\n Armor:{Armor}\n Attack Power:{AttackPower}\n Mana:{_mana}\n");
    }
}

class Vampire : Fighter
{
    public Vampire(string name, int health, int attackPower, int armor) : base(name, health, attackPower, armor)
    {
    }

    public override void Attack(Fighter enemy)
    {
        int damage = AttackPower;
        int healingRatio = 4;

        enemy.TakeDamage(damage);
        int healing = damage / healingRatio;
        Health += healing;
        Console.WriteLine($"{Name} атаковал {enemy.Name}, нанеся {damage} урона и исцелив себя на {healing} единиц здоровья.");
    }
}

class Thief : Fighter
{
    private Random random = new Random();

    public Thief(string name, int health, int attackPower, int armor) : base(name, health, attackPower, armor)
    {
    }

    public override void Attack(Fighter enemy)
    {
        int damage = AttackPower;
        int damageMultiplier = 10;
        int RageTreshold = 35;
        int hitThreshold = 28;
        int minRandomValue = 1;
        int maxRandomValue = 101;

        if (random.Next(minRandomValue, maxRandomValue) >= RageTreshold)
        {
            Console.WriteLine($"{Name} метнул кинжал в {enemy.Name}!");
            damage = AttackPower + damageMultiplier;

            if (random.Next(minRandomValue, maxRandomValue) >= hitThreshold)
            {
                Console.WriteLine($"{Name} попал и нанёс {damage} урона!");
            }
            else
            {
                Console.WriteLine($"{Name} промазал!");
            }
        }
        else
        {
            enemy.TakeDamage(damage);
            Console.WriteLine($"{Name} атаковал {enemy.Name} и нанес {damage} урона.");
        }
    }
}
