using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;


public class EnableSoftMask : MonoBehaviour {
	public SoftMaskScript SoftMaskToActivate;
	// Use this for initialization
	void Start () {
		SoftMaskToActivate = this.gameObject.GetComponent<SoftMaskScript>();
		SoftMaskToActivate.enabled = true;
	}

}
