﻿using System;
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
    private static event Action<string> ScrollRemove;
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

    IEnumerator Enqueue(string name,GameObject trackImg)
    {
        ARobj.Add(name, trackImg);
        nameTable.Enqueue(name);
        if (nameTable.Count > trackImageManager.maxNumberOfMovingImages) Dequeue();
        yield return null;
        trackImg.GetComponentInChildren<Scroll>().Init(name);
       
    }

    #endregion

    void Start()
    {
        Debug.Log("Creating Runtime Mutable Image Library\n");
        try
        {
            trackImageManager = gameObject.GetComponent<ARTrackedImageManager>();
            if (trackImageManager != null) Debug.Log("Not Manager");
            trackImageManager.referenceLibrary = trackImageManager.CreateRuntimeLibrary();
            if (trackImageManager.referenceLibrary is MutableRuntimeReferenceImageLibrary) Debug.Log("Not ReferenceLibrary");
            trackImageManager.maxNumberOfMovingImages = 3;

            trackImageManager.enabled = true;

            //trackImageManager.trackedImagesChanged += OnTrackedImagesChanged;

            ShowTrackerInfo();
            // Debug.Log("Not MutableLibrary");

            PlayerContents.Instance.setLib(MakeLibrary);
            
        }catch(Exception e)
        {
            Debug.Log(e.StackTrace);
        }
        ScrollRemove = (string name) => clear(name);
    }
    

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


    void OnDisable()
    {
        trackImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

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

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        try
        {
            foreach (ARTrackedImage trackedImage in eventArgs.added)
            {
                // Display the name of the tracked image in the canvas
                UpdateARImage(trackedImage);
                pos = trackedImage.transform.position;
            }

            foreach (ARTrackedImage trackedImage in eventArgs.updated)
            {
                // Display the name of the tracked image in the canvas
                UpdateARImage(trackedImage);
                if (trackedImage.transform.rotation.x != 90)
                    trackedImage.transform.Rotate(Vector3.right, 90);
            }
        }
        catch(NullReferenceException e)
        {
            Debug.Log(e.StackTrace);
        }
    }

    private void UpdateARImage(ARTrackedImage trackedImage)
    {
        // Assign and Place Game Object
        AssignGameObject(trackedImage.referenceImage.name, trackedImage);
    }

    void AssignGameObject(string name, ARTrackedImage img)
    {
        GameObject obj;
        if (!ARobj.ContainsKey(name)) StartCoroutine(Enqueue(name, img.gameObject));
        else
        {
            if (ARobj.TryGetValue(name, out obj))
            {
                obj.transform.position = new Vector3(0, 0, 1.5f);
                obj.transform.localScale = scaleFactor;
            }
        }
    }


    void MakeLibrary()
    {
        trackImageManager.referenceLibrary = trackImageManager.CreateRuntimeLibrary();

        List<Texture2D> lists = PlayerContents.Instance.getLib();
        foreach(var txtur in lists)
        {
            StartCoroutine( AddImageJob(txtur, txtur.name));
        }
        trackImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }
    
    void clear(string name)
    {
        GameObject obj;
        ARobj.TryGetValue(name, out obj);
        if (obj != null) Destroy(obj);
    }

    public static void CallDestroy(string name)
    {
        ScrollRemove(name);
    }
    
}
