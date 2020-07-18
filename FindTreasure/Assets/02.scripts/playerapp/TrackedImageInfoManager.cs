using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageInfoManager : MonoBehaviour
{
    ARTrackedImageManager m_TrackedImageManager;
    public GameObject scroll;

    void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            Debug.Log("Pic Name: " + trackedImage.referenceImage.name);
            gameman.Instance.imageText = trackedImage.referenceImage.name;
            if(scroll !=null)
            {
                scroll.SetActive(true);
            }
        }
        foreach (var trackedImage in eventArgs.updated)
        {
            Debug.Log("Pic Update"+trackedImage.referenceImage.name);
            gameman.Instance.imageText = trackedImage.referenceImage.name;
        }
        foreach (var trackedImage in eventArgs.removed)
        {
            Debug.Log("Pic Deleted");
            gameman.Instance.imageText = trackedImage.referenceImage.name;
            if(scroll != null)
            {
                scroll.SetActive(false);
            }
        }
    }
}
