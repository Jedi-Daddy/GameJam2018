using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Model;

namespace Assets.Scripts
{
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
  }
}
