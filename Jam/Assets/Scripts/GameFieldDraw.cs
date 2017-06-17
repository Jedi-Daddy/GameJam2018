using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class GameFieldDraw : MonoBehaviour
{
  public GameObject ContainerCube;
  private const int CellSize = 60;
  private const int WallWidth = 20;
  private Maze _maze;

	// Use this for initialization
	void Start ()
	{
    _maze = MazeBuilder.BuildMaze(4);
	  var segmentWidth = CellSize*5 + WallWidth*4;
    var mazeSegmentOffset = segmentWidth + 10;
    var center = ContainerCube.transform.position;
	  var cubeWidth = ContainerCube.transform.localScale.x;
    var topLeft = new Vector2(center.x - cubeWidth / 2, center.y + cubeWidth / 2);
    for (var i = 0; i < _maze.Segments.Count; i++)
    {
      var offsetToMoveRight = mazeSegmentOffset * (i % 2);
      var offsetToMoveDown = mazeSegmentOffset * (i / 2);

      var segment = DrawMazeSegment(_maze.Segments[i]);
      segment.transform.localPosition = new Vector3(topLeft.x + segmentWidth + offsetToMoveRight, topLeft.y - segmentWidth - offsetToMoveDown, -1);
      segment.transform.SetParent(ContainerCube.transform);
    }
	}

  private GameObject DrawMazeSegment(MazeSegment segment)
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
          var cellScript = newCellObject.GetComponent<CellInfoViewModel>();
          cellScript.CellInfo = segment.GetCellByCoord(new Point(cellX, cellY));
          var x = cellX*CellSize + cellX*WallWidth;
          var y = cellY*CellSize + cellY*WallWidth;
          newCellObject.transform.localScale = new Vector3(CellSize,CellSize);
          segmentObject.transform.localPosition = new Vector3(x,-y);
          newCellObject.transform.SetParent(segmentObject.transform);
        }

        if (isWallColumn)
        {
          var newWallObject = Instantiate(Resources.Load("Prefabs\\wallTest")) as GameObject;
          var x = (cellX + 1) * CellSize + (cellX) * WallWidth;
          var y = cellY * CellSize + (cellY * WallWidth);
          segmentObject.transform.localPosition = new Vector3(x, - y);
          newWallObject.transform.localScale = new Vector3(WallWidth, CellSize);
          newWallObject.transform.SetParent(segmentObject.transform);
        }

      }
    }
    return segmentObject;
  }

  // Update is called once per frame
	void Update () {
		
	}
}
