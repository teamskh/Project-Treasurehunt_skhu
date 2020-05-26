using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
public class Quizlibrary : MonoBehaviour
{
    public string ID;

    ARTrackedImageManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<ARTrackedImageManager>();

        manager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        manager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            // Display the name of the tracked image in the canvas
            ID = trackedImage.referenceImage.name;
            // Give the initial image a reasonable default scale
            trackedImage.transform.localScale =
                new Vector3(-trackedImage.referenceImage.size.x, 0.005f, -trackedImage.referenceImage.size.y);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            // Display the name of the tracked image in the canvas
            ID = trackedImage.referenceImage.name;
            // Give the initial image a reasonable default scale
            trackedImage.transform.localScale =
                new Vector3(-trackedImage.referenceImage.size.x, 0.005f, -trackedImage.referenceImage.size.y);
        }
    }

}
