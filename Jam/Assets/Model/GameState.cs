using System.Collections.Generic;
using System.Linq;
using Assets.Model.Maze.MazeObjects;
using Assets.Model.Maze.MazeObjects.Chest;

namespace Assets.Model
{
  public class GameState
  {
    public int Turn;
    public Maze.Maze Maze;
    public List<Player> Players;
    public List<Hero> Heroes;
    public List<Chest> Chests;

    public Player CurrentPlayer
    {
      get { return Players[Turn%Players.Count]; }
    }

    public bool TurnForMazeAction { get { return Turn % Players.Count(p => !p.IsDead) == 0; } }
  }

  public class Player
  {
    public int Id;
    public bool IsDead;
    public int ActionPoints;
    public List<Card> Cards;
    public ItemSlot Slot;
  }

  public class ItemSlot
  {
    public Weapon Weapon;
    public Anh Anh;

    public ItemSlot(Weapon weapon)
    {
      Weapon = weapon;
    }

    public ItemSlot(Anh anh)
    {
      Anh = anh;
    }
  }

  public class Card
  {
    //todo
  }
}
