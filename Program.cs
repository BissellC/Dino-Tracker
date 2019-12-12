using System;
using System.Collections.Generic;
using System.Linq;

namespace Dino_Tracker
{
  class Program
  {
    static List<Dinosaur> Dinos = new List<Dinosaur>();

    static void AddDino()
    {
      var dinoDiet = true;
      Console.WriteLine("What is the name of the dinosaur?");
      var dinoName = Console.ReadLine();
      Console.WriteLine("What is it's diet? Press 'C' for Carnivore or press 'H' for Herbivore");
      var diet = Console.ReadKey();
      if (diet.Key == ConsoleKey.C)
      {
        dinoDiet = true;
      }
      else if (diet.Key == ConsoleKey.H)
      {
        dinoDiet = false;
      }
      Console.WriteLine("");
      Console.WriteLine("How much does it weigh in pounds?");
      var dinoWeight = Console.ReadLine();
      Console.WriteLine("What enclosure number will it go in?");
      var dinoEnclosure = Console.ReadLine();

      var dino = new Dinosaur();
      dino.Name = dinoName;
      dino.DietType = dinoDiet;
      dino.DateAcquired = DateTime.Now;
      dino.Weight = decimal.Parse(dinoWeight);
      dino.EnclosureNumber = int.Parse(dinoEnclosure);

      Dinos.Add(dino);

    }

    static void DisplayListOfDinos(IEnumerable<Dinosaur> Dinos)
    {
      Console.WriteLine("Your dinosaurs are:");
      foreach (var Dino in Dinos)
      {
        Console.WriteLine(Dino.Name);
        var diet = "";
        if (Dino.DietType == true)
        {
          diet = "Carnivore";
        }
        else if (Dino.DietType == false)
        {
          diet = "Herbivore";
        }
        Console.WriteLine($"Diet: {diet}");
        Console.WriteLine($"Weight: {Dino.Weight} lbs");
        Console.WriteLine($"Enclosure Number: {Dino.EnclosureNumber}");
        Console.WriteLine($"Time found: {Dino.DateAcquired}");
        Console.WriteLine("---------------------------");
      }
    }

    static void RemoveDino()
    {
      Console.WriteLine("What is the name of the dinosaur you would like to remove?");
      var removed = Console.ReadLine();
      Dinos.RemoveAll(dino => dino.Name == removed);

    }

    static void TransferDino()
    {
      Console.WriteLine("What is the name of the dinosaur you would like to transfer?");
      var name = Console.ReadLine();
      var item = Dinos.FirstOrDefault(Dino => Dino.Name == name);
      if (item != null)
      {
        Console.WriteLine($"The Current Enclosure number for {item.Name} is {item.EnclosureNumber}.");
      }
      else
      {
        Console.WriteLine("Name not recognized");
        TransferDino();
      }
      Console.WriteLine("What Enclosure do you want to send him to?");
      var number = Console.ReadLine();
      item.EnclosureNumber = int.Parse(number);
    }

    static void DisplayAll()
    {
      DisplayListOfDinos(Dinos);
    }

    static void DietCounter()
    {
      int carnivores = 0;
      int herbivores = 0;
      foreach (var Dino in Dinos)
      {
        if (Dino.DietType == true)
        {
          carnivores++;
        }
        else if (Dino.DietType == false)
        {
          herbivores++;
        }
      }
      Console.WriteLine("You Have:");
      if (carnivores == 1)
      {
        Console.WriteLine($"{carnivores} carnivore");
      }
      else
      {
        Console.WriteLine($"{carnivores} carnivores");
      }
      if (herbivores == 1)
      {
        Console.WriteLine($"{herbivores} herbivore");
      }
      else
      {
        Console.WriteLine($"{herbivores} herbivores");
      }

    }
    static void Main(string[] args)
    {
      Console.WriteLine("Welcome to Dino Tracker!");
      var input = "";
      while (input != "quit")
      {
        Console.WriteLine("Available commands: add, remove, view, transfer, diets, thicc, quit");
        input = Console.ReadLine().ToLower();
        if (input == "add")
        {
          AddDino();
        }
        else if (input == "view")
        {
          DisplayAll();
        }
        else if (input == "remove")
        {
          RemoveDino();
        }
        else if (input == "transfer")
        {
          TransferDino();
        }
        else if (input == "diets")
        {
          DietCounter();
        }

      }
    }
  }
}
