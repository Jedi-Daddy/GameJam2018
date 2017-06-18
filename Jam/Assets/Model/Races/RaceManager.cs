using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Model.Races
{
  public static class RaceManager
  {
    private static Dictionary<Race, RaceInfo> Races;

    static RaceManager()
    {
      Races = new Dictionary<Race, RaceInfo>
      {
        {
          Race.Elves, new RaceInfo
          {
            CellPrefabLocation = "Prefabs\\MapElements\\Tile_Elf",
            WallPrefabLocation = "Prefabs\\MapElements\\Tile_Elf_slim_v"
          }
        },
		{
			Race.Orc, new RaceInfo
			{
				CellPrefabLocation = "Prefabs\\MapElements\\Tile_Orc",
				WallPrefabLocation = "Prefabs\\MapElements\\Tile_Orc_slim_v"
			}
		},
		{
			Race.Ent, new RaceInfo
			{
				CellPrefabLocation = "Prefabs\\MapElements\\Tile_Ent",
				WallPrefabLocation = "Prefabs\\MapElements\\Tile_Ent_slim_v"
			}
		},
		{
			Race.Vampire, new RaceInfo
			{
				CellPrefabLocation = "Prefabs\\MapElements\\Tile_Vamp",
				WallPrefabLocation = "Prefabs\\MapElements\\Tile_Vamp_slim_v"
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

    //public 
  }

  public class RaceInfo
  {
    public string CellPrefabLocation;
    public string WallPrefabLocation;
  }
}
