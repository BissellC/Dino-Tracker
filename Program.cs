using System;
using System.Collections.Generic;
using System.Linq;


namespace Dino_Tracker
{
  class Program
  {
    static DatabaseContext Db = new DatabaseContext();

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

      Db.Dinosaurs.Add(dino);
      Db.SaveChanges();

    }

    static void DisplayListOfDinos(IEnumerable<Dinosaur> Dinos)
    {
      Console.WriteLine("Your dinosaurs are:");
      foreach (var Dino in Db.Dinosaurs)
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
      var chosen = Db.Dinosaurs.FirstOrDefault(dino => dino.Name == removed);
      if (chosen != null)
      {
        Db.Dinosaurs.Remove(chosen);
        Db.SaveChanges();
        Console.WriteLine("Removed!");
      }
      else
      {
        Console.WriteLine("Invalid Name");
      }

    }

    static void TransferDino()
    {
      Console.WriteLine("What is the name of the dinosaur you would like to transfer?");
      var name = Console.ReadLine();
      var item = Db.Dinosaurs.FirstOrDefault(Dino => Dino.Name == name);
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
      Db.SaveChanges();
    }

    static void DisplayAll()
    {
      DisplayListOfDinos(Db.Dinosaurs);
    }

    static void DietCounter()
    {
      int carnivores = 0;
      int herbivores = 0;
      foreach (var Dino in Db.Dinosaurs)
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

    static void Heavies()
    {
      DisplayTopThree(Db.Dinosaurs.OrderByDescending(dino => dino.Weight).Take(3));
    }

    static void DisplayTopThree(IEnumerable<Dinosaur> Dinos)
    {
      Console.WriteLine("Your 3 heavies dinosaurs are:");
      foreach (var Dino in Dinos)
      {
        Console.WriteLine($"{Dino.Name} weighing in at {Dino.Weight}");
        Console.WriteLine("---------------------------------------------------------");
      }
      Console.WriteLine("Thicc!");
    }

    static void ReleaseDino()
    {
      Console.WriteLine("Which Dinosaur would you like to release?");
      var released = Console.ReadLine();
      var freeDino = Db.Dinosaurs.FirstOrDefault(Dino => Dino.Name == released);
      freeDino.EnclosureNumber = default;
      Console.WriteLine($"Bye, {freeDino.Name}");
      Db.SaveChanges();


    }

    static void HatchDino()
    {
      string[] names = { "Bridgette", "Wonda", "Roderick", "Ginny", "Saundra", "Sook", "Dick", "Mari", "Sparkle", "Chara", "Ericka", "Waldo", "Nieves", "Gertrudis", "Verla", "Donte", "Gregorio", "Olivia", "Breann", "Sung", "Salley", "Markita", "Vonnie", "Jason", "Ona", "Mimi", "Delmar", "Mariana", "Pearle", "Amira", "Dorine", "Mitzie", "Leslee", "Prudence", "Tennie", "Fabiola", "Janna", "Doreen", "Luther", "Su", "Johana", "Willodean", "Werner", "Rosalina", "Paula", "Nicole", "Allena", "Natasha", "Nakita", "Jeff" };
      Console.WriteLine("The egg is hatching!");

      var dino = new Dinosaur();
      Random random = new Random();
      dino.Name = names[random.Next(0, 50)];
      dino.DietType = random.Next(2) == 0;
      dino.DateAcquired = DateTime.Now;
      dino.Weight = random.Next(0, 1000);
      dino.EnclosureNumber = 1;
      var dinoDiet = "";
      if (dino.DietType == true)
      {
        dinoDiet = "Carnivore";
      }
      else if (dino.DietType == false)
      {
        dinoDiet = "Herbivore";
      }

      Console.WriteLine($"Your new dinosaur is named {dino.Name}, weighs {dino.Weight}lbs, and is a {dinoDiet}!");

      Db.Dinosaurs.Add(dino);
      Db.SaveChanges();

    }

    static void NeedsASheep()
    {
      DisplayHungry(Db.Dinosaurs.Where(dino => dino.DietType == true).OrderBy(dino => dino.Weight).Take(1));

    }
    static void DisplayHungry(IEnumerable<Dinosaur> Dinos)
    {

      foreach (var Dino in Dinos)
      {
        Console.WriteLine($"{Dino.Name} is starving at {Dino.Weight} lbs. Give sheep now!");
      }
    }
    static void QuitMessage()
    {
      Console.WriteLine("Goodbye");
    }

    static void UnknownCommand()
    {
      Console.WriteLine("Invalid input, try again.");
    }

    static void Main(string[] args)
    {
      Console.WriteLine("Welcome to Dino Tracker!");
      var input = "";
      while (input != "quit")
      {
        Console.WriteLine("Available commands: add, remove, view, transfer, hatch, release, diets, hungry, heavies, quit");
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
        else if (input == "heavies")
        {
          Heavies();
        }
        else if (input == "quit")
        {
          QuitMessage();
        }
        else if (input == "release")
        {
          ReleaseDino();
        }
        else if (input == "hatch")
        {
          HatchDino();
        }
        else if (input == "hungry")
        {
          NeedsASheep();
        }
        else
        {
          UnknownCommand();
        }

      }
    }
  }
}
