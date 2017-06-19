using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscManager : MonoBehaviour
{
	void Update () 
  {
		if(Input.GetKeyUp(KeyCode.Escape))
	  {
	    SceneManager.LoadScene(0);
	  }
	}
}
