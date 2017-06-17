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

    public GameState StartNewGame()
    {
      GameState = GameStateBuilder.BuildNewGameState(PlayersCount);
      return StartNewTurn();
    }

    public GameState StartNewTurn()
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
        mazeActionResult = ApplyMazeAction(action, GameState);
      }

      startTurnResult.MazeActionResult = mazeActionResult;

      var playerHero = GameState.Heroes.First(h => h.OwnerId == GameState.CurrentPlayer.Id);
      startTurnResult.PlayerHero = playerHero;
      startTurnResult.WhereHeroCanMove = GameState.Maze.GetPassableCells(playerHero.CurrentPositionInMaze, GameState.Turn);

      return GameState;
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
}





