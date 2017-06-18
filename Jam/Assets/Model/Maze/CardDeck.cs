using System;
using System.Collections.Generic;

namespace Assets.Model.Maze
{
  public static class CardDeck
  {
    private static List<Card> CardTypes = new List<Card>
    {
      new Card {Type = CardType.Attack, Power = 3},
      new Card {Type = CardType.Attack, Power = 5},
      new Card {Type = CardType.Attack, Power = 7},
      new Card {Type = CardType.Attack, Power = 9},
      new Card {Type = CardType.Attack, Power = 11},
      new Card {Type = CardType.Defence, Power = 1},
      new Card {Type = CardType.Defence, Power = 2},
      new Card {Type = CardType.Defence, Power = 3},
      new Card {Type = CardType.Defence, Power = 4},
      new Card {Type = CardType.Defence, Power = 5},
    };

    public static List<Card> GetCards(int count)
    {
      var result = new List<Card>();
      var rand = new Random();
      for (int i = 0; i < count; i++)
      {
        result.Add(CardTypes[rand.Next(0, CardTypes.Count)]);
      }
      return result;
    } 

  }
}
