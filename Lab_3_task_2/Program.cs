using System;
using System.Collections.Generic;

namespace Lab_3_task_2
{
public class Computer
{
    public string IPAddress { get; set; }
    public int Power { get; set; }
    public string OperatingSystem { get; set; }

    public Computer(string ipAddress, int power, string os)
    {
        IPAddress = ipAddress;
        Power = power;
        OperatingSystem = os;
    }
}

public class Server : Computer, IConnectable
{
    public int MaxConnections { get; set; }
    public List<Computer> ConnectedComputers { get; }

    public Server(string ipAddress, int power, string os, int maxConnections)
        : base(ipAddress, power, os)
    {
        MaxConnections = maxConnections;
        ConnectedComputers = new List<Computer>();
    }

    public void Connect(Computer computer)
    {
        if (ConnectedComputers.Count < MaxConnections)
        {
            ConnectedComputers.Add(computer);
            Console.WriteLine($"Сервер {IPAddress} підключив комп'ютер {computer.IPAddress}.");
        }
        else
        {
            Console.WriteLine($"Помилка: Сервер {IPAddress} досягнув ліміту з'єднань.");
        }
    }

    public void Disconnect(Computer computer)
    {
        if (ConnectedComputers.Contains(computer))
        {
            ConnectedComputers.Remove(computer);
            Console.WriteLine($"Сервер {IPAddress} відключив комп'ютер {computer.IPAddress}.");
        }
        else
        {
            Console.WriteLine($"Помилка: Сервер {IPAddress} не підключений до комп'ютера {computer.IPAddress}.");
        }
    }
}

public class Workstation : Computer, IConnectable
{
    public Workstation(string ipAddress, int power, string os)
        : base(ipAddress, power, os)
    {
    }

    public void Connect(Computer computer)
    {
        Console.WriteLine($"Робоча станція {IPAddress} підключилась до сервера {computer.IPAddress}.");
    }

    public void Disconnect(Computer computer)
    {
        Console.WriteLine($"Робоча станція {IPAddress} відключилась від сервера {computer.IPAddress}.");
    }
}

public class Router : Computer, IConnectable
{
    public List<Computer> ConnectedComputers { get; }

    public Router(string ipAddress, int power, string os)
        : base(ipAddress, power, os)
    {
        ConnectedComputers = new List<Computer>();
    }

    public void Connect(Computer computer)
    {
        ConnectedComputers.Add(computer);
        Console.WriteLine($"Маршрутизатор {IPAddress} підключив комп'ютер {computer.IPAddress}.");
    }

    public void Disconnect(Computer computer)
    {
        if (ConnectedComputers.Contains(computer))
        {
            ConnectedComputers.Remove(computer);
            Console.WriteLine($"Маршрутизатор {IPAddress} відключив комп'ютер {computer.IPAddress}.");
        }
        else
        {
            Console.WriteLine($"Помилка: Маршрутизатор {IPAddress} не підключений до комп'ютера {computer.IPAddress}.");
        }
    }
}

public interface IConnectable
{
    void Connect(Computer computer);
    void Disconnect(Computer computer);
}

public class Network
{
    public List<Computer> Computers { get; }

    public Network()
    {
        Computers = new List<Computer>();
    }

    public void AddComputer(Computer computer)
    {
        Computers.Add(computer);
    }

    public void SimulateNetwork()
    {
        foreach (var computer in Computers)
        {
            if (computer is IConnectable)
            {
                var connectableComputer = (IConnectable)computer;
                if (Computers.Count > 1)
                {
                    var random = new Random();
                    var targetComputer = Computers[random.Next(Computers.Count)];
                    if (targetComputer != computer)
                    {
                        connectableComputer.Connect(targetComputer);
                        connectableComputer.Disconnect(targetComputer);
                    }
                }
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Минко Ярослав");

        Network network = new Network();

        Server server1 = new Server("192.168.0.1", 4, "Windows Server", 3);
        Server server2 = new Server("192.168.0.2", 4, "Windows Server", 2);
        Workstation workstation1 = new Workstation("192.168.0.3", 2, "Windows 10");
        Workstation workstation2 = new Workstation("192.168.0.4", 2, "Windows 10");
        Router router = new Router("192.168.0.5", 2, "Router OS");

        network.AddComputer(server1);
        network.AddComputer(server2);
        network.AddComputer(workstation1);
        network.AddComputer(workstation2);
        network.AddComputer(router);

        network.SimulateNetwork();
    }
}

}