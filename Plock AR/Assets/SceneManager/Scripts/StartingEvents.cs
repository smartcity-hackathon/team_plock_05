using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StartingEvents : MonoBehaviour {

	public UnityEvent StartingEvent;
	public SceneField StartingScene;
	public GameObject FPSContainer;
	public GameObject StartingPosition;
	private int ControllerType;
	private Vector3 StartingPositionLocation;
	private Quaternion StartingPositionRotation;
	// Use this for initialization
	void Start () {
		ControllerType = PlayerPrefs.GetInt("ControllerType");
		if (ControllerType == 0){
			ControllerType = 1;
		}
		Debug.Log("Controller Type is" + ControllerType);
		Scene CurrentScene = SceneManager.GetActiveScene();
		Debug.Log("Current Scene is " + CurrentScene.name);
		Debug.Log("Starting Scene is " + StartingScene.SceneName);
		if (StartingPosition != null){
		StartingPositionLocation = StartingPosition.transform.position;
		StartingPositionRotation = StartingPosition.transform.rotation;
		}
		else{
			StartingPositionLocation = new Vector3(0, 0, 0);
			StartingPositionRotation = Quaternion.Euler(0,0,0);
		}
		if (CurrentScene.name != StartingScene.SceneName){
			if (ControllerType == 1){
				GameObject ControllerContainer = Instantiate(FPSContainer,StartingPositionLocation, StartingPositionRotation) as GameObject;
				ControllerContainer.transform.parent = StartingPosition.transform;
				}
		}
		else{
		StartingEvent.Invoke ();
		}

	}
	

}
