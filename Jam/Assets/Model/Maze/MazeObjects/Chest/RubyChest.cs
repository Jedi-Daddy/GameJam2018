namespace Assets.Model.Maze.MazeObjects.Chest
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