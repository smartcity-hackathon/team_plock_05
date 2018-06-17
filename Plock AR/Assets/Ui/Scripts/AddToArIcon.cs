using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddToArIcon : MonoBehaviour {

	bool isDownloading;
	public bool isDownloaded = true;
	public GameObject seeButton;
	public GameObject downloadProgressButton;
	public Slider progressBar;
	public GameObject downloadButton;
	float downloadProgress = 0f;

	void Start()
	{		
		downloadProgressButton.SetActive(false);
		if (!isDownloaded)
		{
			stateDownload();
		}
		else
		{
			stateSee();
		}

	}

	public void ToggleAction()
	{
		if (!isDownloaded)
		{
			DownloadAsset();
			Debug.Log("Downloading asset");
		}
		else
		{
			SeeAssetInAR();
			Debug.Log("Augmenting asset");
		}
	}

	void stateSee()
	{
		seeButton.SetActive(true);
		downloadProgressButton.SetActive(false);
		downloadButton.SetActive(false);		
	}
	void stateDownload()
	{
		seeButton.SetActive(false);
		downloadButton.SetActive(true);
	}

	void stateDownloading()
	{
		progressBar.value = 0f;
		downloadButton.SetActive(false);
		downloadProgressButton.SetActive(true);
	}

	void DownloadAsset()
	{
		//Access a public static download manager
		isDownloading = true;
		stateDownloading();
		int myNewTween = LeanTween.value(progressBar.gameObject,progressBar.value,1f,5f).id;
		LTDescr d = LeanTween.descr(myNewTween).setOnUpdate((float val) =>{progressBar.value = val;});
		if (d != null)
		{
			d.setOnComplete(stateSee);
		}

	}
	void CancelAssetDownload()
	{
		//Cancel download and go back to Download state
	}
	void SeeAssetInAR()
	{
		//Minimize interface and go to AR view
	}
}
