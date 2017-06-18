using Assets.Model;
using UnityEngine;

namespace Assets.Scripts.ViewModel
{
  public class GameManagerInitializer : MonoBehaviour
  {
    public GameViewManager GameUI;

    private void Start()
    {
      GameManager.Instance.NewTurn += GameUI.SetState;
      GameManager.Instance.StartNewGame();
    }
  }
}
