using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DentedPixel;
public class UiPanel : MonoBehaviour {

	public Canvas canvas;
	public CanvasGroup canvasGroup;
	public string Name;


	public float animationTime;
	public LeanTweenType leanTweenEasingType = 0;

	//If we want the starting position of the panel to be offScreen left,right,up or dowm
	public bool movePanelOffScreen = false;	
	
	//If we want the starting position of the panel to be a transform
	public Transform startingTransform;

    //jj
    public RectTransform startingRect { get; set; }

    //If we want to add to the starting position of the panel, or specify an offscreen direction
    public Vector2 startingPositionOffset;

	//Where do we want the panel to move to
	public Transform destinationTransform;

    //jj
    public RectTransform destinationRect { get; set; }

    //jj
    public RectTransform rt { get; set; }
    
    //Alpha we want to fade to
    public float destinationAlphaValue = 0f;
	public float baseAlphaValue;

	//Define if the panel is in it's starting or ending position by default	
	public bool _isPanelInOriginalPosition = true;

    //public Vector2 StartingPosition;jj

    public Vector2 StartingPosition
    {
        get
        {
            float canvasScale = GetScale(Screen.width, Screen.height, new Vector2(1920f, 1080f), 0.5f);// po co to - jj??
            Debug.Log(canvasScale);
            Vector2 _startPos;
            if (startingTransform != null)
            {
                //Start Position when a GameObject transform is supplied
                _startPos = startingTransform.gameObject.GetComponent<RectTransform>().localPosition;//jj
            }
            else
            {
                //Calculated start position
                if (movePanelOffScreen)
                {
                    _startPos = new Vector2(Screen.width * startingPositionOffset.x, Screen.height * startingPositionOffset.y);
                }
                else
                {
                    _startPos = new Vector2(Screen.width * startingPositionOffset.x, Screen.height * startingPositionOffset.y);
                }
            }
            return _startPos;

        }
    }
    void Awake()
	{
        if (startingTransform!=null)
        startingRect = startingTransform.GetComponent<RectTransform>();
        if (destinationTransform != null)
            destinationRect = destinationTransform.GetComponent<RectTransform>();
        rt = this.gameObject.GetComponent<RectTransform>();
		if(canvas == null)
		{
			canvas = GetComponentInParent<Canvas>();
		}
		if(canvasGroup != null)
		{
			baseAlphaValue = canvasGroup.alpha;
		}

		if (canvasGroup!= null)
			{
				//Starting alpha value of the canvas group, if we intend to fade the panel instead of moving it
				destinationAlphaValue = canvasGroup.alpha;
			}
        if (destinationTransform!=null)
        rt.localPosition = StartingPosition;
    }

	private float GetScale(int width, int height, Vector2 scalerReferenceResolution, float scalerMatchWidthOrHeight)
        {
            return Mathf.Pow(width/scalerReferenceResolution.x, 1f - scalerMatchWidthOrHeight)*
                   Mathf.Pow(height/scalerReferenceResolution.y, scalerMatchWidthOrHeight);
        }


}