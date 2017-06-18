using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
  class ChestTakingListener : MonoBehaviour
  {
    public void Delete()
    {
      Destroy(this.gameObject);
    }
  }
}
