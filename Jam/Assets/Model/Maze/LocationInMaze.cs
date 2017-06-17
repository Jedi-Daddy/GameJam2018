using System;

namespace Assets.Model.Maze
{
  public class LocationInMaze : IEquatable<LocationInMaze>
  {
    public int SegmentId;
    public Point CoordsInSegment;

    public bool Equals(LocationInMaze other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return SegmentId == other.SegmentId && CoordsInSegment.Equals(other.CoordsInSegment);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((LocationInMaze)obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return (SegmentId * 397) ^ CoordsInSegment.GetHashCode();
      }
    }
  }
}