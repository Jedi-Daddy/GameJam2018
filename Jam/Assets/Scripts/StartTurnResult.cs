using Assets.Model;
using Assets.Model.Maze;
using Assets.Model.Maze.MazeObjects;

namespace Assets.Scripts
{
  public class StartTurnResult
  {
    public Player PlayerToAct;
    public Hero PlayerHero;
    public MazePassibleResult WhereHeroCanMove;
    public MazeActionResult MazeActionResult;
  }
}