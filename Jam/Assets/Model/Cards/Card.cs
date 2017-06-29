namespace Assets.Model.Cards
{
  public class Card
  {
    public CardType Type;
    public int Power;

    public Card()
    {
      
    }

    public Card(Card card)
    {
      Type = card.Type;
      Power = card.Power;
    }
    
    public bool IsActive { get; set; }
  }
}