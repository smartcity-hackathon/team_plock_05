using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class mClick : MonoBehaviour {
    public static bool BlockMovement;
    private int UILayer;
	// Use this for initialization
	void Start () {
        UILayer = LayerMask.NameToLayer("UI");

    }
	
	// Update is called once per frame
	void Update () {
        //return;
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            if (results.Count > 0)
            {
                //Debug.Log(results[0]);
                if (results[0].gameObject.layer == UILayer)
                    BlockMovement = true;
                else
                    BlockMovement = false;
                /*
                for (int r = 0; r < results.Count; r++)
                {
                    if (results[r].gameObject.name == "")
                    {
                        r = results.Count;
                    }
                }
                */
            }
            else
                BlockMovement = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            BlockMovement = false;
        }

            /*

            if (results[])
                BlockMovement = false;
            bool Mybool = false;

            */
        }
    }
