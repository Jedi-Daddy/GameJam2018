using System;
using Assets.Model.Maze;
using Assets.Model.Races;
using UnityEngine;

namespace Assets.Scripts
{
  public class GameFieldDrawer
  {
    //public RectTransform ContainerCube;
    private const int CellSize = 60;
    private const int WallWidth = 30;
    //private Maze _maze;

    // Use this for initialization
    public static void DrawField (RectTransform container, Maze maze)
    {
      const int segmentWidth = CellSize*5 + WallWidth*4;
      var mazeSegmentOffset = segmentWidth + 30;
      var center = container.transform.localPosition;
      var cubeWidth = container.rect.width;
      var topLeft = new Vector2(center.x - cubeWidth / 2, center.y + cubeWidth / 2);

      for (var i = 0; i < maze.Segments.Count; i++)
      {
        var offsetToMoveRight = mazeSegmentOffset * (i % 2);
        var offsetToMoveDown = mazeSegmentOffset * (i / 2);

        var segment = DrawMazeSegment(maze.Segments[i], segmentWidth);
        segment.AddComponent<SegmentInfo>().Id = i;
        segment.transform.SetParent(container.transform);
        segment.transform.localScale = Vector3.one;
        var segmentRect = segment.AddComponent<RectTransform>();
        segmentRect.sizeDelta = new Vector2(segmentWidth, segmentWidth);
        segment.transform.localPosition = new Vector3(topLeft.x + offsetToMoveRight+segmentWidth/2, topLeft.y - offsetToMoveDown - segmentWidth/2, -1);
      }
    }

    //не трогать
    private static GameObject DrawMazeSegment(MazeSegment segment, int segmentWidth)
    {
      var segmentObject = new GameObject();

      var segmentSideLength = Math.Sqrt(segment.Matrix.GetLength(0));//5

      for (int columnNumber = 0; columnNumber < segmentSideLength * 2 - 1; columnNumber++)
      {
        var cellX = columnNumber/2;
        var isWallColumn = columnNumber%2 == 1;
        for (int rowNumber = 0; rowNumber < segmentSideLength * 2 - 1; rowNumber++)
        {
          var cellY = rowNumber/2;
          var isWallRow = rowNumber%2 == 1;
          if (isWallColumn && isWallRow)
          {
            var newCellObject = GameObject.Instantiate(Resources.Load("Prefabs\\MapElements\\Wall")) as GameObject;
            var x = (cellX+1) * CellSize + cellX * WallWidth - segmentWidth / 2 + WallWidth/2;
            var y = (cellY + 1) * CellSize + cellY * WallWidth - segmentWidth / 2 + WallWidth / 2;
            newCellObject.transform.localPosition = new Vector3(x, -y);
            //var rect = newCellObject.AddComponent<RectTransform>();
            //rect.sizeDelta = new Vector3(WallWidth, CellSize);
            newCellObject.transform.SetParent(segmentObject.transform);
            //маленький квадратик
            continue;
          }

          //Cell
          if (!isWallColumn && !isWallRow)
          {
            var newCellObject = GameObject.Instantiate(Resources.Load(RaceManager.GetCellPrefabLocation(segment.RaceType))) as GameObject;
            newCellObject.transform.SetParent(segmentObject.transform);
            var cellScript = newCellObject.GetComponent<CellInfoViewModel>();
            cellScript.CellInfo = segment.GetCellByCoord(new Point(cellX, cellY));
         
            var x = cellX * CellSize + cellX * WallWidth - segmentWidth / 2 + CellSize / 2;
            var y = cellY * CellSize + cellY * WallWidth - segmentWidth/2 + CellSize/2;
            newCellObject.transform.localPosition = new Vector3(x, -y);
          }

          if (isWallColumn)
          {
            var x = (cellX + 1) * CellSize  + (cellX) * WallWidth + WallWidth / 2 - segmentWidth / 2;
            var y = cellY * CellSize + (cellY * WallWidth) - segmentWidth / 2 + CellSize / 2;
          
            var template = segment.CanPass(new Point(cellX, cellY), new Point(cellX +1, cellY))
              ? Resources.Load(RaceManager.GetWallPrefabLocation(segment.RaceType))
              : Resources.Load("Prefabs\\MapElements\\Wall_long");
            var newWallObject = GameObject.Instantiate(template) as GameObject;
            newWallObject.transform.SetParent(segmentObject.transform);
            //var rect = newWallObject.GetComponent<RectTransform>();
            //rect.sizeDelta = new Vector3(WallWidth, CellSize);
            newWallObject.transform.localPosition = new Vector3(x, -y);
          }

          if (isWallRow)
          {
            var x = (cellX) * CellSize + cellX * WallWidth - segmentWidth / 2 + CellSize / 2;
            var y = (cellY + 1) * CellSize + cellY * WallWidth - segmentWidth / 2 + WallWidth/2;
          
            var template = segment.CanPass(new Point(cellX, cellY), new Point(cellX, cellY+1))
              ? Resources.Load("Prefabs\\MapElements\\Tile_Elf_slim_v")
              : Resources.Load("Prefabs\\MapElements\\Wall_long");
            var newWallObject = GameObject.Instantiate(template) as GameObject;
            newWallObject.transform.SetParent(segmentObject.transform);
            newWallObject.transform.eulerAngles = new Vector3(0,0,90f);
            //var rect = newWallObject.GetComponent<RectTransform>();
            //rect.sizeDelta = new Vector3(CellSize, WallWidth);
            newWallObject.transform.localPosition = new Vector3(x, -y);
          }
        }
      }
      return segmentObject;
    }
  }
}
