using Assets.Model;
using UnityEngine;

namespace Assets.Scripts
{
  class CardListener : MonoBehaviour
  {
    public Card Card;

    public void OnClick()
    {
      if (Card.IsActive)
        GameManager.Instance.DeactivateCard(Card);
      else
        GameManager.Instance.ActivateCard(Card);
    }
  }
}
