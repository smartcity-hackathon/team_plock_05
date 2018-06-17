using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PastMarkerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void InitObjectAnimation(int year)
    {
        EventModelsManager.EventModels.Where(x => x.StartDate.Equals(year.ToString())).FirstOrDefault().gameObject.GetComponent<MarkerScript>().InitObjectAnimation();
    }
}
