using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Model.Maze;
using Assets.Model.Maze.MazeObjects;
using Assets.Model.Maze.MazeObjects.Chest;
using Assets.Scripts;

namespace Assets.Model
{
  public class GameManager
  {
    public static readonly GameManager Instance = new GameManager();

    private GameManager()
    {
    }

    public GameState GameState;
    public const int PlayersCount = 4;
    public const int ActionCountDefault = 3;
    public event Action<GameState> NewTurn; 

    private static readonly List<IMazeActionApplier> ActionAppliers = new List<IMazeActionApplier>
    {
      new BlockMazeActionApplier(),
      new TeleportMazeActionApplier()
    };

    public void StartNewGame()
    {
      GameState = GameStateBuilder.BuildNewGameState(PlayersCount);
      StartNewTurn();
      //return GameState;
    }

    public void StartNewTurn()
    {
      ++GameState.Turn;
      if (GameState.CurrentPlayer.IsDead)
        ++GameState.Turn;

      GameState.CurrentPlayer.ActionPoints = ActionCountDefault;
      if (GameState.TurnForMazeAction)
      {
        var action = GameState.Maze.GetAction();
        ApplyMazeAction(action);
      }

      //var playerHero = GameState.Heroes.First(h => h.OwnerId == GameState.CurrentPlayer.Id);
      //startTurnResult.PlayerHero = playerHero;
      //startTurnResult.WhereHeroCanMove = GameState.Maze.GetPassableCells(playerHero.CurrentPositionInMaze, GameState.Turn);
      NewTurn(GameState);
    }

    private void ApplyMazeAction(MazeActionType action)
    {
      foreach (var actionApplier in ActionAppliers)
        actionApplier.ApplyAction(GameState, action);
    }

    public enum HeroMoveResult
    {
      Default = 0,
      StandsOnChest = 1,
    }

    public void MoveHero(LocationInMaze positionToMove)
    {
      var curentPlayer = GameState.CurrentPlayer;
      var heroToMove = GameState.Heroes.First(h => h.OwnerId == curentPlayer.Id);
      if (curentPlayer.ActionPoints <= 0)
        return;
      //We dont validate here
      if(!GameState.Maze.CanPass(heroToMove.CurrentPositionInMaze, positionToMove, GameState.Turn))
        return;
      heroToMove.Move(positionToMove);
      curentPlayer.ActionPoints --;
      if(curentPlayer.ActionPoints == 0)
        StartNewTurn();
      //var objectsStandsOn = GameState.Maze.GetObjects(heroToMove.CurrentPositionInMaze);
      //if (objectsStandsOn != null && objectsStandsOn.Any(o => o.GetType().IsAssignableFrom(typeof(Chest))))
      //  return;
      //return HeroMoveResult.Default;
    }

    public void AttackHero(LocationInMaze heroPosition)
    {
      var curentPlayer = GameState.CurrentPlayer;
      if (curentPlayer.Slot == null || curentPlayer.Slot.Weapon == null)
        return;
      var damage = curentPlayer.Slot.Weapon.Damage;
      var objectsOnCell = GameState.Maze.GetObjects(heroPosition);
      if (objectsOnCell == null || !objectsOnCell.Any())
        return;

      var heroObj = objectsOnCell.FirstOrDefault(h => h.GetType() == typeof (Hero));
      if (heroObj == null)
        return;

      if(!GameState.Maze.CanSee(GameState.CurrentHero.CurrentPositionInMaze, heroPosition))
        return;

      var heroToAttack = heroObj as Hero;
      heroToAttack.HitPoints -= damage;
      curentPlayer.Slot = null;
      if (heroToAttack.HitPoints <= 0)
      {
        var playerOwner = GameState.Players[heroToAttack.OwnerId.Value];
        if (playerOwner.Slot != null && playerOwner.Slot.Anh != null)
        {
          heroToAttack.HitPoints = playerOwner.Slot.Anh.HealingPower;
          playerOwner.Slot = null;
        }
        else
        {
          playerOwner.IsDead = true;
          heroToAttack.Die();
        }
      }

    }

    public ChestOpeningResult OpenChest()
    {
      var curentPlayer = GameState.CurrentPlayer;
      var hero = GameState.Heroes.First(h => h.OwnerId == curentPlayer.Id);
      var chest = GameState.Maze.GetObjects(hero.CurrentPositionInMaze).FirstOrDefault(o => o.GetType() == typeof (Chest)) as Chest;
      return chest.OpenChest();
    }
  }

  internal class TeleportMazeActionApplier : IMazeActionApplier
  {
    public void ApplyAction(GameState state, MazeActionType actionType)
    {
      if(actionType != MazeActionType.Teleport)
        return;
      var random = new Random();
      var locationToTeleport = new LocationInMaze
      {
        SegmentId = random.Next(0, state.Maze.Segments.Count),
        CoordsInSegment = new Point(random.Next(0, 5), random.Next(0, 5)),
      };
      var heroVictim = state.Heroes[random.Next(0, state.Heroes.Count)];
      heroVictim.Move(locationToTeleport);
    }
  }

  internal class BlockMazeActionApplier : IMazeActionApplier
  {
    public void ApplyAction(GameState state, MazeActionType actionType)
    {
      if(actionType != MazeActionType.Lock)
        return;

      var random = new Random();
      state.Maze.Segments[random.Next(0,state.Maze.Segments.Count)].SegmentSpecial.SegmentEffects.Add(new MazeSegmentEffect
      {
        EffectType = MazeSegmentEffectType.Blocked,
        TurnUntil = state.Turn + 2 * state.Players.Count(p=>!p.IsDead)
      });
    }
  }

  public class ChestOpeningResult
  {
    public ChestOpeningResultType ChestOpeningResultType;
    public int Rubys;
    public Weapon Weapon;
    public Anh Anh;
  }
  public enum ChestOpeningResultType
  {
    Ruby = 0,
    Weapon = 1,
    Anh = 2,
    Banana = 3
  }
}





