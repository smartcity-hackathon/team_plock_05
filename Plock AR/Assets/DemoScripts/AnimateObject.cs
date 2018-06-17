using UnityEngine;
using System.Collections;

public class AnimateObject : MonoBehaviour
{
    /// <summary>
    /// Animation curve that is set from the editor
    /// </summary>
    public AnimationCurve animationCurve;

    /// <summary>
    /// Starting Position of the Object
    /// </summary>
    public Vector3 startPosition;

    /// <summary>
    /// Ending Position of the Object
    /// </summary>
    public Vector3 endPosition;

    /// <summary>
    /// Complete animation within seconds
    /// </summary>
    public float animationTime = 1.0f;

    private bool isAnimationRunning = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            OnClick();
        }
    }

    public void OnClick() 
    {
        StartCoroutine(UsingAnimationCurve(startPosition, endPosition, animationTime));
    }

    IEnumerator UsingAnimationCurve(Vector3 startPosition, Vector3 endPosition, float time)
    {
        if (!isAnimationRunning)
        {
            isAnimationRunning = true;
            float i = 0.0f;
            float rate = 1 / time;
            while (i < 1)
            {
                i += Time.deltaTime * rate;
                transform.localPosition = Vector3.Lerp(startPosition, endPosition, animationCurve.Evaluate(i));
                yield return 0;
            }
            isAnimationRunning = false;
        }
        yield return 0;
    }
}
