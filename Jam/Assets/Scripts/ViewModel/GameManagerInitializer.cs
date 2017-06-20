using System.Collections.Generic;
using Assets.Model;
using UnityEngine;

namespace Assets.Scripts.ViewModel
{
  public class GameManagerInitializer : MonoBehaviour
  {
    public GameViewManager GameUI;

    private void Start()
    {
      GameManager.Instance.NewTurn += GameUI.SetState;
      GameManager.Instance.ActionPointUsed += GameUI.SetState;
      GameManager.Instance.StartNewGame(new List<Player>
      {
        new Player
        {
          PlayerRace = Race.Ent,
          HeroName = "John Doe"
        },
         new Player
        {
          PlayerRace = Race.Ent,
          HeroName = "Жопа с ручкой"
        },
         new Player
        {
          PlayerRace = Race.Vampire,
          HeroName = "Жопа с ручкой 2"
        },
         new Player
        {
          PlayerRace = Race.Orc,
          HeroName = "Жопа с ручкой 3"
        }
      });
      GameUI.Initialize(GameManager.Instance.GameState);
    }
  }
}
