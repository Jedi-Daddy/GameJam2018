using System.Collections.Generic;
using System.Linq;
using Assets.Model.Maze;
using Assets.Model.Maze.MazeObjects;
using Assets.Model.Maze.MazeObjects.Chest;

namespace Assets.Model
{
  public class GameState
  {
    public int Turn;
    public int MaxHitPoints;
    public Maze.Maze Maze;
    public List<Player> Players;
    public List<Hero> Heroes;
    public List<Chest> Chests;
    public int? SegmentToRebuild;
    public string Message;

    public Player CurrentPlayer
    {
      get { return Players[Turn%Players.Count]; }
    }

    public bool IsWin
    {
      get { return Players.Count(p => !p.IsDead) == 1; }

    }

    public Hero CurrentHero
    {
      get { return Heroes.First(h => h.OwnerId == CurrentPlayer.Id); }
    }

    public List<PathNode> Path
    {
      get { return Maze.GetPassableCells(CurrentHero.CurrentPositionInMaze, Turn, CurrentPlayer.ActionPoints).PassibleCells; }
    } 

    public bool TurnForMazeAction { get { return Turn != 0 && Turn % Players.Count(p => !p.IsDead) == 0; } }
  }
}
