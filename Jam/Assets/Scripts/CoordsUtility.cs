using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Model.Maze;
using UnityEngine;

namespace Assets.Scripts
{
  public static class CoordsUtility
  {
    public static Vector2 GetUiPosition(LocationInMaze locationInMaze)
    {
      var xFinal = 0;
      var yFinal = 0;
      var halfOfLavaWidth = 15;
      var segmentWidth = 16*30;

      var xSign = locationInMaze.SegmentId%2 == 0 ? -1 : 1;
      var xSegmentOffset = xSign * (segmentWidth - halfOfLavaWidth);//16 tiles by 30 pix
      var ySign = locationInMaze.SegmentId/2 == 0 ? 1 : -1;
      var ySegmentOffset = ySign * (segmentWidth - halfOfLavaWidth);
      var xCellOffset = locationInMaze.CoordsInSegment.X*30 + locationInMaze.CoordsInSegment.X;

      xFinal += xSegmentOffset;
      yFinal += ySegmentOffset;
      return new Vector2(xFinal, yFinal);
    }
  }
}
