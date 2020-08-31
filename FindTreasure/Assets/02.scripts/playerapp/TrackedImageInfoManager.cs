using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TTM.Classes;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageInfoManager : MonoBehaviour
{
    ARTrackedImageManager m_TrackedImageManager;

    [SerializeField]
    private string debugLog;

    [SerializeField]
    private Text jobLog;

    [SerializeField]
    private Text currentImageText;

    [SerializeField]
    private Button captureImageButton;

    [SerializeField]
    private GameObject Scroll;

    [SerializeField]
    private Vector3 scaleFactor = new Vector3(0.1f, 0.1f, 0.1f);

    [SerializeField]
    private XRReferenceImageLibrary runtimeImageLibrary;

    private ARTrackedImageManager trackImageManager = new ARTrackedImageManager();

    Queue<string> nameTable = new Queue<string>();
    Dictionary<string, GameObject> ARobj = new Dictionary<string, GameObject>();
    Vector3 pos;

    #region Controll ARObjects
    string Dequeue()
    {
        var name = nameTable.Dequeue();
        GameObject obj;
        ARobj.TryGetValue(name, out obj);
        Destroy(obj);
        ARobj.Remove(name);
        return name;
    }

    IEnumerator Enqueue(string name)
    {
        GameObject obj = Instantiate(Scroll);
        ARobj.Add(name, obj);
        nameTable.Enqueue(name);
        if (nameTable.Count > trackImageManager.maxNumberOfMovingImages) Dequeue();

        yield return null;

        Q item = PlayerContents.Instance.FindQ(name);
        obj.GetComponent<Scroll>()?.Init(item);
    }

    #endregion

    void Start()
    {
        Debug.Log("Creating Runtime Mutable Image Library\n");
        try
        {
            trackImageManager = gameObject.GetComponent<ARTrackedImageManager>();
            if (trackImageManager != null) Debug.Log("Not Manager");
            trackImageManager.referenceLibrary = trackImageManager.CreateRuntimeLibrary(runtimeImageLibrary);
            if (trackImageManager.referenceLibrary is MutableRuntimeReferenceImageLibrary) Debug.Log("Not ReferenceLibrary");
            trackImageManager.maxNumberOfMovingImages = 3;
            trackImageManager.trackedImagePrefab = Scroll;

            trackImageManager.enabled = true;

           // trackImageManager.trackedImagesChanged += OnTrackedImagesChanged;

            ShowTrackerInfo();
            // Debug.Log("Not MutableLibrary");

            MakeLibrary();
            // captureImageButton.onClick.AddListener(() => StartCoroutine(CaptureImage()));
        }catch(Exception e)
        {
            Debug.Log(e.StackTrace);
        }
    }
    
   //private IEnumerator CaptureImage()
   //{
   //    yield return new WaitForEndOfFrame();
   //
   //    jobLog.text = "Capturing Image...";
   //
   //    var texture = ScreenCapture.CaptureScreenshotAsTexture();
   //
   //    StartCoroutine(AddImageJob(texture));
   //}

    public void ShowTrackerInfo()
    {
        var runtimeReferenceImageLibrary = trackImageManager.referenceLibrary as MutableRuntimeReferenceImageLibrary;
        if (runtimeReferenceImageLibrary != null)
        {
            debugLog += $"TextureFormat.RGBA32 supported: {runtimeReferenceImageLibrary.IsTextureFormatSupported(TextureFormat.RGBA32)}\n";
            debugLog += $"Supported Texture Count ({runtimeReferenceImageLibrary.supportedTextureFormatCount})\n";
            debugLog += $"trackImageManager.trackables.count ({trackImageManager.trackables.count})\n";
           // debugLog += $"trackImageManager.trackedImagePrefab.name ({trackImageManager.trackedImagePrefab.name})\n";
            debugLog += $"trackImageManager.maxNumberOfMovingImages ({trackImageManager.maxNumberOfMovingImages})\n";
            debugLog += $"trackImageManager.supportsMutableLibrary ({trackImageManager.subsystem.SubsystemDescriptor.supportsMutableLibrary})\n";
            debugLog += $"trackImageManager.requiresPhysicalImageDimensions ({trackImageManager.subsystem.SubsystemDescriptor.requiresPhysicalImageDimensions})\n";

            Debug.Log(debugLog);
        }
        else
            Debug.Log("Mutable Error");

    }
   // void OnDisable()
   // {
   //     trackImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
   // }

    public IEnumerator AddImageJob(Texture2D texture2D,string name)
    {
        yield return null;

        debugLog= string.Empty;

       debugLog += "Adding image\n";

        //jobLog.text = "Job Starting...";

        var firstGuid = new SerializableGuid(0, 0);
        var secondGuid = new SerializableGuid(0, 0);

        XRReferenceImage newImage = new XRReferenceImage(firstGuid, secondGuid, new Vector2(0.1f, 0.1f), name, texture2D);

        try
        {
            MutableRuntimeReferenceImageLibrary mutableRuntimeReferenceImageLibrary = trackImageManager.referenceLibrary as MutableRuntimeReferenceImageLibrary;

            debugLog += $"TextureFormat.RGBA32 supported: {mutableRuntimeReferenceImageLibrary.IsTextureFormatSupported(TextureFormat.RGBA32)}\n";

            debugLog += $"TextureFormat size: {texture2D.width}px width {texture2D.height}px height\n";
            Debug.Log(debugLog);

            //var jobHandle = mutableRuntimeReferenceImageLibrary.ScheduleAddImageJob(texture2D, Guid.NewGuid().ToString(), 0.1f);
            mutableRuntimeReferenceImageLibrary.ScheduleAddImageJob(texture2D, name, texture2D.width);
            //while (!jobHandle.IsCompleted)
            //{
            //   // jobLog.text = "Job Running...";
            //}

            debugLog = string.Empty;
            // jobLog.text = "Job Completed...";
             debugLog += $"Job Completed ({mutableRuntimeReferenceImageLibrary.count})\n";
             debugLog += $"Supported Texture Count ({mutableRuntimeReferenceImageLibrary.supportedTextureFormatCount})\n";
             debugLog += $"trackImageManager.trackables.count ({trackImageManager.trackables.count})\n";
            // debugLog += $"trackImageManager.trackedImagePrefab.name ({trackImageManager.trackedImagePrefab.name})\n";
             debugLog += $"trackImageManager.maxNumberOfMovingImages ({trackImageManager.maxNumberOfMovingImages})\n";
             debugLog += $"trackImageManager.supportsMutableLibrary ({trackImageManager.subsystem.SubsystemDescriptor.supportsMutableLibrary})\n";
            debugLog += $"trackImageManager.requiresPhysicalImageDimensions ({trackImageManager.subsystem.SubsystemDescriptor.requiresPhysicalImageDimensions})\n";
            Debug.Log(debugLog);
        }
        catch (Exception e)
        {
            if (texture2D == null)
            {
                //debugLog.text += "texture2D is null";
            }
            //debugLog.text += $"Error: {e.ToString()}";
        }
    }

  //  void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
  //  {
  //     // foreach (ARTrackedImage trackedImage in eventArgs.added)
  //     // {
  //     //     // Display the name of the tracked image in the canvas
  //     //     UpdateARImage(trackedImage);
  //     //     pos = trackedImage.transform.position;
  //     //     trackedImage.transform.Rotate(Vector3.up, 180);
  //     // }
  //     //
  //     // foreach (ARTrackedImage trackedImage in eventArgs.updated)
  //     // {
  //     //     // Display the name of the tracked image in the canvas
  //     //     UpdateARImage(trackedImage);
  //     //     trackedImage.transform.Rotate(Vector3.up, 180);
  //     // }
  //  }

    private void UpdateARImage(ARTrackedImage trackedImage)
    {
        // Display the name of the tracked image in the canvas
        if (Vector3.Distance(pos, trackedImage.transform.position) > 100)
        {
            pos = trackedImage.transform.position;
            Debug.Log($"Img Pos: {trackedImage.transform.position}");
            Debug.Log($"trackedImage.referenceImage.name: {trackedImage.referenceImage.name}");
        }

        // Assign and Place Game Object
        AssignGameObject(trackedImage.referenceImage.name, trackedImage.transform.position);
    }

    void AssignGameObject(string name, Vector3 newPosition)
    {
        GameObject goARObject;

        if (!ARobj.ContainsKey(name)) StartCoroutine(Enqueue(name));

        if (ARobj.TryGetValue(name, out goARObject)){

            goARObject.transform.position =Vector3.zero;
            goARObject.transform.localScale = scaleFactor;
        } 
    }


    void MakeLibrary()
    {
        //var txtures = Resources.LoadAll<Texture2D>("FindTreasure/Images");
        
        //아래 내용은 DownloadFiles 내용이 정상적으로 작동할 때 해제
        var competition = PlayerPrefs.GetInt("p_competition");
        var txtures = Resources.LoadAll<Texture2D>("FindTreasure/IMG" +competition);

        foreach (var txt in txtures)
        {   
            StartCoroutine(AddImageJob(CopyTexture(txt),txt.name));
        }
    }

    public Texture2D CopyTexture(Texture2D texture)
    {
        // Create a temporary RenderTexture of the same size as the texture
        RenderTexture tmp = RenderTexture.GetTemporary(
                            texture.width,
                            texture.height,
                            0,
                            RenderTextureFormat.Default,
                            RenderTextureReadWrite.Linear);

        // Blit the pixels on texture to the RenderTexture
        Graphics.Blit(texture, tmp);

        // Backup the currently set RenderTexture
        RenderTexture previous = RenderTexture.active;

        // Set the current RenderTexture to the temporary one we created
        RenderTexture.active = tmp;

        // Create a new readable Texture2D to copy the pixels to it
        Texture2D myTexture2D = new Texture2D(texture.width, texture.height);

        // Copy the pixels from the RenderTexture to the new Texture
        myTexture2D.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
        myTexture2D.Apply();

        // Reset the active RenderTexture
        RenderTexture.active = previous;

        // Release the temporary RenderTexture
        RenderTexture.ReleaseTemporary(tmp);

        // "myTexture2D" now has the same pixels from "texture" and it's readable.

        return myTexture2D;
    }

}
