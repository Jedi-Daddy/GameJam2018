using System.Collections.Generic;
using System.Linq;
using Assets.Model.Maze;
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

    private static readonly List<IMazeActionApplier> ActionAppliers = new List<IMazeActionApplier>
    {

    };

    public StartTurnResult StartNewGame()
    {
      GameState = GameStateBuilder.BuildNewGameState(PlayersCount);
      var state = StartNewTurn();
      state.CurentState = GameState;

      return state;
    }

    public StartTurnResult StartNewTurn()
    {
      ++GameState.Turn;
      StartTurnResult startTurnResult = null;
      if (GameState.CurrentPlayer.IsDead)
        ++GameState.Turn;
      startTurnResult = (startTurnResult ?? new StartTurnResult());
      GameState.CurrentPlayer.ActionPoints = ActionCountDefault;

      MazeActionResult mazeActionResult = null;
      if (GameState.TurnForMazeAction)
      {
        var action = GameState.Maze.GetAction();
        mazeActionResult = ApplyMazeAction(action);
      }

      startTurnResult.MazeActionResult = mazeActionResult;

      var playerHero = GameState.Heroes.First(h => h.OwnerId == GameState.CurrentPlayer.Id);
      startTurnResult.PlayerHero = playerHero;
      startTurnResult.WhereHeroCanMove = GameState.Maze.GetPassableCells(playerHero.CurrentPositionInMaze, GameState.Turn);

      return startTurnResult;
    }

    private MazeActionResult ApplyMazeAction(MazeActionType action)
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
      heroToMove.Move(positionToMove);
      var objectsStandsOn = GameState.Maze.GetObjects(heroToMove.CurrentPositionInMaze);
      if (objectsStandsOn != null && objectsStandsOn.Any(o => o.GetType().IsAssignableFrom(typeof(Chest))))
        return HeroMoveResult.StandsOnChest;
      return HeroMoveResult.Default;
    }

    public ChestOpeningResult OpenChest()
    {
      var curentPlayer = GameState.CurrentPlayer;
      var hero = GameState.Heroes.First(h => h.OwnerId == curentPlayer.Id);
      var chest = GameState.Maze.GetObjects(hero.CurrentPositionInMaze).FirstOrDefault(o => o.GetType() == typeof (Chest)) as Chest;
      return chest.OpenChest();
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





