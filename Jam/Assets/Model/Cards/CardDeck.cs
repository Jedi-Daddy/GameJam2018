using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Model.Cards
{
  public static class CardDeck
  {
    private const int CardsCount = 4;
    private static readonly Random Rand = new Random();
    private static readonly List<Card> CardTypes = new List<Card>
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
      
      for (int i = 0; i < count; i++)
      {
        result.Add(CardTypes[Rand.Next(0, CardTypes.Count)]);
      }
      return result;
    }

    public static void TryAddCards(List<Card> currentCards)
    {
      if (currentCards.Count >= CardsCount)
        return;
      var defensiveCardsCount = currentCards.Count(c => c.Type == CardType.Defence);
      var attakingCardscount = currentCards.Count(c => c.Type == CardType.Attack);
      var countToAdd = CardsCount - currentCards.Count;
      for (var i = 0; i < countToAdd; )
      {
        var newCard = CardTypes[Rand.Next(0, CardTypes.Count)];
        if (newCard.Type == CardType.Defence)
        {
          if(defensiveCardsCount >= 2)
            continue;
          currentCards.Add(new Card(newCard));
          defensiveCardsCount++;
        }

        if (newCard.Type == CardType.Attack)
        {
          if (attakingCardscount >= 2)
            continue;
          currentCards.Add(new Card(newCard));
          attakingCardscount++;
        }
        i++;
      }
    }

  }
}
