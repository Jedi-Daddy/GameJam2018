using System;

namespace Assets.Model.Maze.Actions
{
  internal class RebuildMazeSegmentActionApplier : IMazeActionApplier
  {
    public void ApplyAction(GameState state, MazeActionType actionType)
    {
      if (actionType != MazeActionType.Rebuild)
        return;
      var random = new Random();
      var segmentId = random.Next(0, state.Maze.Segments.Count);
      var mazeSegment = state.Maze.Segments[segmentId];
      state.Maze.Segments.Remove(mazeSegment);
      var newSegment = MazeBuilder.BuildSegment(state.Players[segmentId]);
      state.Maze.Segments.Insert(segmentId, newSegment);
      state.SegmentToRebuild = segmentId;
    }
  }
}