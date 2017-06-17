using Assets.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewModel
{
  public class GameUIView : MonoBehaviour
  {
    private GameState _currentState;
    private int _curentPlayerId;
    private int _maxHp;

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
    }
    
    public void SetHpValue(int currentHP)
    {
      var value = (float) currentHP/_maxHp;

      HpProgressBarSlider.value = value;
    }

    public void SetTimerValue(string timerValue)
    {
      TimerText.text = timerValue;
    }

    public void SetEnergyPointsCount(int points)
    {
      EnergyPointsText.text = points.ToString();
    }
  }
}
