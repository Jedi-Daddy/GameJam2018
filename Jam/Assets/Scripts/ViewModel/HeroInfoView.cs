using Assets.Model;
using Assets.Model.Maze.MazeObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewModel
{
  public class HeroInfoView : MonoBehaviour
  {
    private int _maxHp;
    public RectTransform AvatarParent;
    public Slider HpProgressBarSlider;
    public Text HpCountText;
    public Text EnergyPointsText;
    
    public void UpdateHero(int maxHp, Player player, Hero hero)
    {
      _maxHp = maxHp;
      SetHpValue(hero.HitPoints);
      SetEnergyPointsCount(player.ActionPoints);
      SetAvatar(hero.Race);
    }

    public void SetHpValue(int currentHp)
    {
      if (HpProgressBarSlider == null || HpCountText == null)
        return;

      var value = (float)currentHp / _maxHp;

      HpCountText.text = string.Format("{0}/{1}", currentHp, _maxHp);
      HpProgressBarSlider.value = value;
    }

    public void SetEnergyPointsCount(int points)
    {
      if (EnergyPointsText == null)
        return;

      EnergyPointsText.text = points.ToString();
    }

    public void SetAvatar(Race race)
    {
      if (AvatarParent == null)
        return;

      if(AvatarParent.childCount > 0)
        for (var i = 0; i < AvatarParent.childCount; i++)
          Destroy(AvatarParent.GetChild(i).gameObject);

      var avatarObj = Resources.Load<GameObject>(HeroHelper.GetAvatarName(race));

      Object.Instantiate(avatarObj, AvatarParent);
    }
  }
}
