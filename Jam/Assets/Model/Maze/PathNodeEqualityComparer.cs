using System.Collections.Generic;

namespace Assets.Model.Maze
{
  public class PathNodeEqualityComparer : IEqualityComparer<PathNode>
  {
    public bool Equals(PathNode x, PathNode y)
    {
      return x.Cell.Equals(y.Cell);
    }

    public int GetHashCode(PathNode node)
    {
      return node.Cell.GetHashCode();
    }
  }
}