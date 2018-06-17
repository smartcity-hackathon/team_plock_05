using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSet : MonoBehaviour {

public Toggle toggle;
public Animator toggleAnimator;
public Toggle[] otherToggles;

void Start(){
	toggle = GetComponent<Toggle>();
	toggleAnimator = toggle.gameObject.GetComponent<Animator>();
}

public void SetToggleStatePressed(){
	toggleAnimator.SetTrigger("Pressed");
	toggle.interactable = false;
	foreach (Toggle otherToggle in otherToggles){
		otherToggle.isOn = false;
		otherToggle.interactable = true;
		otherToggle.GetComponent<Animator>().SetTrigger("Normal");
	}
}
public void SetToggleStateNormal(){
	toggleAnimator.SetTrigger("Normal");
}

public void SetToggleState(){
	if (toggle.isOn){
		SetToggleStatePressed();
	}
	else{
		toggleAnimator.SetTrigger("Normal");
	}
}


}
