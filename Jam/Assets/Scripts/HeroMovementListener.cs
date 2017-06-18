using Assets.Model.Maze;
using UnityEngine;

namespace Assets.Scripts
{
  public class HeroMovementListener : MonoBehaviour
  {
    public void Move(LocationInMaze newLocation)
    {
      transform.localPosition = CoordsUtility.GetUiPosition(newLocation);
    }

    public void Die()
    {
      Destroy(this.gameObject);
    }
  }
}
