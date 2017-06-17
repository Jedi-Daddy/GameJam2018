namespace Assets.Model.Maze.MazeObjects.Chest
{
  internal class AnhChest : Chest
  {
    public Anh Anh;

    public override ChestOpeningResult OpenChest()
    {
      return new ChestOpeningResult
      {
        ChestOpeningResultType = ChestOpeningResultType.Anh,
        Anh = Anh
      };
    }
  }
}