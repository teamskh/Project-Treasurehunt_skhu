using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlacementforPointCloud : MonoBehaviour
{
    [SerializeField]
    private GameObject placedPrefabs;
    /*
    [SerializeField]
    private GameObject welcomPanel;
    [SerializeField]
    privated Button dismissButton;
    */
    private Vector2 touchPosition = default;

    private ARRaycastManager arRaycastManager;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        //dismissButton.onClick.AddListener(Dissmiss);
    }

    //private void Dismiss()=>welcompPanel.SetActive(false);

   void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchPosition = touch.position;
                if(arRaycastManager.Raycast(touchPosition,hits,UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    var hitPose = hits[0].pose;
                    Instantiate(placedPrefabs, hitPose.position, hitPose.rotation);
                }
            }
        }
    }
}
