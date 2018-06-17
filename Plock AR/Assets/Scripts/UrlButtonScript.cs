using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UrlButtonScript : MonoBehaviour
{
    public void VisitUrl()
    {
        string url = gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text;
        if (url.Length == 0)
            return;
        Application.OpenURL(url);
    }
}
