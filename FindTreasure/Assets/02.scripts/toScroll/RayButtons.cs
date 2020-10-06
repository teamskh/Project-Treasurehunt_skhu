using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class RayButtons : MonoBehaviour
{
    ARRaycastManager rayManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    Camera arcam;
    string title;

    void Start()
    {
        rayManager = GetComponent<ARRaycastManager>();
        arcam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        if(touch.phase == TouchPhase.Began)
        {
            Ray ray = arcam.ScreenPointToRay(touch.position);
            RaycastHit hitObj;
            if (Physics.Raycast(ray, out hitObj))
            {
                var check = hitObj.transform.name;
                var answers = check.Split('/');
                if (answers.Length == 2)
                {
                    if (answers[0] == title)
                        Player.Instance.CheckAnswer(answers[0], answers[1]);
                    else
                    {
                        title = answers[0];
                        Debug.Log(hitObj.transform.parent.name);
                    }
                }
                hitObj.transform.GetComponent<ARButtons>()?.CheckAns();
            }
        }
    }
}
