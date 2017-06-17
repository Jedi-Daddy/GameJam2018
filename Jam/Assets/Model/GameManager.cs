using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;

namespace Assets.Model
{
  public class GameManager
  {
    public GameState GameState;
    public const int PlayersCount = 4;
    public const int ActionCountDefault = 3;

    private List<IMazeActionApplier> ActionAppliers = new List<IMazeActionApplier>
    {

    };

    public StartTurnResult StartNewGame()
    {
      GameState = GameStateBuilder.BuildNewGameState(PlayersCount);
      return StartNewTurn();
    }

    public StartTurnResult StartNewTurn()
    {
      ++GameState.Turn;
      StartTurnResult startTurnResult = null;
      if (GameState.CurrentPlayer.IsDead)
        startTurnResult = StartNewTurn();
      startTurnResult = (startTurnResult ?? new StartTurnResult());
      GameState.CurrentPlayer.ActionPoints = ActionCountDefault;

      MazeActionResult mazeActionResult = null;
      if (GameState.TurnForMazeAction)
      {
        var action = GameState.Maze.GetAction();
        mazeActionResult = ApplyMazeAction(action, GameState);
      }

      startTurnResult.MazeActionResult = mazeActionResult;

      var playerHero = GameState.Heroes.First(h => h.OwnerId == GameState.CurrentPlayer.Id);
      startTurnResult.PlayerHero = playerHero;
      startTurnResult.WhereHeroCanMove = GameState.Maze.GetPassableCells(playerHero.CurrentPositionInMaze, GameState.Turn);

      return startTurnResult;
    }

    private MazeActionResult ApplyMazeAction(MazeActionType action, GameState gameState)
    {
      var result = new MazeActionResult();
      foreach (var actionApplier in ActionAppliers)
        actionApplier.ApplyAction(GameState, action, result);
      return result;
    }

    public enum HeroMoveResult
    {
      Default = 0,
      StandsOnChest = 1,
    }

    public HeroMoveResult MoveHero(LocationInMaze positionToMove)
    {
      var curentPlayer = GameState.CurrentPlayer;
      var heroToMove = GameState.Heroes.First(h => h.OwnerId == curentPlayer.Id);
      //We dont validate here
      //GameState.Maze.CanPass(heroeToMove.CurrentPositionInMaze, positionToMove);
      heroToMove.CurrentPositionInMaze = positionToMove;
      var objectsStandsOn = GameState.Maze.GetObjects(heroToMove.CurrentPositionInMaze);
      if (objectsStandsOn != null && objectsStandsOn.Any(o => o.GetType().IsAssignableFrom(typeof(Chest))))
        return HeroMoveResult.StandsOnChest;
      return HeroMoveResult.Default;
    }
  }


  public class PlayerAction
  {
    public int PlayerId;
    public PlayerEffect Effect;
  }

  public class SegmentAction
  {
    public int SegmentId;
    public MazeSegmentEffect SegmentEffect;
  }

  public enum MazeSegmentEffectType
  {

  }

  internal interface IMazeActionApplier
  {
    void ApplyAction(GameState state, MazeActionType actionType, MazeActionResult result);
  }

  public class MazeActionResult
  {
    public List<SegmentAction> SegmentActions;
    public List<PlayerAction> PlayerActions;

    public bool IsEmpty
    {
      get
      {
        return (SegmentActions == null || !SegmentActions.Any()) && (PlayerActions == null || !PlayerActions.Any());
      }
    }
  }

  public static class GameStateBuilder
  {
    public static GameState BuildNewGameState(int playersCount)
    {
      var gameState = new GameState();
      return gameState;
    }
  }
}





