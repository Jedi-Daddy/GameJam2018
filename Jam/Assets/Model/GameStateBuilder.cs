using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Model.Maze;
using Assets.Model.Maze.MazeObjects;
using Assets.Model.Maze.MazeObjects.Chest;
using Assets.Scripts;

namespace Assets.Model
{
  class GameStateBuilder
  {
    public static GameState BuildNewGameState(int playersCount)
    {
      var gameState = new GameState
      {
        Turn = -1,
        Maze = MazeBuilder.BuildNew(playersCount),
        Heroes = new List<Hero>(),
        Chests = new List<Chest>(),
        Players = new List<Player>()
      };
      var chestsPlacer = new ChestsPlacer();
      gameState.MaxHitPoints = 50;
      for (var i = 0; i < playersCount; i++)
      {
        gameState.Players.Add(new Player
        {
          Id = i ,
          Slot = new ItemSlot(new Weapon{Damage = 50})
        });
        gameState.Heroes.Add(new Hero
        {
          OwnerId = i,
          HitPoints = gameState.MaxHitPoints,
          Race = (Race)i,
          CurrentPositionInMaze = new LocationInMaze
          {
            SegmentId = i,
            CoordsInSegment = MazeSegment.HeroLocation
          },
        });
        gameState.Chests.AddRange(chestsPlacer.GetChestForSegment(i));
      }
      return gameState;
    }

    private class ChestsPlacer
    {
      private static readonly List<Chest> ChestsToPlace = new List<Chest>
      {
        new AnhChest {Anh = new Anh {HealingPower = 50}, ChestResultType = ChestOpeningResultType.Anh},
        new AnhChest {Anh = new Anh {HealingPower = 25}, ChestResultType = ChestOpeningResultType.Anh},
        new WeaponChest {Weapon = new Weapon {Damage = 40},ChestResultType = ChestOpeningResultType.Weapon},
        new WeaponChest {Weapon = new Weapon {Damage = 20},ChestResultType = ChestOpeningResultType.Weapon},
      };

      private readonly List<Chest> _chestsLeftToPlace;

      private readonly Random _chestRandom;

      public ChestsPlacer()
      {
        _chestRandom = new Random();
        _chestsLeftToPlace = new List<Chest>(ChestsToPlace);
      }

      public List<Chest> GetChestForSegment(int ownerId)
      {
        var chestLocationsSet = new HashSet<Point>();

        while (chestLocationsSet.Count != 2)
          chestLocationsSet.Add(MazeSegment.ChestsPossibleLocations[_chestRandom.Next(0, MazeSegment.ChestsPossibleLocations.Count)]);

        var chestLocations = chestLocationsSet.ToList();
        var result = new List<Chest>
        {
          new RubyChest
          {
            ChestResultType = ChestOpeningResultType.Ruby,
            CurrentPositionInMaze = new LocationInMaze()
            {
              SegmentId = ownerId,
              CoordsInSegment = chestLocations[0]
            },
            OwnerId = ownerId,
            RubyAmount = 1
          }
        };
        var battleChest = _chestsLeftToPlace[_chestRandom.Next(0, _chestsLeftToPlace.Count)];
        _chestsLeftToPlace.Remove(battleChest);
        battleChest.CurrentPositionInMaze = new LocationInMaze
        {
          SegmentId = ownerId,
          CoordsInSegment = chestLocations[1]
        };
        battleChest.OwnerId = ownerId;
        result.Add(battleChest);

        return result;
      }
    }
  }
}
