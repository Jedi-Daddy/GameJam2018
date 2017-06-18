using Assets.Model;

namespace Assets.Scripts
{
  public enum WeaponType
  {
    Sword,
    Anh,
    Banana,
  }

  public static class HeroHelper
  {
    private const string avatarsPath = @"Prefabs\HeroesAvatars\";

    public static string GetAvatarName(Race race)
    {
      switch (race)
      {
        case Race.Elves:
          return avatarsPath + "ElfAvatar";
        case Race.Orc:
          return avatarsPath + "OrcAvatar";
        case Race.Ent:
          return avatarsPath + "EntAvatar";
        case Race.Vampire:
          return avatarsPath + "VampAvatar";
      }

      return "Default";
    }

    public static WeaponType GetActiveItemType(ItemSlot item)
    {
      if (item.Anh != null)
        return WeaponType.Anh;
      
      //if (item.Weapon.IsBanana)
      //  return WeaponType.Banana;

      return WeaponType.Sword;

    }
  }
}
