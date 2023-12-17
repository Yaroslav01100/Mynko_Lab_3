using System;
using System.Collections.Generic;

namespace Lab_3_task_3
{
public class Road
{
    public double Lengthd { get; set; }
    public double Width { get; set; }
    public int NumberofLanes { get; set; }
    public int Trafficlevel { get; set; }
}

public class Vehicle : IDriveable
{
    public double Speed { get; set; }
    public double Size { get; set; }
    public string Type { get; set; }

    public void Move()
    {
        Console.WriteLine($"Транспортний засіб типу {Type} рухається зі швидкістю {Speed} км/год.");
    }

    public void Stop()
    {
        Console.WriteLine($"Транспортний засіб типу {Type} зупинився.");
    }
}

public interface IDriveable
{
    void Move();
    void Stop();
}

public class VehicleMove
{
    public void VehicleMoveOnRoad(Vehicle засіб, Road road)
    {
        Console.WriteLine($"Транспортний засіб типу {засіб.Type} рухається на дорозі довжиною {road.Lengthd} км зі швидкістю {засіб.Speed} км/год.");

    }
}

public class Simulation
{
    public List<Vehicle> Vehicles { get; set; }

    public Simulation(List<Vehicle> vehicles)
    {
        Vehicles = vehicles;
    }

    public void TrafficModels(Road road)
    {
        Console.WriteLine($"Моделювання трафіку на дорозі. Довжина дороги: {road.Lengthd} км, кількість смуг: {road.NumberofLanes}");
        foreach (var засіб in Vehicles)
        {
            VehicleMove vehicleMove = new VehicleMove();
            vehicleMove.VehicleMoveOnRoad(засіб, road);
        }
    }

    public void TrafficOptimiztion(Road road)
    {
        Console.WriteLine($"Оптимізація трафіку на дорозі. Поточний рівень трафіку: {road.Trafficlevel}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Минко Ярослав");

        var road = new Road { Lengthd = 100, NumberofLanes = 3, Trafficlevel = 4 };
        var car1 = new Vehicle { Type = "Автомобіль", Speed = 90 };
        var car2 = new Vehicle { Type = "Автомобіль", Speed = 80 };
        var bus = new Vehicle { Type = "Автобус", Speed = 60 };

        var vehicles = new List<Vehicle> { car1, car2, bus };
        var simulation = new Simulation(vehicles);

        simulation.TrafficModels(road);
        simulation.TrafficOptimiztion(road);

        Console.ReadLine();
    }
}

}