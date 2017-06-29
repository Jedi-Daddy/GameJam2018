using Assets.Model;
using Assets.Model.Cards;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
  class CardListener : MonoBehaviour
  {
    public Card Card;
    public Text Description;

    public void SetDescription()
    {
      if (Description == null)
        return;

      Description.text = string.Format("{0}: +{1}", Card.Type == CardType.Attack ? "Атака" : "Защита", Card.Power);
    }

    public void OnClick()
    {
      if (Card.IsActive)
        GameManager.Instance.DeactivateCard(Card);
      else
        GameManager.Instance.ActivateCard(Card);
    }
  }
}
