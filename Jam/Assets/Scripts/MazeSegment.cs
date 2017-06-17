using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MazeSegment
{
  public int[,] Matrix;

  public List<CellInfo> Cells; 
  private Dictionary<int, CellInfo> _cellsById;
  private readonly Dictionary<Point, CellInfo> _cellsByCoords;

  public readonly Point HeroLocation = new Point(2,2);
  public readonly List<Point> ChestsPossibleLocations = new List<Point>
  {
    new Point(1, 1),
    new Point(1, 3),
    new Point(3, 3),
    new Point(3, 1)
  };
  
  public MazeSegment(string templateLocation)
  {
    var fileReader = new StreamReader(File.OpenRead(templateLocation));
    string line = null;
    int lineNumber = 0;
    Cells = new List<CellInfo>();
    _cellsById = new Dictionary<int, CellInfo>();
    _cellsByCoords = new Dictionary<Point, CellInfo>();
    while ((line = fileReader.ReadLine()) != null)
    {
      if(Matrix == null)
        Matrix = new int[line.Length,line.Length];//25x25
      for (var i = 0; i < line.Length; i++)
      {
        Matrix[i, lineNumber] = Convert.ToInt32(line[i]);
      }
      lineNumber++;//ends as 25
    }
    fileReader.Close();
    var mazeSideLength = (int)Math.Sqrt(lineNumber);//5
    for (var i = 0; i < lineNumber; i++)
    {
      var cellToAdd = new CellInfo
      {
        Id = i,
        Coords = new Point
        {
          X = i%mazeSideLength,
          Y = i/mazeSideLength
        }
      };
      Cells.Add(cellToAdd);
      _cellsById.Add(cellToAdd.Id, cellToAdd);
      _cellsByCoords.Add(cellToAdd.Coords, cellToAdd);
    }
  }

  public bool CanPass(Point from, Point to)
  {
    var cellFrom = _cellsByCoords[from];
    var cellTo = _cellsByCoords[to];
    return Matrix[cellFrom.Id, cellTo.Id] == 1;
  }
}