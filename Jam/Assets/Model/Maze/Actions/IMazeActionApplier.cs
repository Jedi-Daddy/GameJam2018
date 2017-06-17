using Assets.Scripts;

namespace Assets.Model
{
  public interface IMazeActionApplier
  {
    void ApplyAction(GameState state, MazeActionType actionType, MazeActionResult result);
  }
}