using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Model;
using Assets.Model.Maze;
using Assets.Model.Maze.MazeObjects;
using UnityEngine;

namespace Assets.Scripts
{
  class HeroClickListener : MonoBehaviour
  {
    public Hero Hero;

    public void OnClick()
    {
      GameManager.Instance.ClickHero(Hero);
    }
  }
}
