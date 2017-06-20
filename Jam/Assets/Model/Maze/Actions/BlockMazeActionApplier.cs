using System;
using System.Linq;

namespace Assets.Model.Maze.Actions
{
  internal class BlockMazeActionApplier : IMazeActionApplier
  {
    public void ApplyAction(GameState state, MazeActionType actionType)
    {
      if(actionType != MazeActionType.Lock)
        return;

      var random = new Random();
      state.Maze.Segments[random.Next(0,state.Maze.Segments.Count)].SegmentSpecial.SegmentEffects.Add(new MazeSegmentEffect
      {
        EffectType = MazeSegmentEffectType.Blocked,
        TurnUntil = state.Turn + 2 * state.Players.Count(p=>!p.IsDead)
      });
    }
  }
}