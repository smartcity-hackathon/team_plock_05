using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectAnimation : MonoBehaviour
{

    public float time;
    public AnimationCurve animationCurve;
    public GameObject ObjectToAnimate;
    public GameObject TargetPosition;
    //public float animationTime;
    private bool isAnimationRunning = false;
    private Vector3 targetPosition;
    private Vector3 starterPosition;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    public bool GoToTarget = false;
    public float zoomAmount;
    GameObject ArButton;

    void Start()
    {
        initialPosition = ObjectToAnimate.transform.localPosition;
        //initialRotation = ObjectToAnimate.transform.rotation;
        ArButton = GameObject.Find("ArButton");
    }

    public void AnimateUsingAnimationCurve()
    {
        //targetPosition = new Vector3 (TargetPosition.transform.localPosition.x, TargetPosition.transform.localPosition.y, zoomAmount);
        //starterPosition = new Vector3 (ObjectToAnimate.transform.localPosition.x,ObjectToAnimate.transform.localPosition.y, ObjectToAnimate.transform.localPosition.z);
        //GoToTarget = true;
        //StartCoroutine(UsingAnimationCurve(starterPosition, targetPosition, time));
    }
    
    public void FillContentWithEventModel(EventModel eventModelReference)
    {
        FillGallery(eventModelReference);
        SetTitle(eventModelReference);
        SetDetails(eventModelReference);
        SetDescription(eventModelReference);
        ToggleArButton(eventModelReference);
    }
    private void ToggleArButton(EventModel eventModelReference)
    {
        ArButton.SetActive(eventModelReference.EventType == EventTypes.Past);
    }
    private void FillGallery(EventModel eventModelReference)
    {
        int count = eventModelReference.Graphics.Count;
        GameObject gallery = GameObject.Find("ListGallery");
        int childrenCount = gallery.transform.childCount;
        for (int i = 0; i < childrenCount; i++)
        {
            gallery.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < count; i++)
        {
            GameObject rect = gallery.transform.GetChild(i).gameObject;
            rect.SetActive(true);
            rect.GetComponent<Image>().sprite = SpriteHelper.CreatSprite(eventModelReference.Graphics[i]);
        }
    }
    private void SetTitle(EventModel eventModelReference)
    {
        var g = GameObject.Find("PopupTitle");
        TextMeshProUGUI t = g.GetComponent<TextMeshProUGUI>();//
        t.SetText(eventModelReference.Name);
    }
    private void SetDetails(EventModel eventModelReference)
    {
        var t = GameObject.Find("PopupDetails").GetComponent<TextMeshProUGUI>();
        if (eventModelReference.EventType == EventTypes.Past)
        {
            t.SetText("");
            return;
        }
        StringBuilder sb = new StringBuilder();
        string startDate = eventModelReference.StartDate;
        string endDate = eventModelReference.EndDate;
        bool oneDay = startDate.Equals(endDate);
        if (oneDay)
        {
            sb.Append(startDate);
        }
        else
        {
            sb.Append("Od ");
            sb.Append(startDate);
            sb.Append(" do ");
            sb.Append(endDate);
        }
        sb.Append("      ");
        sb.Append("Wstęp: " + eventModelReference.Price);
        t.SetText(sb.ToString());
    }
    private void SetDescription(EventModel eventModelReference)
    {
        var t = GameObject.Find("PopupDescription").GetComponent<TextMeshProUGUI>();
        t.SetText(eventModelReference.Description);
    }
    private void SetWebsite(EventModel eventModelReference)
    {
        TextMeshProUGUI urlText = GameObject.Find("UrlButton").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if(eventModelReference.Url == null || eventModelReference.Url.Length == 0)
        {
            urlText.SetText("");
            return;
        }
        urlText.SetText(eventModelReference.Url);
    }
    public void AnimateUsingAnimationCurveInternal()
    {
        targetPosition = new Vector3(TargetPosition.transform.localPosition.x, TargetPosition.transform.localPosition.y, zoomAmount);
        starterPosition = new Vector3(ObjectToAnimate.transform.localPosition.x, ObjectToAnimate.transform.localPosition.y, ObjectToAnimate.transform.localPosition.z);
        GoToTarget = true;
        StartCoroutine(UsingAnimationCurve(starterPosition, targetPosition, time));
    }
    public void ResetPositionUsingAnimationCurve()
    {
        targetPosition = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z);
        starterPosition = new Vector3(ObjectToAnimate.transform.localPosition.x, ObjectToAnimate.transform.localPosition.y, ObjectToAnimate.transform.localPosition.z);
        GoToTarget = false;
        //MainCam.transform.position = initialCameraPosition;
        StartCoroutine(UsingAnimationCurve(starterPosition, targetPosition, time));
        //MainCam.transform.rotation = initialCameraRotation;
    }

    IEnumerator UsingAnimationCurve(Vector3 startPos, Vector3 endPos, float time)
    {

        float i = 0.0f;
        float rate = 1 / time;
        while (i < 1)
        {
            i += Time.deltaTime * rate;
            ObjectToAnimate.transform.localPosition = Vector3.Lerp(startPos, endPos, animationCurve.Evaluate(i));
            yield return new WaitForEndOfFrame();
        }
    }
}
