using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
  internal class GameState
  {
    public Maze Maze;
    public List<Player> Player;
    public List<Hero> 
  }

  public class MazeObject
  {
    public int? OwnerId;
    public LocationInMaze LocationInMaze;
  }

  internal class Hero : MazeObject
  {
    public Race Race;
    public int HitPoints;
  }

  public abstract class Chest : MazeObject
  {
    
  }

  public class RubyChest : Chest
  {
    public int RubyAmount;
  }

  public enum Race
  {
    Elves = 1,
    Ent = 2,
    Orc = 3,
    Vampire = 4
  }

  internal class Player
  {
    public int Id;
    public bool IsDead;
    public int ActionPoints;
    public List<Card> Cards;
  }

  internal class Card
  {
    //todo
  }
}
