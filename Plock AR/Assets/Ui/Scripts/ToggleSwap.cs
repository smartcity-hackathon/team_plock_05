using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class ToggleSwap : MonoBehaviour {
public GameObject ToggleOn;
public GameObject ToggleOff;

public Toggle toggle;
 
void Awake() {
	toggle = GetComponent<Toggle>();
	UpdateToggle();
	}
public void UpdateToggle() {
	if (toggle.isOn){
		ToggleOn.SetActive(true);
		ToggleOff.SetActive(false);
	}
	else{
		ToggleOn.SetActive(false);
		ToggleOff.SetActive(true);
	}
	
	}
}