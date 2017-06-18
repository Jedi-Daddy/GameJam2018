using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assets.Model.Maze
{
  public class SegmentSpecial
  {
    public Race RaceType = Race.Elves;
    public List<MazeSegmentEffect> SegmentEffects;
  }

  public class MazeSegment
  {
    public int[,] Matrix;
    public SegmentSpecial SegmentSpecial;

    public List<CellInfo> Cells; 
    private readonly Dictionary<int, CellInfo> _cellsById;
    private readonly Dictionary<Point, CellInfo> _cellsByCoords;

    public static readonly Point HeroLocation = new Point(2,2);
    public static readonly List<Point> ChestsPossibleLocations = new List<Point>
    {
      new Point(1, 1),
      new Point(1, 3),
      new Point(3, 3),
      new Point(3, 1)
    };

    public MazeSegment(MazeSegment segment)
    {
      Matrix = segment.Matrix;
      Cells = new List<CellInfo>(segment.Cells);
      _cellsById = new Dictionary<int, CellInfo>();
      _cellsByCoords = new Dictionary<Point, CellInfo>();
      Cache();
    }
  
    public MazeSegment(string templateLocation)
    {
      var fileReader = new StreamReader(File.OpenRead(templateLocation));
      string textLine = null;
      int lineNumber = 0;
      Cells = new List<CellInfo>();
      _cellsById = new Dictionary<int, CellInfo>();
      _cellsByCoords = new Dictionary<Point, CellInfo>();
      while ((textLine = fileReader.ReadLine()) != null)
      {
        var line = textLine.Split(',');
        if(Matrix == null)
          Matrix = new int[line.Length, line.Length];//25x25
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
        var cellToAdd = new CellInfo()
        {
          Id = i,
          Coords = new Point
          {
            X = i%mazeSideLength,
            Y = i/mazeSideLength
          }
        };
        Cells.Add(cellToAdd);
      }
      Cache();
    }

    public void Cache()
    {
      foreach (var cellInfo in Cells)
      {
        _cellsById.Add(cellInfo.Id, cellInfo);
        _cellsByCoords.Add(cellInfo.Coords, cellInfo);
      }
    }

    public bool CanPass(Point from, Point to)
    {
      if(Math.Abs(from.X - to.X) > 1 || Math.Abs(to.Y - from.Y) > 1 ||
        (Math.Abs(from.X - to.X) == 1 && Math.Abs(to.Y - from.Y) == 1))
        return false;
      var cellFrom = _cellsByCoords[from];
      var cellTo = _cellsByCoords[to];
      return Matrix[cellFrom.Id, cellTo.Id] == 1;
    }

    public CellInfo GetCellByCoord(Point coords)
    {
      return _cellsByCoords[coords];
    }

    public CellInfo GetCellById(int id)
    {
      return _cellsById[id];
    }

    public bool HasEffect(MazeSegmentEffectType type, int currentGameTurn)
    {
      return SegmentSpecial != null
             && SegmentSpecial.SegmentEffects != null
             && SegmentSpecial.SegmentEffects.Any(se => se.EffectType == type && se.TurnUntil >= currentGameTurn);
    }

    public MazeSegment Clone()
    {
      return new MazeSegment(this);
    }
  }

  public class SegmentEffect
  {
  }
}