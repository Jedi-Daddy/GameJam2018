using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class GameFieldDraw : MonoBehaviour
{
  public RectTransform ContainerCube;
  private const int CellSize = 60;
  private const int WallWidth = 20;
  private Maze _maze;

	// Use this for initialization
	void Start ()
	{
    _maze = MazeBuilder.BuildMaze(4);
	  var segmentWidth = CellSize*5 + WallWidth*4;
    var mazeSegmentOffset = segmentWidth + 40;
    var center = ContainerCube.transform.localPosition;
	  var cubeWidth = ContainerCube.rect.width;
    var topLeft = new Vector2(center.x - cubeWidth / 2, center.y + cubeWidth / 2);

    for (var i = 0; i < _maze.Segments.Count; i++)
    {
      var offsetToMoveRight = mazeSegmentOffset * (i % 2);
      var offsetToMoveDown = mazeSegmentOffset * (i / 2);

      var segment = DrawMazeSegment(_maze.Segments[i], segmentWidth);
      segment.transform.SetParent(ContainerCube.transform);
      var segmentRect = segment.AddComponent<RectTransform>();
      segmentRect.sizeDelta = new Vector2(segmentWidth, segmentWidth);
      segment.transform.localPosition = new Vector3(topLeft.x + offsetToMoveRight+segmentWidth/2, topLeft.y - offsetToMoveDown - segmentWidth/2, -1);
    }
	}

  //не трогать
  private GameObject DrawMazeSegment(MazeSegment segment, int segmentWidth)
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
          //маленький квадратик
          continue;
        }

        //Cell
        if (!isWallColumn && !isWallRow)
        {
          var newCellObject = Instantiate(Resources.Load("Prefabs\\cellTest")) as GameObject;
          newCellObject.transform.SetParent(segmentObject.transform);
          var cellScript = newCellObject.GetComponent<CellInfoViewModel>();
          cellScript.CellInfo = segment.GetCellByCoord(new Point(cellX, cellY));
          var x = cellX * CellSize + cellX * WallWidth - segmentWidth / 2 + CellSize / 2;
          var y = cellY * CellSize + cellY * WallWidth - segmentWidth/2 + CellSize/2;
          newCellObject.transform.localPosition = new Vector3(x, -y);
        }

        if (isWallColumn)
        {
          var newWallObject = Instantiate(Resources.Load("Prefabs\\wallTest")) as GameObject;
          newWallObject.transform.SetParent(segmentObject.transform);
          var rect = newWallObject.GetComponent<RectTransform>();
          var x = (cellX + 1) * CellSize + WallWidth / 2 + (cellX) * WallWidth - segmentWidth / 2;
          var y = cellY * CellSize + (cellY * WallWidth) - segmentWidth / 2 + CellSize / 2;
          rect.sizeDelta = new Vector3(WallWidth, CellSize);
          newWallObject.transform.localPosition = new Vector3(x, -y);
        }

        if (isWallRow)
        {
          var newWallObject = Instantiate(Resources.Load("Prefabs\\wallTest")) as GameObject;
          newWallObject.transform.SetParent(segmentObject.transform);
          var rect = newWallObject.GetComponent<RectTransform>();

          var x = (cellX) * CellSize + cellX * WallWidth - segmentWidth / 2 + CellSize / 2;
          var y = (cellY + 1) * CellSize + cellY * WallWidth - segmentWidth / 2 + WallWidth/2;
          rect.sizeDelta = new Vector3(CellSize, WallWidth);
          newWallObject.transform.localPosition = new Vector3(x, -y);
        }
      }
    }
    return segmentObject;
  }

  // Update is called once per frame
	void Update () {
		
	}
}
