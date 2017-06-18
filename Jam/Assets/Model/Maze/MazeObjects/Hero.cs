using System;

namespace Assets.Model.Maze.MazeObjects
{
  public class Hero : MazeObject
  {
    public Race Race;
    public int HitPoints;
    public event Action<LocationInMaze> OnMove;
    public event Action OnDie;

    public void RemoveEvents()
    {
      OnMove = null;
      OnDie = null;
    }

    public void Move(LocationInMaze positionToMove)
    {
      if (OnMove != null)
        OnMove(positionToMove);
      CurrentPositionInMaze = positionToMove;
    }

    public void Die()
    {
      OnMove = null;
      var onDieActions = OnDie;
      OnDie = null;
      if (onDieActions != null)
        onDieActions();
    }
  }
}