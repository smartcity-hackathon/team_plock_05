namespace Mapbox.Examples
{
    using UnityEngine;
    using Mapbox.Utils;
    using Mapbox.Unity.Map;
    using Mapbox.Unity.MeshGeneration.Factories;
    using Mapbox.Unity.Utilities;
    using System.Collections.Generic;
    using System;
    using Mapbox.Json;
    using System.Globalization;
    using System.Linq;

    public class SpawnOnMap : MonoBehaviour
    {
        [SerializeField]
        AbstractMap _map;

        List<Vector2d> _locations;

        [SerializeField]
        float _spawnScale = 50f;

        [SerializeField]
        GameObject _markerPrefab;

        List<GameObject> _spawnedObjects = new List<GameObject>();

        // data from JSON
        public TextAsset encodedGeoJSON;
        public GeoJSON.FeatureCollection FeatureCollection { get; set; }
        void Start()
        {
            if (encodedGeoJSON != null)
            {
                FeatureCollection = GeoJSON.GeoJSONObject.Deserialize(encodedGeoJSON.text);
            }
            if (FeatureCollection == null)
                return;
            _locations = new List<Vector2d>();
            foreach (var fit in FeatureCollection.features)
            {
                _locations.Add(Conversions.StringToLatLon(fit.properties["y"] + "," + fit.properties["x"]));
                GameObject go = Instantiate(_markerPrefab);
                EventModel em = go.GetComponentInChildren<EventModel>();
                EventModelsManager.InitializeEventModel(em, fit, go);
                EventModelsManager.InitializeEventModelGraphics(em);
                EventModelsManager.EventModels.Add(em);
            }
        }
        private void Update()
        {
            UpdatePositionAndScaleOfEventModels();
        }
        public static void UpdateEventModelsAvailability(int month)
        {
            if (month < 1)
            {
                EventModelsManager.EventModels.ForEach(x => x.IsAvailable = false);
                if (month == -1)
                {
                    EventModelsManager.EventModels.Where(x => x.StartDate.Equals("1920")).FirstOrDefault().IsAvailable = true;
                }
                else if (month == -2)
                {
                    EventModelsManager.EventModels.Where(x => x.StartDate.Equals("1351")).FirstOrDefault().IsAvailable = true;
                }
                else if (month == -3)
                {
                    EventModelsManager.EventModels.Where(x => x.StartDate.Equals("1130")).FirstOrDefault().IsAvailable = true;
                }
            }
            else
            {
                EventModelsManager.EventModels.ForEach(x => x.IsAvailable = x.IsInMonth(month));
            }
            //Debug.Log("available: " + EventModelsManager.EventModels.Where(x => x.IsAvailable).Count().ToString());
        }
        private void UpdatePositionAndScaleOfEventModels()
        {
            for (int i = 0; i < EventModelsManager.EventModels.Count; i++)
            {
                Vector3 v = _map.GeoToWorldPosition(_locations[i], true);
                EventModelsManager.EventModels[i].RelatedGameObject.transform.localPosition = new Vector3(v.x, 1.0f, v.z);
                EventModelsManager.EventModels[i].RelatedGameObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            }
        }
    }
}