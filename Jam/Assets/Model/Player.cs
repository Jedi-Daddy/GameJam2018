using System.Collections.Generic;
using System.Linq;
using Assets.Model.Cards;

namespace Assets.Model
{
  public class Player
  {
    public int Id;
    public bool IsDead;
    public int ActionPoints;
    public List<Card> Cards;
    public ItemSlot Slot;
    public int RubyAmmount;
    public Race PlayerRace;
    public string HeroName;

    public Card ActiveCard
    {
      get { return Cards.FirstOrDefault(c => c.IsActive); }
    }
  }
}