using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Model.Cards;
using Assets.Model.Maze;
using Assets.Model.Maze.MazeObjects;
using Assets.Model.Maze.MazeObjects.Chest;

namespace Assets.Model
{
  class GameStateBuilder
  {
    public static GameState BuildNewGameState(List<Player> players)
    {
      var gameState = new GameState
      {
        Turn = -1,
        Heroes = new List<Hero>(),
        Chests = new List<Chest>(),
        Players = new List<Player>()
      };

      var chestsPlacer = new ChestsPlacer();
      gameState.MaxHitPoints = 50;
      for (var i = 0; i < players.Count; i++)
      {
        var currentPlayer = players[i];
        currentPlayer.Id = i;
        var cards = new List<Card>();
        CardDeck.TryAddCards(cards);
        players[i].Cards = cards;
        gameState.Players.Add(players[i]);

        gameState.Heroes.Add(new Hero
        {
          OwnerId = i,
          HitPoints = gameState.MaxHitPoints,
          Race = currentPlayer.PlayerRace,
          Name = currentPlayer.HeroName,
          CurrentPositionInMaze = new LocationInMaze
          {
            SegmentId = i,
            CoordsInSegment = MazeSegment.HeroLocation
          },
        });
        gameState.Chests.AddRange(chestsPlacer.GetChestForSegment(i));
      }

      gameState.Maze = MazeBuilder.BuildNew(players);
      
      foreach (var hero in gameState.Heroes)
      {
        gameState.Maze.AddMazeObject(hero);
      }

      foreach (var chest in gameState.Chests)
      {
        gameState.Maze.AddMazeObject(chest);
      }
      
      return gameState;
    }

    private class ChestsPlacer
    {
      private static readonly List<Chest> ChestsToPlace = new List<Chest>
      {
        new AnhChest {Anh = new Anh {HealingPower = 50}, ChestResultType = ChestOpeningResultType.Anh, IsPassable = true},
        new AnhChest {Anh = new Anh {HealingPower = 25}, ChestResultType = ChestOpeningResultType.Anh, IsPassable = true},
        new WeaponChest {Weapon = new Weapon {Damage = 40},ChestResultType = ChestOpeningResultType.Weapon, IsPassable = true},
        new WeaponChest {Weapon = new Weapon {Damage = 20},ChestResultType = ChestOpeningResultType.Weapon, IsPassable = true},
      };

      private readonly List<Chest> _chestsLeftToPlace;

      private readonly Random _chestRandom;

      public ChestsPlacer()
      {
        _chestRandom = new Random();
        _chestsLeftToPlace = new List<Chest>(ChestsToPlace);
      }

      public IEnumerable<Chest> GetChestForSegment(int ownerId)
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
            RubyAmount = 1,
            IsPassable = true
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
