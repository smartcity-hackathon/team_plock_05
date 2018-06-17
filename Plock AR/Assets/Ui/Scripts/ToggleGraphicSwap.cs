using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleGraphicSwap : MonoBehaviour {
 
	Toggle toggle;

	void Awake() 
		{
			toggle = GetComponent<Toggle>();
			toggle.onValueChanged.AddListener(OnTargetToggleValueChanged);
		}

	void OnEnable() 
		{
			toggle.targetGraphic.enabled = !toggle.isOn;
		}

	void OnTargetToggleValueChanged(bool on) 
		{
			toggle.targetGraphic.enabled = !on;
		}
}