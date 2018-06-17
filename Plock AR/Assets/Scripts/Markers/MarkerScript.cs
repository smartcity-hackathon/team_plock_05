using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        EventModelReference = GetComponent<EventModel>();
    }
    public EventModel EventModelReference { get; set; }
    public GameObject SelectedObject { get; set; }
    private void Update()
    {
#if UNITY_EDITOR
        //HandleMouseButtons();
#elif UNITY_ANDROID
        HandleTouch();
#endif
    }
    public void InitObjectAnimation()
    {
        Debug.Log("marker: " + EventModelReference.Name);
        ObjectAnimation oa = GameObject.Find("Popup").GetComponent<ObjectAnimation>();
        oa.FillContentWithEventModel(EventModelReference);
        oa.AnimateUsingAnimationCurveInternal();
    }
    private void OnMouseDown()
    {
        InitObjectAnimation();
    }
    private void HandleMouseButtons()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    SelectObject(Input.mousePosition);
        //}
        //if (Input.GetMouseButtonUp(0) && SelectedObject != null)
        //{
        //}
    }
    private void HandleTouch()
    {
        if (Input.touchCount > 1 || Input.touchCount == 0)
            return;
        var touch = Input.touches[0];
        if (touch.phase == TouchPhase.Began)
        {
            SelectObject(touch.position);
        }
        if (touch.phase == TouchPhase.Ended && SelectedObject != null)
        {
            //todo
        }
    }
    private void SelectObject(Vector3 inputPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(inputPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            SelectedObject = hit.transform.gameObject;
        }
        else
        {
            SelectedObject = null;
        }
    }
}
