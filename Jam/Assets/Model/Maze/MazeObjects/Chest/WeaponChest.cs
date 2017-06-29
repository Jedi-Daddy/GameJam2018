namespace Assets.Model.Maze.MazeObjects.Chest
{
  internal class WeaponChest : Chest
  {
    public Weapon Weapon;
    public override ChestOpeningResult OpenChest()
    {
      OnTake();
      return new ChestOpeningResult
      {
        ChestOpeningResultType = ChestOpeningResultType.Weapon,
        Weapon = Weapon
      };
    }
  }
}