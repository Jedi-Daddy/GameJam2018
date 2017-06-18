using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;

namespace Assets.Model.Maze
{
  public class MazePassibleResult
  {
    public List<LocationInMaze> PassibleCells;
    public List<LocationInMaze> ImpassibleCells;
  }

  public class Maze
  {
    public List<MazeSegment> Segments;
    public List<MazeObject> MazeObjects;

    public Maze(int playersCount)
    {
      Segments = new List<MazeSegment>(playersCount);
      MazeObjects = new List<MazeObject>();
    }

    public MazePassibleResult GetPassableCells(LocationInMaze from, int gameTurn)
    {
      var result = new MazePassibleResult
      {
        PassibleCells = new List<LocationInMaze>(),
        ImpassibleCells = new List<LocationInMaze>()
      };

      for (var segmentId = 0; segmentId < Segments.Count; segmentId++)
      {
        var currentSegment = Segments[segmentId];
        foreach (var cellInfo in currentSegment.Cells)
        {
          var location = new LocationInMaze
          {
            SegmentId = segmentId,
            CoordsInSegment = cellInfo.Coords
          };
          if (CanPass(from, location, gameTurn))
            result.PassibleCells.Add(location);
          else
          {
            result.ImpassibleCells.Add(location);
          }
        }
      }
      return result;
    }

    public bool CanPass(LocationInMaze from, LocationInMaze to, int currentGameTurn)
    {
      if(from.Equals(to))
        return false;

      if (from.SegmentId == to.SegmentId)
        return Segments[from.SegmentId].CanPass(from.CoordsInSegment, to.CoordsInSegment);

      if (Segments[from.SegmentId].HasEffect(MazeSegmentEffectType.Blocked, currentGameTurn)
        || Segments[to.SegmentId].HasEffect(MazeSegmentEffectType.Blocked, currentGameTurn))
        return false;

      if(from.CoordsInSegment.X != to.CoordsInSegment.X && from.CoordsInSegment.Y != to.CoordsInSegment.Y)
        return false;
      

      var fromSegment = Segments[from.SegmentId];
      var mazeSide = (int)Math.Sqrt(fromSegment.Matrix.GetLength(0));
      var mazeSideCenter = mazeSide/2;
      var isFromLeftGate = from.CoordsInSegment.X == 0 && from.CoordsInSegment.Y == mazeSideCenter;
      var isFromRightGate = from.CoordsInSegment.X == mazeSide - 1 && from.CoordsInSegment.Y == mazeSideCenter;
      var isFromTopGate = from.CoordsInSegment.Y == 0 && from.CoordsInSegment.X == mazeSideCenter;
      var isFromBottomGate = from.CoordsInSegment.Y == mazeSide - 1 && from.CoordsInSegment.X == mazeSideCenter;

      var isToLeftGate  = to.CoordsInSegment.X == 0 && to.CoordsInSegment.Y == mazeSideCenter;
      var isToRightGate = to.CoordsInSegment.X == mazeSide - 1 && to.CoordsInSegment.Y == mazeSideCenter;
      var isToTopGate = to.CoordsInSegment.Y == 0 && to.CoordsInSegment.X == mazeSideCenter;
      var isToBottomGate = to.CoordsInSegment.Y == mazeSide - 1 && to.CoordsInSegment.X == mazeSideCenter;

      var isFromFirstRow = from.SegmentId/2 == 0;
      var isFromFirstColumn = from.SegmentId % 2 == 0;
      var onSameRow = from.SegmentId/2 == to.SegmentId/2;
      var onSameColumn = from.SegmentId % 2 == to.SegmentId % 2;

      if (!isFromRightGate && !isFromLeftGate && !isFromTopGate && !isFromBottomGate)
        return false;

      if (!isToRightGate && !isToLeftGate && !isToTopGate && !isToBottomGate)
        return false;

      if (isFromRightGate)
      {
        if(!isToLeftGate)
          return false;
        if (((to.SegmentId - from.SegmentId) != 1 && !(!onSameColumn && !onSameRow)))
          return false;
      }

      if (isFromLeftGate)
      {
        if(!isToRightGate)
          return false;
        if (((from.SegmentId - to.SegmentId) != 1 && !(!onSameColumn && !onSameRow)))
          return false;
      }

      if (isFromTopGate)
      {
        if(!isToBottomGate)
          return false;

        if (isFromFirstRow)
        {
          if (isFromFirstColumn && to.SegmentId != 3)
            return false;
          if(!isFromFirstColumn && to.SegmentId != 2)
            return false;
        }
        else
        {
          if (isFromFirstColumn && to.SegmentId != 0)
            return false;
          if (!isFromFirstColumn && to.SegmentId != 1)
            return false;
        }
      }

      if (isFromBottomGate)
      {
        if(!isToTopGate)
          return false;

        if (isFromFirstRow)
        {
          if (isFromFirstColumn && to.SegmentId != 2)
            return false;
          if (!isFromFirstColumn && to.SegmentId != 3)
            return false;
        }
        else
        {
          if (isFromFirstColumn && to.SegmentId != 1)
            return false;
          if (!isFromFirstColumn && to.SegmentId != 0)
            return false;
        }
      }
      return true;
    }

    public MazeActionType GetAction()
    {
      return MazeActionType.Nothing;
      var rand = new Random();
      if (rand.NextDouble() < 0.5)
        return MazeActionType.Teleport;
      else
      {
        return MazeActionType.Teleport;
      }
    }

    public List<MazeObject> GetObjects(LocationInMaze currentPositionInMaze)
    {
      return MazeObjects.Where(mo => currentPositionInMaze.CoordsInSegment.Equals(currentPositionInMaze)).ToList();
    }

    public bool CanSee(LocationInMaze currentPositionInMaze, LocationInMaze heroPosition)
    {
      return true;
    }
  }
}
