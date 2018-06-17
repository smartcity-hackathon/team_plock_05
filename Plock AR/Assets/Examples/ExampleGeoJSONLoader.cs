using UnityEngine;
using System.Collections;

public class ExampleGeoJSONLoader : MonoBehaviour {

	public TextAsset encodedGeoJSON;

	public GeoJSON.FeatureCollection collection;

	// Use this for initialization
	void Start () {
	}
    public void GiveDebug()
    {
        foreach (GeoJSON.FeatureObject Fit in collection.features)
        {
            string MyS;
            Fit.properties.TryGetValue("symbol", out MyS);
            Debug.Log(Fit.geometry.FirstPosition() + ">>" + MyS);
        }
    }

	void OnGUI() {
		if (encodedGeoJSON != null) {
			if (GUI.Button (new Rect (0, 0, 200, 200), "Load GeoJSON"))
				collection = GeoJSON.GeoJSONObject.Deserialize (encodedGeoJSON.text);
		}
	}
}
