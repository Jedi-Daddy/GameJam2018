using System.Collections;
using System.Collections.Generic;
using Assets.Model;
using Assets.Model.Maze;
using Assets.Scripts;
using UnityEngine;

public class CellInfoViewModel : MonoBehaviour
{
  public CellInfo CellInfo;
  private SegmentInfo _segmentInfo;
  
  public void OnClick()
  {
    if(_segmentInfo == null)
      _segmentInfo = transform.parent.GetComponent<SegmentInfo>();

    var segmentId = _segmentInfo.Id;

    GameManager.Instance.MoveHero(new LocationInMaze
    {
      CoordsInSegment = CellInfo.Coords,
      SegmentId = segmentId
    });
  }
}
