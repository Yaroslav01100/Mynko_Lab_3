using System;

namespace Lab_3_task_1
{
using System;
using System.Collections.Generic;


public class LivingOrganism
{
    public double Energy { get; set; }
    public int Age { get; set; }
    public double Size { get; set; }

    public LivingOrganism(double energy, int age, double size)
    {
        Energy = energy;
        Age = age;
        Size = size;
    }
}


public class Animal : LivingOrganism, IReproducible, IPredator
{
    public string Species { get; set; }
    public int OffspringCount { get; set; }

    public Animal(double energy, int age, double size, string species, int offspringCount)
        : base(energy, age, size)
    {
        Species = species;
        OffspringCount = offspringCount;
    }

    public void Reproduce()
    {
        Console.WriteLine($"{Species} тварина розмножується.");
    }

    public void Hunt(LivingOrganism prey)
    {
        Console.WriteLine($"{Species} тварина полює на {prey.GetType().Name}.");
    }
}


public class Plant : LivingOrganism, IReproducible
{
    public string Type { get; set; }

    public Plant(double energy, int age, double size, string type)
        : base(energy, age, size)
    {
        Type = type;
    }

    public void Reproduce()
    {
        Console.WriteLine($"{Type} рослина розмножується.");
    }
}


public class Microorganism : LivingOrganism, IReproducible
{
    public string MicrobeType { get; set; }

    public Microorganism(double energy, int age, double size, string microbeType)
        : base(energy, age, size)
    {
        MicrobeType = microbeType;
    }

    public void Reproduce()
    {
        Console.WriteLine($"{MicrobeType} мікроорганізм розмножується.");
    }
}


public interface IReproducible
{
    void Reproduce();
}


public interface IPredator
{
    void Hunt(LivingOrganism prey);
}


public class Ecosystem
{
    public List<LivingOrganism> Organisms { get; set; }

    public Ecosystem()
    {
        Organisms = new List<LivingOrganism>();
    }

    public void AddOrganism(LivingOrganism organism)
    {
        Organisms.Add(organism);
    }

    public void SimulateEcosystem()
    {
        foreach (var organism in Organisms)
        {
            Console.WriteLine($"Характеристики {organism.GetType().Name}:");
            Console.WriteLine($"Енергія: {organism.Energy}");
            Console.WriteLine($"Вік: {organism.Age}");
            Console.WriteLine($"Розмір: {organism.Size}");

            if (organism is IReproducible)
            {
                ((IReproducible)organism).Reproduce();
            }

            if (organism is IPredator)
            {
                foreach (var prey in Organisms)
                {
                    if (prey != organism)
                    {
                        ((IPredator)organism).Hunt(prey);
                    }
                }
            }

            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Минко Ярослав");

        Ecosystem ecosystem = new Ecosystem();

        Animal lion = new Animal(100, 5, 2, "Лев", 2);
        Animal zebra = new Animal(50, 3, 1, "Зебра", 1);
        Plant tree = new Plant(20, 10, 5, "Дерево");
        Microorganism bacteria = new Microorganism(5, 1, 0.01, "Бактерія");

        ecosystem.AddOrganism(lion);
        ecosystem.AddOrganism(zebra);
        ecosystem.AddOrganism(tree);
        ecosystem.AddOrganism(bacteria);

        ecosystem.SimulateEcosystem();
    }
}

}