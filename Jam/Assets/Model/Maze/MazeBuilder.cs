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

    public static Maze BuildNew(List<Player> players)
    {
      var result = new Maze(players.Count) { Segments = new List<MazeSegment>(players.Count) };
      for (int i = 0; i < players.Count; i++)
      {
        result.Segments.Add(BuildSegment(players[i]));
      }
      return result;
    }

    public static MazeSegment BuildSegment(Player player)
    {
      var template = _segmentTemplates[_random.Next(0, _segmentTemplates.Count)];
      var newtemplate = template.Clone();
      newtemplate.SegmentSpecial = new SegmentSpecial
      {
        SegmentEffects = new List<MazeSegmentEffect>(),
        RaceType = player.PlayerRace
      };

      return newtemplate;
    }
  }
}
