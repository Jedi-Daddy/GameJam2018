namespace Assets.Model.Maze.Actions
{
  public interface IMazeActionApplier
  {
    void ApplyAction(GameState state, MazeActionType actionType);
  }
}