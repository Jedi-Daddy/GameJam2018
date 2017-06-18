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

        GameFieldDrawer.DrawHero(FieldContainer, hero);

        if (hero == state.CurrentHero) 
          continue;

        HeroesInfo[j].UpdateHero(state.MaxHitPoints, state.CurrentPlayer, hero);
        j++;
      }
    }
  }
}
