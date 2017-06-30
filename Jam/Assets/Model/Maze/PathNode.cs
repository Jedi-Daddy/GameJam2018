using System;

namespace Assets.Model.Maze
{
  public class PathNode : IEquatable<PathNode>
  {
    public LocationInMaze Cell;
    public int StepsToGet;

    public bool Equals(PathNode other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return Equals(Cell, other.Cell) && StepsToGet == other.StepsToGet;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((PathNode)obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return ((Cell != null ? Cell.GetHashCode() : 0) * 397) ^ StepsToGet;
      }
    }
  }
}