using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UnhighlightUiButton : MonoBehaviour {

	private EventSystem eventSystem;

	void Start () {
		eventSystem = EventSystem.current;
	}

	void Update () {
		if (Input.GetMouseButtonUp (0)) {
			eventSystem.SetSelectedGameObject(null);
			}
		}
}
