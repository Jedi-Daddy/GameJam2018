using Assets.Model;
using UnityEngine;

namespace Assets.Scripts.ViewModel
{
  public class GameManagerInitializer : MonoBehaviour
  {
    public GameViewManager GameUI;

    private void Start()
    {
      var state = GameManager.Instance.StartNewGame();
      GameUI.SetState(state);
    }
  }
}
