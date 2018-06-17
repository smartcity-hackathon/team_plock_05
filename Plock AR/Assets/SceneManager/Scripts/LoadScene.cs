using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {

	public GameObject LoadingScreen;
	public Slider loadingBar;
	public AnimationCurve Fade;
	public float FadeTime = 2;
	public bool IsLoading = false;

	public static int CurrentlyLoadingScene;

	private CanvasGroup _canvasGroup;

	void Start(){
		if (LoadingScreen == null) {
			//LoadingScreen = GameObject.FindGameObjectWithTag ("LoadingScreen");
		}
		if (LoadingScreen != null) {
			_canvasGroup = LoadingScreen.GetComponent<CanvasGroup> ();
			_canvasGroup.alpha = 0f;
		}
	}

	public void LoadNewSceneIndexAdditive(int SceneIndex)
	{
		Debug.Log ("Load Additive Scene by Index");
		//Add load level for new scene here.
		SceneHandler.LoadLevelProgress = 0f;
		SceneManager.LoadScene(SceneIndex, LoadSceneMode.Additive);
	}
	public void LoadNewSceneIndex(int SceneIndex)
	{
		Debug.Log ("Load Scene by Index");
		//StartCoroutine(Wait(5f));
		//Add load level for new scene here.
		//CurrentlyLoadingScene = SceneIndex;
		SceneHandler.LoadLevelProgress = 0f;
		SceneManager.LoadScene(SceneIndex);
		//Debug.Log (async.progress);
	}

	public void LoadNewSceneIndexAsync(int SceneIndex)
	{
        EventModelsManager.ClearEventModels();
		IsLoading = true;
		SceneHandler.LoadLevelProgress = 0f;
		CurrentlyLoadingScene = SceneIndex;
		StartCoroutine(LoadSceneIndexAsyncWithFade(SceneIndex));
	}
	public void UnloadOldSceneAsync(int SceneIndex){
		SceneManager.UnloadSceneAsync(SceneIndex);
	}

	public IEnumerator FadeOut(float Min, float Max, float FadeTime){
		float i = 0.0f;
		float rate = 1 / FadeTime;
		while (i < 1) { 
			i +=Time.deltaTime * rate;
			_canvasGroup.alpha = Mathf.Lerp (Min, Max, Fade.Evaluate(i));
			yield return null;
		}
		//yield return new WaitForSeconds(2);
	}

	public IEnumerator LoadSceneIndexAsyncWithFade(int SceneIndex){
		LoadingScreen.SetActive (true);
		yield return StartCoroutine(FadeOut(0f,1f, FadeTime));
		yield return StartCoroutine(SceneHandler.LoadAsync(SceneIndex));

//		while (SceneHandler.operation.progress < 0.9f) {
//			yield return null;
//		}
		yield return new WaitForSeconds(FadeTime);
		yield return StartCoroutine (FadeOut (1f, 0f, FadeTime));
		IsLoading = false;
		loadingBar.value = 0f;
		LoadingScreen.SetActive (false);
		
	}

//	public void LoadNewSceneIndexAsyncAdditive(int SceneIndex)
//	{
//		SceneHandler.LoadLevelProgress = 0f;
//		StartCoroutine(SceneHandler.LoadAsyncAdditive(SceneIndex, LoadingScreen));
//		//Add load level for new scene here.
//	}
}
