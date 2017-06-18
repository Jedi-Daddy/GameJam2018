using Assets.Model.Maze.MazeObjects.Chest;

namespace Assets.Model
{
  public class RubyChest : Chest
  {
    public int RubyAmount;

    public override ChestOpeningResult OpenChest()
    {
      OnTake();
      return new ChestOpeningResult
      {
        ChestOpeningResultType = ChestOpeningResultType.Ruby,
        Rubys = RubyAmount
      };
    }
  }
}