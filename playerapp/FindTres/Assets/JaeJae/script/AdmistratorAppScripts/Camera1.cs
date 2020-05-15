﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1 : MonoBehaviour
{
	public GameObject TakePicture_b;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	// Update is called once per frame
	/*void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (Input.mousePosition.x < Screen.width / 3)
			{
				// Take a screenshot and save it to Gallery/Photos
				StartCoroutine(TakeScreenshotAndSave());
			}
			else
			{
				// Don't attempt to pick media from Gallery/Photos if
				// another media pick operation is already in progress
				if (NativeGallery.IsMediaPickerBusy())
					return;

				if (Input.mousePosition.x < Screen.width * 2 / 3)
				{
					// Pick a PNG image from Gallery/Photos
					// If the selected image's width and/or height is greater than 512px, down-scale the image
					PickImage(512);
				}
				else
				{
					// Pick a video from Gallery/Photos
					//PickVideo();
				}
			}
		}
	}*/
	public void TakeScreenShot()
	{
		StartCoroutine(TakeScreenshotAndSave());
	}
	private IEnumerator TakeScreenshotAndSave()
	{
		TakePicture_b.SetActive(false);
		yield return new WaitForEndOfFrame();
		Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		ss.Apply();

		// Save the screenshot to Gallery/Photos
		Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(ss, "GalleryTest", "Image.png"));

		// To avoid memory leaks
		Destroy(ss);
		TakePicture_b.SetActive(true);
	}

	public  void PickImage(int maxSize)
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

				// Assign texture to a temporary quad and destroy it after 5 seconds
				GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
				quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
				quad.transform.forward = Camera.main.transform.forward;
				quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

				Material material = quad.GetComponent<Renderer>().material;
				if (!material.shader.isSupported) // happens when Standard shader is not included in the build
					material.shader = Shader.Find("Legacy Shaders/Diffuse");

				material.mainTexture = texture;

				Destroy(quad, 5f);

				// If a procedural texture is not destroyed manually, 
				// it will only be freed after a scene change
				Destroy(texture, 5f);
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

	public void ChangeSceneToCamera()
	{
		Application.LoadLevel("CameraSample");//카메라
	}
}
