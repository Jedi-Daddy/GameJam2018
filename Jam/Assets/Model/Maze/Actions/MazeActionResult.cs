using System.Collections.Generic;
using System.Linq;

namespace Assets.Model.Maze.Actions
{
  public class MazeActionResult
  {
    public List<SegmentAction> SegmentActions;
    public List<PlayerAction> PlayerActions;

    public bool IsEmpty
    {
      get
      {
        return (SegmentActions == null || !SegmentActions.Any()) && (PlayerActions == null || !PlayerActions.Any());
      }
    }
  }
}