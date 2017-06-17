using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewModel
{
  public class HeroInfoView : MonoBehaviour
  {
    private int _maxHP;
    public Slider HPProgressBarSlider;
    
    public void Initialize(int maxHP)
    {
      _maxHP = maxHP;
    }

    public void UpdateHPValue(int currentHP)
    {
      var value = (float)currentHP / _maxHP;
      HPProgressBarSlider.value = value;
    }
  }
}
