using System;

namespace Assets.Model.Maze.MazeObjects.Chest
{
  public abstract class Chest : MazeObject
  {
    public ChestOpeningResultType ChestResultType;
    public event Action Take;

    public void OnTake()
    {
      if (Take != null)
        Take();
    }
    
    public abstract ChestOpeningResult OpenChest();
  }
}