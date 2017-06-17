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
	  var mazeSegmentOffset = CellSize*5 + WallWidth*4 + 10;
    var center = ContainerCube.transform.position;
	  var cubeWidth = ContainerCube.transform.localScale.x;
	  var cubeHeight = ContainerCube.transform.localScale.y;
	  var topLeft = new Vector2(center.x - cubeWidth, center.y - cubeWidth);
    for (var i = 0; i < _maze.Segments.Count; i++)
    {
      var curentSegmentTopLeft = new Vector2(topLeft.x + mazeSegmentOffset*(i%2), topLeft.y + mazeSegmentOffset*(i/2));
      DrawMazeSegment(_maze.Segments[i], curentSegmentTopLeft);
    }
	}

  private void DrawMazeSegment(MazeSegment segment, Vector2 to)
  {
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
        }
      }
    }

  }

  // Update is called once per frame
	void Update () {
		
	}
}
