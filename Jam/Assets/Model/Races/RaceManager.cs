using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Model.Races
{
  public static class RaceManager
  {
    private static Dictionary<Race, RaceInfo> Races;

    private static Dictionary<ChestOpeningResultType, ChestVisualInfo> ChestTypes = new Dictionary
      <ChestOpeningResultType, ChestVisualInfo>
    {
      {
        ChestOpeningResultType.Weapon, new ChestVisualInfo
        {
          PrefabLocation = "Prefabs\\weaponChest"
        }
      },
      {
        ChestOpeningResultType.Anh, new ChestVisualInfo
        {
          PrefabLocation = "Prefabs\\anhChest"
        }
      },
      {
        ChestOpeningResultType.Ruby, new ChestVisualInfo
        {
          PrefabLocation = "Prefabs\\goldChest"
        }
      }
    };

    static RaceManager()
    {
      Races = new Dictionary<Race, RaceInfo>
      {
        {
          Race.Elves, new RaceInfo
          {
            CellPrefabLocation = "Prefabs\\MapElements\\Tile_Elf",
            WallPrefabLocation = "Prefabs\\MapElements\\Tile_Elf_slim_v",
            HeroPrefabLocation ="Prefabs\\Hero"
          }
        },
        {
          Race.Orc, new RaceInfo
          {
            CellPrefabLocation = "Prefabs\\MapElements\\Tile_Orc",
            WallPrefabLocation = "Prefabs\\MapElements\\Tile_Orc_slim_v",
            HeroPrefabLocation ="Prefabs\\Hero"
          }
        },
        {
          Race.Ent, new RaceInfo
          {
            CellPrefabLocation = "Prefabs\\MapElements\\Tile_Ent",
            WallPrefabLocation = "Prefabs\\MapElements\\Tile_Ent_slim_v",
            HeroPrefabLocation ="Prefabs\\Hero"
          }
        },
        {
          Race.Vampire, new RaceInfo
          {
            CellPrefabLocation = "Prefabs\\MapElements\\Tile_Vamp",
            WallPrefabLocation = "Prefabs\\MapElements\\Tile_Vamp_slim_v",
            HeroPrefabLocation = "Prefabs\\Hero"
          }
        }

      };
    }

    public static string GetCellPrefabLocation(Race race)
    {
      return Races[race].CellPrefabLocation;
    }

    public static string GetWallPrefabLocation(Race race)
    {
      return Races[race].WallPrefabLocation;
    }

    public static string GetHeroPrefabLocation(Race race)
    {
      return Races[race].HeroPrefabLocation;
    }

    public static string GetChestPrefabLocation(ChestOpeningResultType chestType)
    {
      return ChestTypes[chestType].PrefabLocation;
    }

    //public 
  }

  internal class ChestVisualInfo
  {
    public string PrefabLocation;
  }

  public class RaceInfo
  {
    public string CellPrefabLocation;
    public string WallPrefabLocation;
    public string HeroPrefabLocation;
  }
}
