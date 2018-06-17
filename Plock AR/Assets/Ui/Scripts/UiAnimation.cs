using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiAnimation{

public static bool isAnimationRunning = false;



public static IEnumerator SlidePanel (UiPanel PanelToSlide, Vector3 SlidePanelFrom, Vector3 SlidePanelTo,  AnimationCurve animationCurve, float time)
{
	yield return new WaitWhile(() => isAnimationRunning == true);
			float i = 0.0f;
			float rate = 1 / time;
			while (i < 1)
			{
				isAnimationRunning = true;
				i += Time.deltaTime * rate;
				PanelToSlide.gameObject.GetComponent<RectTransform>().localPosition=Vector3.Lerp(SlidePanelFrom,SlidePanelTo, animationCurve.Evaluate(i));
				isAnimationRunning = false;
				yield return new WaitForEndOfFrame();
			}
}
}
