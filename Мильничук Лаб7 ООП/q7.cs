using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

interface ICallable
{
    void Call(string number);
}

interface IBrowsable
{
    void Browse(string site);
}

interface IIdentifiable
{
    string GetId();
}

interface IBirthable
{
    string GetBirthdate();
}

interface IBuyer
{
    void BuyFood();
    int Food { get; }
}

class Smartphone : ICallable, IBrowsable
{
    private List<string> calls = new List<string>();
    private List<string> browsing = new List<string>();

    public void Call(string number)
    {
        if (Regex.IsMatch(number, @"^\d+$"))
        {
            Console.WriteLine($"Calling... {number}");
        }
        else
        {
            Console.WriteLine("Invalid number!");
        }
    }

    public void Browse(string site)
    {
        if (site.Any(char.IsDigit))
        {
            Console.WriteLine("Invalid URL!");
        }
        else
        {
            Console.WriteLine($"Browsing: {site}!");
        }
    }

    public void MakeCalls(List<string> numbers)
    {
        foreach (var number in numbers)
        {
            Call(number);
        }
    }

    public void VisitSites(List<string> sites)
    {
        foreach (var site in sites)
        {
            Browse(site);
        }
    }
}

class Citizen : IIdentifiable, IBirthable, IBuyer
{
    public string Name { get; private set; }
    public int Age { get; private set; }
    private string idNum;
    private string birthdate;
    private int food;

    public Citizen(string name, int age, string idNum, string birthdate)
    {
        Name = name;
        Age = age;
        this.idNum = idNum;
        this.birthdate = birthdate;
        food = 0;
    }

    public string GetId()
    {
        return idNum;
    }

    public string GetBirthdate()
    {
        return birthdate;
    }

    public void BuyFood()
    {
        food += 10;
    }

    public int Food => food;
}

class Robot : IIdentifiable
{
    private string model;
    private string idNum;

    public Robot(string model, string idNum)
    {
        this.model = model;
        this.idNum = idNum;
    }

    public string GetId()
    {
        return idNum;
    }
}

class Pet : IBirthable
{
    private string name;
    private string birthdate;

    public Pet(string name, string birthdate)
    {
        this.name = name;
        this.birthdate = birthdate;
    }

    public string GetBirthdate()
    {
        return birthdate;
    }
}

static void PhoneSystem()
{
    var numbers = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
    var sites = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

    var smartphone = new Smartphone();
    smartphone.MakeCalls(new List<string>(numbers));
    smartphone.VisitSites(new List<string>(sites));
}

static void UtopiaSystem()
{
    var citizens = new List<Citizen>();
    var robots = new List<Robot>();

    while (true)
    {
        string line = Console.ReadLine();
        if (line == "End")
        {
            break;
        }

        var data = line.Split();
        if (data[0] == "Citizen")
        {
            var citizen = new Citizen(data[1], int.Parse(data[2]), data[3], data[4]);
            citizens.Add(citizen);
        }
        else if (data[0] == "Robot")
        {
            var robot = new Robot(data[1], data[2]);
            robots.Add(robot);
        }
        else if (data[0] == "Pet")
        {
            var pet = new Pet(data[1], data[2]);
        }
    }

    string fakeIdSuffix = Console.ReadLine();
    foreach (var citizen in citizens)
    {
        if (citizen.GetId().EndsWith(fakeIdSuffix))
        {
            Console.WriteLine(citizen.GetId());
        }
    }

    string year = Console.ReadLine();
    foreach (var citizen in citizens)
    {
        if (citizen.GetBirthdate().EndsWith(year))
        {
            Console.WriteLine(citizen.GetBirthdate());
        }
    }
}

public static void Main()
{
    PhoneSystem();
    UtopiaSystem();
}
