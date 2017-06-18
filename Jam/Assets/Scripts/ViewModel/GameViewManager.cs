using Assets.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewModel
{
  public class GameViewManager : MonoBehaviour
  {
    private GameState _currentState;
    
    public RectTransform FieldContainer;

    public HeroInfoView CurrentHeroInfo;
    public HeroInfoView[] HeroesInfo;

    public Text TimerText;

    public void SetState(GameState state)
    {
      _currentState = state;

      CurrentHeroInfo.UpdateHero(state.MaxHitPoints, state.CurrentPlayer, state.CurrentHero);

      GameFieldDrawer.DrawField(FieldContainer, state.Maze);
      var heroes = state.Heroes;
      for (int i = 0, j = 0; i < heroes.Count;i++)
      {
        var hero = state.Heroes[i];

        if (hero != state.CurrentHero)
        {
          HeroesInfo[j].UpdateHero(state.MaxHitPoints, state.CurrentPlayer, hero);
          j++;
        }

        if(hero.HitPoints > 0)
          GameFieldDrawer.DrawHero(FieldContainer, hero);
      }

      foreach (var chest in state.Chests)
      {
        GameFieldDrawer.DrawChest(FieldContainer, chest);
      }
    }
  }
}
