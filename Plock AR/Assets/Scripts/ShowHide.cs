using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHide : MonoBehaviour {
    private Image[] childrenImages;
    // Use this for initialization
    public static CoverState MyCoverState;
    public enum CoverState { Other = 0, XI = 1, XIII = 2, XIX = 3 }
    private CoverState prevCoverState;
    private CoverRun coverRun;
    private enum CoverRun {Done = 0, Cover=1, Uncover=2}
    private float fCover;
    private CanvasGroup MyCover;
    void Start() {
        //childrenImages = transform.GetComponentsInChildren<Image>(true);
        childrenImages = new Image[4];
        for (int e = 0; e< childrenImages.Length;e++)
        {
            childrenImages[e] = transform.GetChild(e).GetComponent<Image>();
        }
        MyCover = childrenImages[3].GetComponent<CanvasGroup>();
        MyCoverState = CoverState.Other;
        prevCoverState = CoverState.Other;
        fCover = 0;
    }

    // Update is called once per frame
    public void ChangeCoverT(int Tw)
    {
        if (Tw ==0)
        MyCoverState = CoverState.Other;
        else if (Tw == 1)
            MyCoverState = CoverState.XI;
        else if (Tw == 2)
            MyCoverState = CoverState.XIII;
        else if (Tw == 3)
            MyCoverState = CoverState.XIX;
    }
    private void ExchangeGraphic()
    {
        int UnhidenNumber = -1;
        if (MyCoverState == CoverState.XI)
            UnhidenNumber = 0;
        else if (MyCoverState == CoverState.XIII)
            UnhidenNumber = 1;
        else if (MyCoverState == CoverState.XIX)
            UnhidenNumber = 2;
        for (int t = 0; t<childrenImages.Length-1;t++)
        {
            if (UnhidenNumber == t)
                childrenImages[t].gameObject.SetActive(true);
            else
                childrenImages[t].gameObject.SetActive(false);
        }

    }
	void Update () {
        if (MyCoverState != prevCoverState)
        {
            coverRun = CoverRun.Cover;
            prevCoverState = MyCoverState;
        }
        if (coverRun == CoverRun.Cover)
        {
            fCover += Time.deltaTime*2;
            MyCover.alpha = fCover;
            if (fCover>1)
            {
                fCover = 1;
                ExchangeGraphic();
                coverRun = CoverRun.Uncover;
                MyCover.alpha = fCover;
            }
            childrenImages[3].gameObject.SetActive(true);
        }
        else if (coverRun == CoverRun.Uncover)
        {
            fCover -= Time.deltaTime*2;
            if (fCover < 0)
            {
                fCover = 0;
                // hidecover
                coverRun = CoverRun.Done;
                childrenImages[3].gameObject.SetActive(false);
            }
            MyCover.alpha = fCover;
        }
        // 


            // uncover


    }
}
