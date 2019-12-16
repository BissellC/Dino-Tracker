using System;

namespace Dino_Tracker
{
  public class Dinosaur
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public bool DietType { get; set; }
    public DateTime DateAcquired { get; set; }
    public decimal Weight { get; set; }
    public int EnclosureNumber { get; set; }
  }
}