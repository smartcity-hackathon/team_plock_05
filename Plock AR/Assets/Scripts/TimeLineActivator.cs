using Mapbox.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimeLineActivator : MonoBehaviour {

	public Scrollbar Target;
	float targetValue;
    private bool GoToPoint;
    private int NrElement;
    private float distance;
    private int GoingIndicator;
    private bool DuringManipulation;
    private bool IsGoingTo;
    public float snapSpeed = 1;
    private Transform PathContainer;
    public AnimationCurve animationCurve;
  //  private Transform[] AllPaths;
    private List<SpriteRenderer[]> AllPaths = new List<SpriteRenderer[]>();
    private List<string> AllPathsName = new List<string>();
    private Button[] ContainerButton;
    private int ChosenId = 0;
    private int NewID = 0;
    private bool ThereIsPath;
    private string[] LeftList;

    public float BasicFadeSpeed = 1;
    public float LengthDependFadeSpeed = 1;
    public Transform ButtonHolder;
    //	public Button EventIndicatorButton1;
    //	public Button EventIndicatorButton2;
    //	public Button EventIndicatorButton3;
    //	public Button EventIndicatorButton4;
    //	public Button EventIndicatorButton5;
    //	public Button EventIndicatorButton6;
    //	public Button EventIndicatorButton7;
    //	public Button EventIndicatorButton8;
    //	public Button EventIndicatorButton9;
    //	public Button EventIndicatorButton10;
    //	public Button EventIndicatorButton11;

    private float[] EventIndicatorPosition;
    public float BigBallSize = 0.003f;

    //	public float EventIndicator1 = 0f;
    //	public float EventIndicator2 = 0.1f;
    //	public float EventIndicator3 = 0.2f;
    //	public float EventIndicator4 = 0.3f;
    //	public float EventIndicator5 = 0.4f;
    //	public float EventIndicator6 = 0.5f;
    //	public float EventIndicator7 = 0.6f;
    //	public float EventIndicator8 = 0.7f;
    //	public float EventIndicator9 = 0.8f;
    //	public float EventIndicator10 = 0.9f;
    //	public float EventIndicator11 = 1f;
    void Start()
    {
        IsGoingTo = true;
        ContainerButton = ButtonHolder.GetComponentsInChildren<Button>();
        NrElement = ContainerButton.Length;
        EventIndicatorPosition = new float[NrElement];
        for (int r = 0; r< NrElement; r++)
        {
            EventIndicatorPosition[r] = (ContainerButton[r].transform.parent.GetComponent<RectTransform>().anchoredPosition.x+500.0f)/1000.0f;

        }
        ActivateDeactivateButton();
        return;// no paths no fun
        foreach(Transform Path in PathContainer)
        {
            string[] PathName = Path.name.Split('e');
            if (PathName[0] == "Voyag")
            {
                AllPaths.Add(Path.GetComponentsInChildren<SpriteRenderer>());
                AllPathsName.Add(PathName[PathName.Length-1]);
            }
        }
        Debug.Log(AllPaths[0].Length + "     "+ AllPaths[1].Length);

        

        for (int k = 0; k < AllPaths.Count; k++)
        {
            SpriteRenderer[] TSprites = AllPaths[k];
            for (int i = 0; i < TSprites.LongLength; i++)
            {
                TSprites[i].color = new Color(0.4f, 0.05f, 0, 0);
            }
            TSprites[TSprites.LongLength - 1].transform.localScale = new Vector3(0, 0, 0);
        }
        
    }
    IEnumerator ThePath(int TPathNumber)
    {
     SpriteRenderer[] TSprites = AllPaths[TPathNumber];
        int u = 0;
        int TNewID = NewID;
        yield return new WaitForSeconds(0.2f);
        while (ThereIsPath)
            yield return null;
        ThereIsPath = true;
        /*
        for (int i = 0; i< TSprites.LongLength; i ++)
        {
            TSprites[i].enabled = false;
        }
        
        for (int k = 0; k < 11; k++)
        {
            for (int i = 0; i < TSprites.LongLength; i++)
            {
                TSprites[i].color = new Color(0.4f, 0.05f, 0, Mathf.Lerp(TSprites[i].color.a, 0, (float)k / 10));
            }
            yield return null;
        }
        */
        long TotalNum = TSprites.LongLength;
        float TotalSpeed = 0.01f*LengthDependFadeSpeed * TotalNum + BasicFadeSpeed;
        while (u< TotalNum && TNewID == NewID)
        {
            TSprites[u].enabled = true;
            if (TPathNumber != ChosenId)
                u = 5000;
            //       for (int k = 0; k<11;k++)
            
            float colorValue = 0;

            Debug.Log("TotalSpeed" + TotalSpeed);
            while (colorValue<0.99f)
            {
                colorValue += Time.deltaTime * TotalSpeed;
                colorValue = Mathf.Clamp(colorValue, 0, 1);
                if (u < TotalNum - 1)
                {
                    TSprites[u].color = new Color(0.4f, 0.05f, 0, Mathf.Lerp(0, 1, colorValue));
                }
                else
                {
                    TSprites[u].transform.localScale = new Vector3(colorValue*BigBallSize, colorValue * BigBallSize, colorValue * BigBallSize);
                }
                    yield return null;
            }
            yield return null;
            //yield return new WaitForSeconds(0.2f);

            u++;
        }
        while (TNewID == NewID)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);

        float TemporaryAlphaIndex = 0;
       for (int k = 0; k < 31; k++)
       {
            for (int i = 0; i < TSprites.LongLength; i++)
            {
                if (i < TSprites.LongLength - 1)
                {
                    float TAlpha = TSprites[i].color.a;
                    TSprites[i].color = new Color(0.4f, 0.05f, 0, Mathf.Lerp(TAlpha, 0, (float)k / 30));
                    if (k == 0)
                    {
                        TemporaryAlphaIndex = TemporaryAlphaIndex + TAlpha;
                        if (TemporaryAlphaIndex < Mathf.Epsilon)
                        {
                            k = 1000;
                            Debug.Log("LetsBreakThisRidiculusProcess!");
                        }
                    }
                }
                else
                {
                    Vector3 TAlpha = TSprites[i].transform.localScale;
                    TSprites[i].transform.localScale = Vector3.Lerp(TAlpha, Vector3.zero, (float)k / 30);
                }
            }
            if (k < 999)
            {
                yield return null;
            }
        }
        ThereIsPath = false;


        /*
        for (int i = 0; i < TSprites.LongLength; i++)
        {
            TSprites[i].enabled = true;
            yield return null; yield return null;
        }
        */
        yield return null;
    }
    void Update()
	{
        if (Input.GetMouseButton(0))
        {
            IsGoingTo = true;
        }
        else
        {
            if (GoToPoint)
            {
                targetValue = Target.value;
                if (IsGoingTo)
                {
                    for (int i = 0; i < NrElement; i++)
                    {
                        float tempDist = Mathf.Abs(targetValue - EventIndicatorPosition[i]);
                        if (tempDist < distance)
                        {
                            distance = tempDist;
                            GoingIndicator = i;
                            //   IStartMoving = true;
                        }
                    }
                }
                IsGoingTo = false;
                Target.value = Mathf.Lerp(targetValue, EventIndicatorPosition[GoingIndicator], animationCurve.Evaluate(Time.time) * snapSpeed);
            }
        }
	}
    public void ChangeMapElements(string buttonName)
    {
        int month = int.Parse(buttonName);
        SpawnOnMap.UpdateEventModelsAvailability(month);
        if(month == -1)
        {
            ShowHide.MyCoverState = ShowHide.CoverState.XIX;
        }
        else if (month == -2)
        {
            ShowHide.MyCoverState = ShowHide.CoverState.XIII;
        }
        else if (month == -3)
        {
            ShowHide.MyCoverState = ShowHide.CoverState.XI;
        }
        else
        {
            ShowHide.MyCoverState = ShowHide.CoverState.Other;
        }
    }

	public void ActivateDeactivateButton()
	{
        GoToPoint = true;
        distance = 0.75f;
        targetValue = Target.value;
        //Debug.Log(targetValue);
		for (int u = 0; u < ContainerButton.Length; u++) {

            float tempDist = Mathf.Abs(targetValue - EventIndicatorPosition[u]);

            if (tempDist < 0.01f)
			{
                //                if (!ContainerButton[u].interactable && !Input.GetMouseButton(0))
                if (!ContainerButton[u].interactable)
                {
                    ContainerButton[u].interactable = true;
                    ContainerButton[u].GetComponent<Image>().raycastTarget = true;
                    string[] ButtonName = ContainerButton[u].name.Split('#');
                    //GoingIndicator = u;
                    //   Debug.Log(AllPathsName.FirstOrDefault(s => s == ButtonName[ButtonName.Length - 1]));
                    //    Debug.Log(AllPathsName.IndexOf(ButtonName[ButtonName.Length - 1]));

                    //     FirstOrDefault(s => s == ButtonName[ButtonName.Length - 1]));
                    Debug.Log("show certain scenne" + ButtonName[ButtonName.Length - 1]);
                    ChangeMapElements(ButtonName[ButtonName.Length - 1]);
                    /*
                    int TPathNumber = AllPathsName.IndexOf(ButtonName[ButtonName.Length - 1]);
                   if (TPathNumber > -1)
                    {
                        if (NewID < 100)
                            NewID++;
                        else
                            NewID = 0;
                        Debug.Log("show certain scenne" + TPathNumber);
                        //StartCoroutine(ThePath(TPathNumber));
                        ChosenId = TPathNumber;
 
                    }
                    */
                    //     StartCoroutine(ThePath());
                }
            }
			else
			{
				ContainerButton[u].interactable = false;
                ContainerButton[u].GetComponent<Image>().raycastTarget = false;
            }
            if (tempDist < 0.0001f)
            {
                GoToPoint = false;
            }

            }
//		if (Mathf.Abs(targetValue - EventIndicator1)<0.05f)
//		{
//			EventIndicatorButton1.interactable = true;
//
//		}
//		else
//		{
//			EventIndicatorButton1.interactable = false;
//
//		}
//
//		if (Mathf.Abs(targetValue - EventIndicator2)<0.05f)
//		{
//			EventIndicatorButton2.interactable = true;
//
//		}
//		else
//		{
//			EventIndicatorButton2.interactable = false;
//
//		}
//
//		if (targetValue == EventIndicator3)
//		{
//			EventIndicatorButton3.interactable = true;
//
//		}
//
//		if (targetValue != EventIndicator3)
//		{
//			EventIndicatorButton3.interactable = false;
//
//		}
//
//		if (targetValue == EventIndicator4)
//		{
//			EventIndicatorButton4.interactable = true;
//
//		}
//
//		if (targetValue != EventIndicator4)
//		{
//			EventIndicatorButton4.interactable = false;
//
//		}
//
//		if (targetValue == EventIndicator5)
//		{
//			EventIndicatorButton5.interactable = true;
//
//		}
//		if (targetValue != EventIndicator5)
//		{
//			EventIndicatorButton5.interactable = false;
//
//		}
//
//		if (targetValue == EventIndicator6)
//		{
//			EventIndicatorButton6.interactable = true;
//
//		}
//		if (targetValue != EventIndicator6)
//		{
//			EventIndicatorButton6.interactable = false;
//
//		}
//		if (targetValue == EventIndicator7)
//		{
//			EventIndicatorButton7.interactable = true;
//
//		}
//		if (targetValue != EventIndicator7)
//		{
//			EventIndicatorButton7.interactable = false;
//
//		}
//
//		if (targetValue == EventIndicator8)
//		{
//			EventIndicatorButton8.interactable = true;
//
//		}
//		if (targetValue != EventIndicator8)
//		{
//			EventIndicatorButton8.interactable = false;
//
//		}
//
//		if (targetValue == EventIndicator9)
//		{
//			EventIndicatorButton9.interactable = true;
//
//		}
//		if (targetValue != EventIndicator9)
//		{
//			EventIndicatorButton9.interactable = false;
//
//		}
//
//		if (targetValue == EventIndicator10)
//		{
//			EventIndicatorButton10.interactable = true;
//
//		}
//		if (targetValue != EventIndicator10)
//		{
//			EventIndicatorButton10.interactable = false;
//
//		}
//
//		if (targetValue == EventIndicator11)
//		{
//			EventIndicatorButton11.interactable = true;
//
//		}
//		if (targetValue != EventIndicator11)
//		{
//			EventIndicatorButton11.interactable = false;
//
//		}
	}
}
