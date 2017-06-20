using System;

namespace Assets.Model.Maze.Actions
{
  internal class TeleportMazeActionApplier : IMazeActionApplier
  {
    public void ApplyAction(GameState state, MazeActionType actionType)
    {
      if(actionType != MazeActionType.Teleport)
        return;
      var random = new Random();
      var locationToTeleport = new LocationInMaze
      {
        SegmentId = random.Next(0, state.Maze.Segments.Count),
        CoordsInSegment = new Point(random.Next(0, 5), random.Next(0, 5)),
      };
      var heroVictim = state.Heroes[random.Next(0, state.Heroes.Count)];
      heroVictim.Move(locationToTeleport);
    }
  }
}