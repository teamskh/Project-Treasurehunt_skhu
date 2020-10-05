using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class RayButtons : MonoBehaviour
{
    ARRaycastManager rayManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        rayManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        if(touch.phase == TouchPhase.Began)
        {
            if (rayManager == null) return;
            if (rayManager.Raycast(touch.position, hits))
            {
                foreach(var button in hits)
                {
                    Debug.Log(button.trackableId.ToString());
                }
            }
        }
    }
}
