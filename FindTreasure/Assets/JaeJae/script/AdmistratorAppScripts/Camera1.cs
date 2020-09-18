using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using UnityEngine.Android;

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
		savess = ss;
		savess.Apply();
		selectImage.texture = ss;
		upperbar.SetActive(true);
		RawImagePV.SetActive(true);
	}

	public void TakeScreenShot()
	{
		StartCoroutine(TakeScreenshotPV());
	}

	public void useImage()
	{
		upperbar.SetActive(false);
		RawImagePV.SetActive(false);
		Texture2D texture= new Texture2D(700, 500, TextureFormat.RGB24, false);

		texture.ReadPixels(new Rect(200, 600, 1000, 900),0,0);
		
		texture=ScaleTexture(texture, (int)usingImage.rectTransform.rect.width, (int)usingImage.rectTransform.rect.height);
		texture.Apply();
		usingImage.texture = texture;
		
		
	}

	private Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
	{
		Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);
		Color[] rpixels = result.GetPixels(0);
		float incX = (1.0f / (float)targetWidth);
		float incY = (1.0f / (float)targetHeight);
		for (int px = 0; px < rpixels.Length; px++)
		{
			rpixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth), incY * ((float)Mathf.Floor(px / targetWidth)));
		}
		result.SetPixels(rpixels, 0);
		result.Apply();
		return result;
	}

	/*
	public void saveImage()
    {
		byte[] imageBytes = savess.EncodeToPNG();
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
	}*/
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
		//NativeGallery.OpenSettings();
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
