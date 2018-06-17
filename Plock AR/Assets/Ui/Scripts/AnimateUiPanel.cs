using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DentedPixel;

public class AnimateUiPanel : MonoBehaviour {

    public Canvas ParentCanvas;
    public bool isTweening = false;
    public int myTween;


    void Start() {
        if (ParentCanvas == null)
        {
            ParentCanvas = GetComponentInParent<Canvas>();
        }
    }
    public void TogglePanel(UiPanel PanelToAnimate)
    {
        if (PanelToAnimate._isPanelInOriginalPosition)
        {
            MovePanelTo(PanelToAnimate);
        }
        else
        {
            MovePanelToOriginalPosition(PanelToAnimate);
        }
    }
    public void ApplyResolutionChange(UiPanel PanelToAnimate)
    {

    }
    public void MovePanelTo(UiPanel PanelToAnimate)
    {
        LeanTween.cancel(PanelToAnimate.gameObject);
        PanelToAnimate._isPanelInOriginalPosition = !PanelToAnimate._isPanelInOriginalPosition;
        Vector2 _movePanelTo = PanelToAnimate.destinationRect.localPosition;
        //Debug.Log(_movePanelTo);
        float _time = PanelToAnimate.animationTime;
        //int myNewTween = LeanTween.move(PanelToAnimate.rt, _movePanelTo, _time).setEase(PanelToAnimate.leanTweenEasingType).id;
        int myNewTween = LeanTween.moveLocal(PanelToAnimate.gameObject, _movePanelTo, _time).setEase(PanelToAnimate.leanTweenEasingType).id;
        LTDescr d = LeanTween.descr(myNewTween);
        d.setOnComplete((object val) => { PanelToAnimate.rt.localPosition = _movePanelTo; });
    }
    public void MovePanelToOriginalPosition(UiPanel PanelToAnimate)
    {
        LeanTween.cancel(PanelToAnimate.gameObject);
        PanelToAnimate._isPanelInOriginalPosition = !PanelToAnimate._isPanelInOriginalPosition;
        Vector2 _movePanelTo = PanelToAnimate.StartingPosition;
        //Debug.Log(_movePanelTo + "original");
        float _time = PanelToAnimate.animationTime;
        //int myNewTween = LeanTween.move(PanelToAnimate.rt, _movePanelTo, _time).setEase(PanelToAnimate.leanTweenEasingType).id;
        int myNewTween = LeanTween.moveLocal(PanelToAnimate.gameObject, _movePanelTo, _time).setEase(PanelToAnimate.leanTweenEasingType).id;
        LTDescr d = LeanTween.descr(myNewTween);
        //delAction = FinishMovement;
        //class ToMeve { RectTransform MyRect; Vector2 Vect;};
        d.setOnComplete((object val) => { PanelToAnimate.rt.localPosition = _movePanelTo; });
        //d.setOnComplete(MoveFinished, PanelToAnimate.gameObject);

    }
    public void MoveFinished(object myObj)
    {
        GameObject myGO = (GameObject)myObj as GameObject;
        Debug.Log(myGO.name);
    }
    /*
    public void TogglePanelMarg(UiPanel PanelToAnimate)
    {
        if (PanelToAnimate._isPanelInOriginalPosition)
        {
            MoveMarginsTo(PanelToAnimate);
        }
        else
        {
            MoveMarginsToOriginalPosition(PanelToAnimate);
        }
    }
    public void MoveMarginsTo(UiPanel PanelToAnimate)
    {
        LeanTween.cancel(PanelToAnimate.gameObject);
        PanelToAnimate._isPanelInOriginalPosition = !PanelToAnimate._isPanelInOriginalPosition;
        //Vector3 _movePanelTo = PanelToAnimate.destinationTransform.localPosition;
        float _time = PanelToAnimate.animationTime;
        int myNewTween = LeanTween.moveMargin(new LTRect(), new Vector2(0, 0), _time).setEase(PanelToAnimate.leanTweenEasingType).id;
        LTDescr d = LeanTween.descr(myNewTween);
    }
    public void MoveMarginsToOriginalPosition(UiPanel PanelToAnimate)
    {
        LeanTween.cancel(PanelToAnimate.gameObject);
        //Vector3 _movePanelTo = PanelToAnimate.StartingPosition;
        float _time = PanelToAnimate.animationTime;
        //LTRect MyRect = LeanTween.LTRect();
        int myNewTween = LeanTween.moveMargin(new LTRect(), new Vector2(-1,0), _time).setEase(PanelToAnimate.leanTweenEasingType).id;
        LTDescr d = LeanTween.descr(myNewTween);
        PanelToAnimate._isPanelInOriginalPosition = !PanelToAnimate._isPanelInOriginalPosition;
    }
    */
    public void BouncePanel(UiPanel PanelToAnimate)
	{
		if(!PanelToAnimate._isPanelInOriginalPosition)
		{
			PanelToAnimate._isPanelInOriginalPosition =!PanelToAnimate._isPanelInOriginalPosition;
			Vector3 _movePanelTo  = PanelToAnimate.StartingPosition;
			float _time = PanelToAnimate.animationTime;
			int myNewTween = LeanTween.moveLocal(PanelToAnimate.gameObject,_movePanelTo,_time).setEase(PanelToAnimate.leanTweenEasingType).id;
			LTDescr d = LeanTween.descr(myNewTween);
		}
		else
		{
			MovePanelTo(PanelToAnimate);
		}
	}
	public void FadePanel(UiPanel PanelToAnimate)
	{
        {
            if (PanelToAnimate._isPanelInOriginalPosition)
            {
                FadePanelTarget(PanelToAnimate);
            }
            else
            {
                FadePanelToOriginal(PanelToAnimate);
            }
        }
    }
    public void FadePanelTarget(UiPanel PanelToAnimate)
    {
        float TargetAlpha = PanelToAnimate.destinationAlphaValue;
        if (TargetAlpha > 0.9)
            PanelToAnimate.gameObject.SetActive(true);
        CanvasGroup canvasGroup = PanelToAnimate.GetComponent<CanvasGroup>();
        int myNewTween = LeanTween.value(PanelToAnimate.gameObject, canvasGroup.alpha, TargetAlpha, PanelToAnimate.animationTime).id;
        LTDescr d = LeanTween.descr(myNewTween).setOnUpdate((float val) => { canvasGroup.alpha = val; });
        //d.setOnComplete((object val) => { PanelToAnimate.rt.localPosition = _movePanelTo; });
        if (TargetAlpha < 0.1)
            d.setOnComplete((object val) => { PanelToAnimate.gameObject.SetActive(false); });
        //d.setOnComplete(FadeFinishedOut, PanelToAnimate.gameObject);
    }
    public void FadePanelToOriginal(UiPanel PanelToAnimate)
    {
        float TargetAlpha = PanelToAnimate.baseAlphaValue;
        if (TargetAlpha > 0.9)
            PanelToAnimate.gameObject.SetActive(true);
        CanvasGroup canvasGroup = PanelToAnimate.GetComponent<CanvasGroup>();
        int myNewTween = LeanTween.value(PanelToAnimate.gameObject, canvasGroup.alpha, TargetAlpha, PanelToAnimate.animationTime).id;
        LTDescr d = LeanTween.descr(myNewTween).setOnUpdate((float val) => { canvasGroup.alpha = val; });
        //d.setOnComplete((object val) => { PanelToAnimate.rt.localPosition = _movePanelTo; });
        if (TargetAlpha < 0.1)
            d.setOnComplete((object val) => { PanelToAnimate.gameObject.SetActive(false); });
        //d.setOnComplete(FadeFinishedOut, PanelToAnimate.gameObject);
    }
    public void FadeFinishedOut(object myObj)
    {
        GameObject myGO = (GameObject)myObj as GameObject;
        Debug.Log(myGO.name);
    }

    /*
	IEnumerator TweenPanelWithCheck(UiPanel PanelToAnimate,Vector3 _movePanelTo,float _time)
	{		
		Vector3 _movePanelFrom = PanelToAnimate.gameObject.GetComponent<RectTransform>().localPosition;
		GameObject myPanel = PanelToAnimate.gameObject;
		while(!LeanTween.isTweening(myPanel))
		{
			int myNewTween = LeanTween.moveLocal(myPanel,_movePanelTo,_time).setEase(PanelToAnimate.leanTweenEasingType).id;
			LTDescr d = LeanTween.descr(myNewTween);
			yield return new WaitForEndOfFrame();
		}	
	}
	*/

}
