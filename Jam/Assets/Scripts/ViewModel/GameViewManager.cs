using Assets.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewModel
{
  public class GameViewManager : MonoBehaviour
  {
    private GameState _currentState;
    private int _curentPlayerId;
    private int _maxHp;

    public RectTransform FieldContainer;
    public HeroInfoView[] HeroesInfo;
    public Slider HpProgressBarSlider;
    public Text TimerText;
    public Text EnergyPointsText;

    public void SetState(GameState state)
    {
      _currentState = state;
      _curentPlayerId = _currentState.CurrentPlayer.Id;

      SetHpValue(_currentState.Heroes[_curentPlayerId].HitPoints);
      SetEnergyPointsCount(_currentState.CurrentPlayer.ActionPoints);
      GameFieldDrawer.DrawField(FieldContainer, state.Maze);
      foreach (var hero in state.Heroes)
      {
        GameFieldDrawer.DrawHero(FieldContainer, hero);
      }
    }
    
    public void SetHpValue(int currentHP)
    {
      if (HpProgressBarSlider == null)
        return;

      var value = (float) currentHP/_maxHp;

      HpProgressBarSlider.value = value;
    }

    public void SetTimerValue(string timerValue)
    {
      if (TimerText == null)
        return;

      TimerText.text = timerValue;
    }

    public void SetEnergyPointsCount(int points)
    {
      if (EnergyPointsText == null)
        return;

      EnergyPointsText.text = points.ToString();
    }
  }
}
