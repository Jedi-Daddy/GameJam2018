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
      //locationInMaze = new LocationInMaze
      //{
      //  SegmentId = 0,
      //  CoordsInSegment = new Point(2, 2)
      //};
      var xFinal = 0;
      var yFinal = 0;
      const int halfOfLavaWidth = 15;
      const int segmentWidth = 14*30;
      var xSegmentOffset = 0;
      if (locationInMaze.SegmentId%2 == 0)
        xSegmentOffset = -1*(segmentWidth + halfOfLavaWidth); //14 tiles by 30 pix
      else
      {
        xSegmentOffset = halfOfLavaWidth;
      }
      var ySegmentOffset = 0;
      if (locationInMaze.SegmentId/2 == 0)
        ySegmentOffset = segmentWidth + halfOfLavaWidth;
      else
      {
        ySegmentOffset = -halfOfLavaWidth;
      }

      var xCellsOffset = 30 + (locationInMaze.CoordsInSegment.X*60 + locationInMaze.CoordsInSegment.X*30);
      var yCellsOffset = -30 - (locationInMaze.CoordsInSegment.Y * 60 + locationInMaze.CoordsInSegment.Y * 30);
     

      xFinal += xSegmentOffset;
      xFinal += xCellsOffset;
      yFinal += ySegmentOffset;
      yFinal += yCellsOffset;
      //var xCellOffset = locationInMaze.CoordsInSegment.X*30 + 30;

      
      return new Vector2(xFinal, yFinal);
    }

    //public static LocationInMaze GetUiPosition(Vector2 locationOnField)
    //{
    //  //var xFinal = 0;
    //  //var yFinal = 0;
    //  //const int halfOfLavaWidth = 15;
    //  //const int segmentWidth = 14 * 30;
    //  //var xSegmentOffset = 0;
    //  //if (locationInMaze.SegmentId % 2 == 0)
    //  //  xSegmentOffset = -1 * (segmentWidth + halfOfLavaWidth); //14 tiles by 30 pix
    //  //else
    //  //{
    //  //  xSegmentOffset = halfOfLavaWidth;
    //  //}
    //  //var ySegmentOffset = 0;
    //  //if (locationInMaze.SegmentId / 2 == 0)
    //  //  ySegmentOffset = segmentWidth + halfOfLavaWidth;
    //  //else
    //  //{
    //  //  ySegmentOffset = -halfOfLavaWidth;
    //  //}

    //  //var xCellsOffset = 30 + (locationInMaze.CoordsInSegment.X * 60 + locationInMaze.CoordsInSegment.X * 30);
    //  //var yCellsOffset = -30 - (locationInMaze.CoordsInSegment.Y * 60 + locationInMaze.CoordsInSegment.Y * 30);


    //  //xFinal += xSegmentOffset;
    //  //xFinal += xCellsOffset;
    //  //yFinal += ySegmentOffset;
    //  //yFinal += yCellsOffset;
    //  ////var xCellOffset = locationInMaze.CoordsInSegment.X*30 + 30;


    //  //return new Vector2(xFinal, yFinal);
    //}
  }
}
