using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class SceneSelector : MonoBehaviour {

public UnityEvent Event1;

public void StartGameWithFPSController (){
	StartCoroutine (ExecuteInOrder(1));
}

public void StartGameWithViveController(){
	StartCoroutine (ExecuteInOrder(2));
}
public IEnumerator ExecuteInOrder(int ControllerType){
	PlayerPrefs.SetInt ("ControllerType", ControllerType);
	Event1.Invoke ();
	yield return new WaitForEndOfFrame ();
}

}
