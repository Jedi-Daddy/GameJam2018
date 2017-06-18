using System;

namespace Assets.Model.Maze.MazeObjects
{
  public class Hero : MazeObject
  {
    public Race Race;
    public int HitPoints;
    public event Action<LocationInMaze> OnMove;

    public void Move(LocationInMaze positionToMove)
    {
      if (OnMove != null)
        OnMove(positionToMove);
    }
  }
}