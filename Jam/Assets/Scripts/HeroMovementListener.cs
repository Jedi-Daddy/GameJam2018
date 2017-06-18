using Assets.Model.Maze;
using Assets.Model.Maze.MazeObjects;
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
