using System;

namespace Assets.Model.Maze.MazeObjects.Chest
{
  public abstract class Chest : MazeObject
  {
    public ChestOpeningResultType ChestResultType;
    public event Action Take;
    
    public void RemoveEvents()
    {
      Take = null;
    }

    public void OnTake()
    {
      var eventChain = Take;
      Take = null;
      if (eventChain != null)
        eventChain();
    }
    
    public abstract ChestOpeningResult OpenChest();
  }
}