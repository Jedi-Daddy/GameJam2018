using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Model.Maze.MazeObjects;

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

    public void AddMazeObject(MazeObject mazeObject)
    {
      MazeObjects.Add(mazeObject);
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
          if (CanPass2(from, location, gameTurn))
            result.PassibleCells.Add(location);
          else
          {
            result.ImpassibleCells.Add(location);
          }
        }
      }
      return result;
    }

    //public bool CanPass(LocationInMaze from, LocationInMaze to, int currentGameTurn)
    //{
    //  if(from.Equals(to))
    //    return false;

    //  var objectsOnCell = GetObjects(to);
    //  if(objectsOnCell != null && objectsOnCell.Any(o=>!o.IsPassable))
    //    return false;

    //  if (from.SegmentId == to.SegmentId)
    //    return Segments[from.SegmentId].CanPass(from.CoordsInSegment, to.CoordsInSegment);

    //  if (Segments[from.SegmentId].HasEffect(MazeSegmentEffectType.Blocked, currentGameTurn)
    //    || Segments[to.SegmentId].HasEffect(MazeSegmentEffectType.Blocked, currentGameTurn))
    //    return false;

    //  if(from.CoordsInSegment.X != to.CoordsInSegment.X && from.CoordsInSegment.Y != to.CoordsInSegment.Y)
    //    return false;
      

    //  var fromSegment = Segments[from.SegmentId];
    //  var mazeSide = (int)Math.Sqrt(fromSegment.Matrix.GetLength(0));
    //  var mazeSideCenter = mazeSide/2;
    //  var isFromLeftGate = from.CoordsInSegment.X == 0 && from.CoordsInSegment.Y == mazeSideCenter;
    //  var isFromRightGate = from.CoordsInSegment.X == mazeSide - 1 && from.CoordsInSegment.Y == mazeSideCenter;
    //  var isFromTopGate = from.CoordsInSegment.Y == 0 && from.CoordsInSegment.X == mazeSideCenter;
    //  var isFromBottomGate = from.CoordsInSegment.Y == mazeSide - 1 && from.CoordsInSegment.X == mazeSideCenter;

    //  var isToLeftGate  = to.CoordsInSegment.X == 0 && to.CoordsInSegment.Y == mazeSideCenter;
    //  var isToRightGate = to.CoordsInSegment.X == mazeSide - 1 && to.CoordsInSegment.Y == mazeSideCenter;
    //  var isToTopGate = to.CoordsInSegment.Y == 0 && to.CoordsInSegment.X == mazeSideCenter;
    //  var isToBottomGate = to.CoordsInSegment.Y == mazeSide - 1 && to.CoordsInSegment.X == mazeSideCenter;

    //  var isFromFirstRow = from.SegmentId/2 == 0;
    //  var isFromFirstColumn = from.SegmentId % 2 == 0;
    //  var onSameRow = from.SegmentId/2 == to.SegmentId/2;
    //  var onSameColumn = from.SegmentId % 2 == to.SegmentId % 2;

    //  if (!isFromRightGate && !isFromLeftGate && !isFromTopGate && !isFromBottomGate)
    //    return false;

    //  if (!isToRightGate && !isToLeftGate && !isToTopGate && !isToBottomGate)
    //    return false;

    //  if (isFromRightGate)
    //  {
    //    if(!isToLeftGate)
    //      return false;

    //    var neighbourSegment = (to.SegmentId - from.SegmentId) == 1;

    //    if (!neighbourSegment)
    //    {
    //      var isDiagonal = (!onSameColumn && !onSameRow);
    //      if(!isDiagonal)
    //        return false;

    //      if (isDiagonal && (from.SegmentId == 0 && from.CoordsInSegment.X != 0))
    //        return false;

    //      if (isDiagonal && (from.SegmentId == 3 && from.CoordsInSegment.X == 0))
    //        return false;
    //      if (isDiagonal && (from.SegmentId == 2 && from.CoordsInSegment.X == mazeSide -1))
    //        return false;
    //    }
    //    //)}
    //    //  return false;

    //  }

    //  if (isFromLeftGate)
    //  {
    //    if(!isToRightGate)
    //      return false;

    //    var isDiagonal = (!onSameColumn && !onSameRow); 
    //    if (((from.SegmentId - to.SegmentId) != 1) && (!isDiagonal || from.SegmentId == 1))
    //      return false;

    //    if (isDiagonal && (from.SegmentId == 3 && from.CoordsInSegment.X == 0))
    //      return false;
    //  }

    //  if (isFromTopGate)
    //  {
    //    if(!isToBottomGate)
    //      return false;

    //    if (isFromFirstRow)
    //    {
    //      if (isFromFirstColumn && to.SegmentId != 3)
    //        return false;
    //      if(!isFromFirstColumn && to.SegmentId != 2)
    //        return false;
    //    }
    //    else
    //    {
    //      if (isFromFirstColumn && to.SegmentId != 0)
    //        return false;
    //      if (!isFromFirstColumn && to.SegmentId != 1)
    //        return false;
    //    }
    //  }

    //  if (isFromBottomGate)
    //  {
    //    if(!isToTopGate)
    //      return false;

    //    if (isFromFirstRow)
    //    {
    //      if (isFromFirstColumn && to.SegmentId != 2)
    //        return false;
    //      if (!isFromFirstColumn && to.SegmentId != 3)
    //        return false;
    //    }
    //    else
    //    {
    //      if (isFromFirstColumn && to.SegmentId != 1)
    //        return false;
    //      if (!isFromFirstColumn && to.SegmentId != 0)
    //        return false;
    //    }
    //  }
    //  return true;
    //}

    public bool CanPass2(LocationInMaze from, LocationInMaze to, int currentGameTurn, bool checkPassibility = true)
    {
      if (from.Equals(to))
        return false;

      var fromSegment = Segments[from.SegmentId];
      //If something stands on destination cell
      var objectsOnCell = GetObjects(to);
      if (checkPassibility && objectsOnCell != null && objectsOnCell.Any(o => !o.IsPassable))
        return false;

      //Moving on same segment
      if (from.SegmentId == to.SegmentId)
        return fromSegment.CanPass(from.CoordsInSegment, to.CoordsInSegment);

      //Current maze is blocked
      if (Segments[from.SegmentId].HasEffect(MazeSegmentEffectType.Blocked, currentGameTurn)
        || Segments[to.SegmentId].HasEffect(MazeSegmentEffectType.Blocked, currentGameTurn))
        return false;

      var sideLength = (int)Math.Sqrt(fromSegment.Matrix.GetLength(0));
      var mazeSideCenter = (int)Math.Round((double)sideLength / 2);

      if (from.CoordsInSegment.Equals(to.CoordsInSegment))
        return false;

      //Check we are standing before the gate
      if (from.CoordsInSegment.X != mazeSideCenter && from.CoordsInSegment.Y != mazeSideCenter)
        return false;

      //One of coords has to be the same
      if (from.CoordsInSegment.X != to.CoordsInSegment.X && from.CoordsInSegment.Y != to.CoordsInSegment.Y)
        return false;

      var isSameRow = from.SegmentId / 2 == to.SegmentId / 2;
      var isSameColumn = from.SegmentId % 2 == to.SegmentId % 2;

      var isFromSmallest = from.SegmentId < to.SegmentId;

      if (isSameRow)
      {
        if (from.CoordsInSegment.Y != mazeSideCenter || to.CoordsInSegment.Y != mazeSideCenter)
          return false;

        //One coord has to have max x and other min x
        if (Math.Abs(from.CoordsInSegment.X - to.CoordsInSegment.X) != sideLength - 1)
          return false;

        if (isFromSmallest && from.CoordsInSegment.X != sideLength - 1
         || !isFromSmallest && to.CoordsInSegment.X != sideLength - 1)
          return false;
      }
      else
      {
        if (isSameColumn)
        {
          if (from.CoordsInSegment.X != mazeSideCenter || to.CoordsInSegment.X != mazeSideCenter)
            return false;

          //One coord has to have max y and other min y
          if (Math.Abs(from.CoordsInSegment.Y - to.CoordsInSegment.Y) != sideLength - 1)
            return false;

          if (isFromSmallest && from.CoordsInSegment.Y != sideLength - 1
              || !isFromSmallest && to.CoordsInSegment.Y != sideLength - 1)
            return false;
        }
        else// different row and column
        {
          var isRightColumnFrom = from.SegmentId % 2 == 0;
          if (isRightColumnFrom && (from.CoordsInSegment.X == sideLength - 1))
            return false;

          var isFirstRowFrom = from.SegmentId / 2 == 0;
          if (isFirstRowFrom && (from.CoordsInSegment.Y == sideLength - 1))
            return false;

          if (!isRightColumnFrom && (from.CoordsInSegment.X == 0))
            return false;
          if (!isFirstRowFrom && (from.CoordsInSegment.Y == 0))
            return false;

          //We are moving verticaly
          if (from.CoordsInSegment.X == mazeSideCenter && to.CoordsInSegment.X != mazeSideCenter)
            return false;
          if (to.CoordsInSegment.X == mazeSideCenter && from.CoordsInSegment.X != mazeSideCenter)
            return false;
          if (from.CoordsInSegment.Y == mazeSideCenter && to.CoordsInSegment.Y != mazeSideCenter)
            return false;
          if (to.CoordsInSegment.Y == mazeSideCenter && from.CoordsInSegment.Y != mazeSideCenter)
            return false;
          if (from.CoordsInSegment.X == sideLength - 1 && to.CoordsInSegment.X != 0)
            return false;
          if (to.CoordsInSegment.X == sideLength - 1 && from.CoordsInSegment.X != 0)
            return false;
          if (from.CoordsInSegment.Y == sideLength - 1 && to.CoordsInSegment.Y != 0)
            return false;
          if (to.CoordsInSegment.Y == sideLength - 1 && from.CoordsInSegment.Y != 0)
            return false;
          if (from.CoordsInSegment.X == 0 && to.CoordsInSegment.X != sideLength - 1)
            return false;
          if (to.CoordsInSegment.X == 0 && from.CoordsInSegment.X != sideLength - 1)
            return false;
          if (from.CoordsInSegment.Y == 0 && to.CoordsInSegment.Y != sideLength - 1)
            return false;
          if (to.CoordsInSegment.Y == 0 && from.CoordsInSegment.Y != sideLength - 1)
            return false;
        }
      }

      return true;
    }



    private static readonly Dictionary<MazeActionType, double> ActionsByProbability = new Dictionary
     <MazeActionType, double>
    {
      {MazeActionType.Lock, 0.33},
      {MazeActionType.Rebuild, 0.33},
      {MazeActionType.Teleport, 0.33},
      //{MazeActionType.Nothing, 0.1},
    };

    public MazeActionType GetAction()
    {
      return GetRandomProbability(ActionsByProbability, 1, false)[0];
    }

    public static T[] GetRandomProbability<T>(Dictionary<T, double> probabilityByValue, int count, bool unique)
    {
      var rand = new Random();
      var result = new T[count];

      int remaining = count;
      int taken = 0;

      var set = !unique ? null : new HashSet<T>();

      while (remaining > 0)
      {
        var value = rand.NextDouble();
        var prev = 0.0;
        foreach (var prob in probabilityByValue)
        {
          if (value >= prev && value < prev + prob.Value)
          {
            if (!unique || !set.Contains(prob.Key))
            {
              result[taken++] = prob.Key;
              remaining--;

              if (unique)
                set.Add(prob.Key);

              break;
            }
          }
          prev += prob.Value;
        }
      }

      return result;
    }
   
    public List<MazeObject> GetObjects(LocationInMaze currentPositionInMaze)
    {
      return MazeObjects.Where(mo => mo.CurrentPositionInMaze.Equals(currentPositionInMaze)).ToList();
    }

    public void RemoveObject(MazeObject objectToRemove)
    {
      MazeObjects.Remove(objectToRemove);
    }

    public bool CanSee(LocationInMaze currentPositionInMaze, LocationInMaze positionToSee)
    {
      if (currentPositionInMaze.SegmentId != positionToSee.SegmentId)
        return false;

      if (!Segments[currentPositionInMaze.SegmentId].CanPass(currentPositionInMaze.CoordsInSegment, positionToSee.CoordsInSegment))
        return false;

      return true;
    }
  }
}
