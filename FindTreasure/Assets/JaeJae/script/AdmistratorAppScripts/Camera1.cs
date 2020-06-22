using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class Camera1 : MonoBehaviour
{
	public GameObject TakePicture_b;
	public GameObject RawImagePV;
	public GameObject upperbar;
	public RawImage selectImage;
	public RawImage usingImage;
	public static Texture2D savess;
	public GameObject CselectB;
	public GameObject Image;
	void Start()
    {
		RawImagePV.SetActive(false);
		upperbar.SetActive(false);
		CselectB.SetActive(false);
		TakePicture_b.SetActive(false);
		Image.SetActive(true);
	}
	
	IEnumerator TakeScreenshotPV()
	{
		TakePicture_b.SetActive(false);
		yield return new WaitForEndOfFrame();
		Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		ss.Apply();
		upperbar.SetActive(true);
		RawImagePV.SetActive(true);
		selectImage.texture = ss;

		savess = ss;
		savess.Apply();
	}

	public void TakeScreenShot()
	{
		StartCoroutine(TakeScreenshotPV());

	}

	public void useImage()
	{
		upperbar.SetActive(false);
		RawImagePV.SetActive(false);
		
		usingImage.texture = savess;
		savess.Apply();
		
	}

	public void saveImage()
    {
		byte[] imageBytes = savess.EncodeToPNG();
		/*
		//Save image to file 
		System.IO.File.WriteAllBytes(path, imageBytes);*/
		var dirPath = Application.dataPath + "/Resources/Texture/";
		if (!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
		}
		File.WriteAllBytes(dirPath + onClicks.Ttitle + ".png", imageBytes);
		Debug.Log(onClicks.Ttitle);
#if UNITY_EDITOR
		AssetDatabase.ImportAsset(dirPath + onClicks.Ttitle + ".png");
#endif
	}
	/*
		public void SaveImage()
		{
			//ImageV.gameObject.SetActive(true);
			Texture2D saveImage = (Texture2D)selectImage.texture;
			Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(saveImage, "GalleryTest", "Image.png"));

			// To avoid memory leaks
			Destroy(saveImage);
			//TakePicture_b.SetActive(true);
		}
		*/
	public void PickImage(int maxSize)
	{
		NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
		{
			Debug.Log("Image path: " + path);
			if (path != null)
			{
				// Create Texture from selected image
				Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
				if (texture == null)
				{
					Debug.Log("Couldn't load texture from " + path);
					return;
				}
				
				usingImage.texture = texture;
			}
		}, "Select a PNG image", "image/png");

		Debug.Log("Permission result: " + permission);
	}

	/*	private void PickVideo()
		{
			NativeGallery.Permission permission = NativeGallery.GetVideoFromGallery((path) =>
			{
				Debug.Log("Video path: " + path);
				if (path != null)
				{
					// Play the selected video
					Handheld.PlayFullScreenMovie("file://" + path);
				}
			}, "Select a video");

			Debug.Log("Permission result: " + permission);
		}*/
}
