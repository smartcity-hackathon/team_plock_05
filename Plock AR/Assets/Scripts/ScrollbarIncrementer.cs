using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ScrollbarIncrementer : MonoBehaviour
{
	public Scrollbar Target;
	public Button TheOtherButton;
	public float Step = 0.1f;

	public void Increment()
	{
		if (Target == null || TheOtherButton == null) throw new Exception("Setup ScrollbarIncrementer first!");
		Target.value = Mathf.Clamp(Target.value + Step, 0, 1);
		//GetComponent<Button>().interactable = Target.value != 1;
		//TheOtherButton.interactable = true;
		//EventSystem.current.SetSelectedGameObject(null);
		//Debug.Log(Target.value);
	}

	public void CheckScrollBar ()
	{
		if (Target.value == 1);
		{
			//Increment();
			//GetComponent<Button>().interactable = Target.value != 1;
			//TheOtherButton.interactable = true;
		}

		if (Target.value == 0);
		{
			//Decrement ();
			//GetComponent<Button>().interactable = Target.value != 0;;
			//TheOtherButton.interactable = true;
		}
	}
	public void Decrement()
	{
		if (Target == null || TheOtherButton == null) throw new Exception("Setup ScrollbarIncrementer first!");
		Target.value = Mathf.Clamp(Target.value - Step, 0, 1);
		//GetComponent<Button>().interactable = Target.value != 0;;
		//TheOtherButton.interactable = true;
		//EventSystem.current.SetSelectedGameObject(null);
		//Debug.Log(Target.value);
	}

	public float HoldFrequency = 0.1f;
	public void OnPointerDown(bool increment)
	{
		StartCoroutine("IncrementDecrementSequence", increment);
		//EventSystem.current.SetSelectedGameObject(null);
	}

	public void OnPointerUp()
	{
		StopCoroutine("IncrementDecrementSequence");
		//EventSystem.current.SetSelectedGameObject(null);
	}

	IEnumerator IncrementDecrementSequence(bool increment)
	{
		yield return new WaitForSeconds(HoldFrequency);
		if (increment) Increment();
		else           Decrement();
		StartCoroutine("IncrementDecrementSequence", increment);
	}
}