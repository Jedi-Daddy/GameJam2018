using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    public Image ActiveFrameImage; //todo: set enable when hero is active
    public RectTransform CurrentWeaponContainer;
    public Text Nickname;

    private readonly Dictionary<Race, string> Nicks = new Dictionary<Race, string>
    {
      {Race.Elves, "L@g@Li$"},
      {Race.Ent, "Treeen2s_4_C@n@bi$"},
      {Race.Orc, "0r(rim THE GLu20n0u$"},
      {Race.Vampire, "M0$$Fer@2"}
    };
    
    public void UpdateHero(int maxHp, Player player, Hero hero)
    {
      _maxHp = maxHp;
      SetHpValue(hero.HitPoints);
      SetEnergyPointsCount(player.ActionPoints);
      SetAvatar(hero.Race);
      SetWeaponIcon(player.Slot);
      SetNick(hero.Race);
    }

    public void SetNick(Race race)
    {
      if (Nickname == null)
        return;

      Nickname.text = Nicks[race];
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

    public void SetWeaponIcon(ItemSlot item)
    {
      if (CurrentWeaponContainer == null)
        return;

      if (CurrentWeaponContainer.childCount > 0)
        for (var i = 0; i < CurrentWeaponContainer.childCount; i++)
          Destroy(CurrentWeaponContainer.GetChild(i).gameObject);

      if (item == null)
        return;

      var prefabName = string.Empty;

      var type = HeroHelper.GetActiveItemType(item);

      switch (type)
      {
        case WeaponType.Anh:
          prefabName = "Prefabs\\Anh";
          break;

        case WeaponType.Sword:
          prefabName = "Prefabs\\Sword";
          break;

        case WeaponType.Banana:
          prefabName = "Prefabs\\Banana";
          break;
      }

      var weaponTamplate = Resources.Load<GameObject>(prefabName);
      var weaponObj =  Instantiate<GameObject>(weaponTamplate, CurrentWeaponContainer);
      weaponObj.transform.localPosition = Vector3.zero;
    }

    public void SetActiveFrame(bool isActive)
    {
      if (ActiveFrameImage == null)
        return;

      ActiveFrameImage.enabled = isActive;
    }
  }
}
