using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Touch3DObj : MonoBehaviour
{
    private Vector2 touchPosition = default;

    [SerializeField] Text txt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            touchPosition = touch.position;

            if(touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hitObj;
                if(Physics.Raycast(ray,out hitObj))
                {
                    if (hitObj.transform.tag == "uname")
                    {
                        txt.text = hitObj.transform.name;
                    }
                }
            }
        }
    }
}
