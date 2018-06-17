using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableImageComponent : MonoBehaviour {

	public Image imageToActivate;
	// Use this for initialization
	void Start () {
		imageToActivate = this.gameObject.GetComponent<Image>();
		imageToActivate.enabled = true;
	}	
}
