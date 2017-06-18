using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Model.Maze.MazeObjects.Chest;
using UnityEngine;

namespace Assets.Scripts
{
  class ChestTakingListener : MonoBehaviour
  {
    public Chest Chest;

    public void Delete()
    {
      Chest.Take -= Delete;
      Destroy(this.gameObject);
    }
  }
}
