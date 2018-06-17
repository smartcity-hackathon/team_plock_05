﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class SceneHandler
{
	public static float LoadLevelProgress = 0f;
	public static AsyncOperation operation;

	public static float LoadLevelOpacity = 0f;
	public static AnimationCurve OpacityChangeCurve;
	//public SceneField LoadingScreen;


	public static IEnumerator LoadAsync(int index)
	{ 
		bool isLoading = true;
		if (isLoading == true)
		{
			isLoading = false;
		SceneHandler.LoadLevelProgress = 0f;
		operation = SceneManager.LoadSceneAsync(index);
		//operation.allowSceneActivation = false;

		while(!operation.isDone) {
			yield return operation.isDone;
			LoadLevelProgress = Mathf.Clamp01 (operation.progress / 0.9f);
			}
			isLoading = true;
		}
	}

	public static IEnumerator LoadAsyncAdditive(int index, string LoadingScreen)
	{
		SceneManager.LoadScene (LoadingScreen, LoadSceneMode.Additive);
		yield return new WaitForSeconds (1f);
		operation = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);

		operation.allowSceneActivation = false;

		while(LoadLevelProgress < 1f) {
			LoadLevelProgress = Mathf.Clamp01 (operation.progress / 0.9f);
			Debug.Log (LoadLevelProgress);
			//Destroy (LoadingScreen);
			//  Debug.Log("loading progress: " + operation.progress);
			yield return null;
		}
		operation.allowSceneActivation = true;
		while (LoadLevelProgress >= 1f) {
			SceneManager.UnloadSceneAsync (LoadingScreen);
			//SceneHandler.LoadLevelProgress = 0f;
		}
	}
		
}