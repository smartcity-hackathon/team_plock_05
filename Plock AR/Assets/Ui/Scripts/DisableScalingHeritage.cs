using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableScalingHeritage : MonoBehaviour {

	void Update () {
		Vector3 childScale = GetComponent<RectTransform>().transform.localScale;
		Vector3 parentScale = transform.parent.GetComponent<RectTransform>().transform.localScale;
		//float newX = childScale.x/parentScale.x;
		//float newY = childScale.y/parentScale.y;
		//float newZ = childScale.z/parentScale.z;
		//Vector3 NewChildScale *= childScale;
		childScale *= 1.0f / parentScale.y;
		//transform.localScale = transform.localScale;
	}
}
