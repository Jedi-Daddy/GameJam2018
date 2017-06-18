using System;
using System.Collections.Generic;
using System.IO;

namespace Assets.Model.Maze
{
  public static class MazeBuilder
  {
    public const string TemplatesLocation = @"MazeTemplates";
    private static List<MazeSegment> _segmentTemplates;
    private static Random _random;

    static MazeBuilder()
    {
      _random = new Random();
      _segmentTemplates = new List<MazeSegment>();
      foreach (var filePath in Directory.GetFiles(TemplatesLocation))
      {
        _segmentTemplates.Add(new MazeSegment(filePath));
      }
    }

    public static Maze BuildNew(int playersCount)
    {
      var result = new Maze(playersCount) { Segments = new List<MazeSegment>(playersCount) };
      for (int i = 0; i < playersCount; i++)
      {
        result.Segments.Add(BuildSegment(i));
      }
      return result;
    }

    public static MazeSegment BuildSegment(int playerId)
    {
      var template = _segmentTemplates[_random.Next(0, _segmentTemplates.Count)];
      var newtemplate = template.Clone();
      newtemplate.SegmentSpecial = new SegmentSpecial
      {
        SegmentEffects = new List<MazeSegmentEffect>(),
        RaceType = (Race)playerId
      };

      return newtemplate;
    }
  }
}
