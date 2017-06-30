using System.Collections.Generic;
using Assets.Model;
using UnityEngine;

namespace Assets.Scripts.ViewModel
{
  public class GameManagerInitializer : MonoBehaviour
  {
    public GameViewManager GameUI;

      //{Race.Elves, "L@g@Li$"},
      //{Race.Ent, "Treeen2s_C@n@bi$"},
      //{Race.Orc, "0r(rim THE GLu20n0u$"},
      //{Race.Vampire, "M0$$Fer@2"}

    private void Start()
    {
      GameManager.Instance.NewTurn += GameUI.SetState;
      GameManager.Instance.ActionPointUsed += GameUI.SetState;
      GameManager.Instance.StartNewGame(new List<Player>
      {
        new Player
        {
          PlayerRace = Race.Elves,
          HeroName = "L@g@Li$"
        },
         new Player
        {
          PlayerRace = Race.Ent,
          HeroName = "Treeen2s_C@n@bi$"
        },
         new Player
        {
          PlayerRace = Race.Vampire,
          HeroName = "M0$$Fer@2"
        },
         new Player
        {
          PlayerRace = Race.Orc,
          HeroName = "0r(rim THE GLu20n0u$"
        }
      });
      GameUI.Initialize(GameManager.Instance.GameState);
    }
  }
}
