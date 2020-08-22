using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace LocationParentEditor
{
  public class KeywordSet
  {
    public string Name { get; set; }
    public string[] Keywords { get; set; }
  };

  public class KeywordSets
  {
    private const string LocTypeAyleidRuin = "TES4LocTypeAyleidRuin";
    private const string LocTypeBarracks = "LocTypeBarracks";
    private const string LocTypeCastle = "LocTypeCastle";
    private const string LocTypeCave = "TES4LocTypeCave";
    private const string LocTypeCity = "LocTypeCity";
    private const string LocTypeDungeon = "LocTypeDungeon";
    private const string LocTypeDwelling = "LocTypeDwelling";
    private const string LocTypeFarm = "LocTypeFarm";
    private const string LocTypeFightersGuild = "TES4LocTypeFightersGuild";
    private const string LocTypeFort = "TES4LocTypeFort";
    private const string LocTypeGuild = "LocTypeGuild";
    private const string LocTypeHabitation = "LocTypeHabitation";
    private const string LocTypeHabitationHasInn = "LocTypeHabitationHasInn";
    private const string LocTypeHouse = "LocTypeHouse";
    private const string LocTypeInn = "LocTypeInn";
    private const string LocTypeJail = "LocTypeJail";
    private const string LocTypeMagesGuild = "TES4LocTypeMagesGuild";
    private const string LocTypeMine = "LocTypeMine";
    private const string LocTypePlayerHouse = "LocTypePlayerHouse";
    private const string LocTypeSettlement = "LocTypeSettlement";
    private const string LocTypeShip = "LocTypeShip";
    private const string LocTypeShipwreck = "LocTypeShipwreck";
    private const string LocTypeStore = "LocTypeStore";
    private const string LocTypeTemple = "LocTypeTemple";
    private const string LocTypeTown = "LocTypeTown";
    private const string LocTypeVampireLair = "LocTypeVampireLair";

    public static KeywordSet AyleidRuinSet = new KeywordSet
    {
      Name = "Ayleid ruin",
      Keywords = new string[]
      {
        LocTypeAyleidRuin,
        LocTypeDungeon
      }
    };

    public static KeywordSet CaveDungeonSet = new KeywordSet
    {
      Name = "Cave dungeon",
      Keywords = new string[]
      {
        LocTypeCave,
        LocTypeDungeon
      }
    };

    public static KeywordSet CitySet = new KeywordSet
    {
      Name = "City",
      Keywords = new string[]
      {
        LocTypeCity,
        LocTypeHabitation
      }
    };

    public static KeywordSet CityWithInnSet = new KeywordSet
    {
      Name = "City with inn",
      Keywords = new string[]
      {
        LocTypeCity,
        LocTypeHabitation,
        LocTypeHabitationHasInn
      }
    };

    public static KeywordSet DungeonSet = new KeywordSet
    {
      Name = "Dungeon",
      Keywords = new string[]
      {
        LocTypeDungeon
      }
    };

    public static KeywordSet FarmSet = new KeywordSet
    {
      Name = "Farm",
      Keywords = new string[]
      {
        LocTypeFarm,
        LocTypeHabitation,
        LocTypeSettlement
      }
    };

    public static KeywordSet FightersGuild = new KeywordSet
    {
      Name = "Fighters Guild",
      Keywords = new string[]
      {
        LocTypeDwelling,
        LocTypeFightersGuild,
        LocTypeGuild
      }
    };

    public static KeywordSet FortSet = new KeywordSet
    {
      Name = "Fort",
      Keywords = new string[]
      {
        LocTypeFort,
        LocTypeDungeon
      }
    };

    public static KeywordSet HouseSet = new KeywordSet
    {
      Name = "House",
      Keywords = new string[]
      {
        LocTypeDwelling,
        LocTypeHouse
      }
    };

    public static KeywordSet InnSet = new KeywordSet
    {
      Name = "Inn",
      Keywords = new string[]
      {
        LocTypeDwelling,
        LocTypeInn,
      }
    };

    public static KeywordSet JailSet = new KeywordSet
    {
      Name = "Jail",
      Keywords = new string[]
      {
        LocTypeDwelling,
        LocTypeJail
      }
    };

    public static KeywordSet MagesGuild = new KeywordSet
    {
      Name = "Mages Guild",
      Keywords = new string[]
      {
        LocTypeDwelling,
        LocTypeGuild,
        LocTypeMagesGuild
      }
    };

    public static KeywordSet Store = new KeywordSet
    {
      Name = "Store",
      Keywords = new string[]
      {
        LocTypeStore
      }
    };

    public static KeywordSet StoreWithBed = new KeywordSet
    {
      Name = "Store with bed",
      Keywords = new string[]
      {
        LocTypeStore,
        LocTypeDwelling
      }
    };

    public static KeywordSet TempleSet = new KeywordSet
    {
      Name = "Temple",
      Keywords = new string[]
      {
        LocTypeTemple,
        LocTypeDwelling
      }
    };

    public static KeywordSet TownSet = new KeywordSet
    {
      Name = "Town",
      Keywords = new string[]
      {
        LocTypeTown,
        LocTypeHabitation
      }
    };

    public static KeywordSet TownWithInnSet = new KeywordSet
    {
      Name = "Town with inn",
      Keywords = new string[]
      {
        LocTypeTown,
        LocTypeHabitation,
        LocTypeHabitationHasInn
      }
    };
  }
}
