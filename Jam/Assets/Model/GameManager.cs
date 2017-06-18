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
    public const int CardsCount = 4;
    public event Action<GameState> NewTurn;
    public event Action<GameState> ActionPointUsed; 

    private static readonly List<IMazeActionApplier> ActionAppliers = new List<IMazeActionApplier>
    {
      new BlockMazeActionApplier(),
      new TeleportMazeActionApplier(),
      new RebuildMazeSegmentActionApplier(),
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

      if (GameState.CurrentPlayer.Cards.Count < CardsCount)
      {
        GameState.CurrentPlayer.Cards.AddRange(CardDeck.GetCards(CardsCount - GameState.CurrentPlayer.Cards.Count));
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

    public void MoveHero(LocationInMaze positionToMove)
    {
      var curentPlayer = GameState.CurrentPlayer;
      var heroToMove = GameState.Heroes.First(h => h.OwnerId == curentPlayer.Id);
      if (curentPlayer.ActionPoints <= 0)
        return;
      //We dont validate here
      if(!GameState.Maze.CanPass(heroToMove.CurrentPositionInMaze, positionToMove, GameState.Turn))
        return;

      var objectsOnCell = GameState.Maze.GetObjects(positionToMove);
      var chests = new List<Chest>();
      foreach (var mo in objectsOnCell)
      {
        if(mo as Chest != null)
          chests.Add((Chest)mo);
      }
        

      var chest = chests.FirstOrDefault();
      if (chest != null && chest.OwnerId!= GameState.CurrentPlayer.Id) 
      {
        var openChestResult = chest.OpenChest();
        if (openChestResult.Rubys > 0)
          GameState.CurrentPlayer.RubyAmmount += openChestResult.Rubys;
        if(openChestResult.Weapon != null)
          GameState.CurrentPlayer.Slot = new ItemSlot(openChestResult.Weapon);
        if (openChestResult.Anh != null)
          GameState.CurrentPlayer.Slot = new ItemSlot(openChestResult.Anh);
      }

      heroToMove.Move(positionToMove);
      OnAction();
      if(curentPlayer.ActionPoints == 0)
        StartNewTurn();
      //var objectsStandsOn = GameState.Maze.GetObjects(heroToMove.CurrentPositionInMaze);
      //if (objectsStandsOn != null && objectsStandsOn.Any(o => o.GetType().IsAssignableFrom(typeof(Chest))))
      //  return;
      //return HeroMoveResult.Default;
    }

    public void OnAction()
    {
      GameState.CurrentPlayer.ActionPoints--;
      if (ActionPointUsed != null)
        ActionPointUsed(GameState);
    }

    public void ClickHero(Hero hero)
    {
      if(GameState.CurrentHero == hero)
        return;
      else
      {
        AttackHero(hero);
      }
    }

    public void ActivateCard(Card card)
    {
      GameState.CurrentPlayer.Cards.ForEach(c=>c.IsActive = false);
      card.IsActive = true;
      if(card.Type == CardType.Defence)
        HealWithCard(card);
    }

    public void DeactivateCard(Card card)
    {
      GameState.CurrentPlayer.Cards.ForEach(c => c.IsActive = false);
    }

    public void AttackHero(Hero heroToAttack)
    {
      var curentPlayer = GameState.CurrentPlayer;

      if (!GameState.Maze.CanSee(GameState.CurrentHero.CurrentPositionInMaze, heroToAttack.CurrentPositionInMaze))
        return;

      if (curentPlayer.ActiveCard != null)
      {
        AttackHeroWithCard(heroToAttack, curentPlayer.ActiveCard);
      }
      else
      {
        if (curentPlayer.Slot == null || curentPlayer.Slot.Weapon == null)
          return;

        var damage = curentPlayer.Slot.Weapon.Damage;
        curentPlayer.Slot = null;
        ApplyDamage(heroToAttack, damage);
      }
    }

    public void AttackHeroWithCard(Hero heroToAttack, Card card)
    {
      if(card.Type != CardType.Attack)
        return;

      var damage = card.Power;

      GameState.CurrentPlayer.Cards.Remove(card);
      ApplyDamage(heroToAttack, damage);
    }

    public void HealWithCard(Card card)
    {
      if(GameState.CurrentHero.HitPoints == 50)
        return;

      GameState.CurrentPlayer.Cards.Remove(card);
      ApplyHealing(GameState.CurrentHero, card.Power);
    }

    public void ApplyHealing(Hero hero, int healing)
    {
      hero.HitPoints = Math.Min(hero.HitPoints + healing, 50);
    }

    public void ApplyDamage(Hero hero, int damage)
    {
      hero.HitPoints -= damage;

      if (hero.HitPoints <= 0)
      {
        var playerOwner = GameState.Players[hero.OwnerId.Value];
        if (playerOwner.Slot != null && playerOwner.Slot.Anh != null)
        {
          hero.HitPoints = playerOwner.Slot.Anh.HealingPower;
          playerOwner.Slot = null;
        }
        else
        {
          playerOwner.IsDead = true;
          hero.Die();
        }
      }
    }

    //public ChestOpeningResult OpenChest()
    //{
    //  var curentPlayer = GameState.CurrentPlayer;
    //  var hero = GameState.Heroes.First(h => h.OwnerId == curentPlayer.Id);
    //  var chest = GameState.Maze.GetObjects(hero.CurrentPositionInMaze).FirstOrDefault(o => o.GetType() == typeof (Chest)) as Chest;
    //  return chest.OpenChest();
    //}
  }

  internal class RebuildMazeSegmentActionApplier : IMazeActionApplier
  {
    public void ApplyAction(GameState state, MazeActionType actionType)
    {
      if (actionType != MazeActionType.Rebuild)
        return;
      var random = new Random();
      var segmentId = random.Next(0, state.Maze.Segments.Count);
      var mazeSegment = state.Maze.Segments[segmentId];
      state.Maze.Segments.Remove(mazeSegment);
      var newSegment = MazeBuilder.BuildSegment(segmentId);
      state.Maze.Segments.Insert(segmentId, newSegment);
      state.SegmentToRebuild = segmentId;
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





