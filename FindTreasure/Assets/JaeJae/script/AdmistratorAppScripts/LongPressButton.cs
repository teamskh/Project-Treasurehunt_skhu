﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongPressButton : UIBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{

    public UnityEvent onClick;
    //private bool btnDown = false;
    public GameObject BPrefab;
    public float durationThreshold = 1f;
    public float duration = 1.3f;
    public UnityEvent onLongPress = new UnityEvent();
    public UnityEvent setActive = new UnityEvent();
    public bool isPointerDown = false;
    public bool longPressTriggered = false;
    public float timePressStarted;
    

    public void OnPointerDown(PointerEventData eventData)
    {
        //held = false;
        //GameObject AskD = gameObject.GetComponent<QuizList>().AskD;
        BPrefab.GetComponent<Button>()?.onClick.AddListener(SceneChange);
        Invoke("OnLongPress", durationThreshold);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke("OnLongPress");

        //if (!held)
        // onClick.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CancelInvoke("OnLongPress");
    }

    private void OnLongPress()
    {
        //held = true;
        BPrefab.GetComponent<Button>()?.onClick.RemoveListener(SceneChange);
        gameObject.AddComponent<QuizList>().SetActive();
        onLongPress.Invoke();
        Debug.Log("OnLongPressed!!");
    }

  

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log(name + "Game Object Clicked!", this);

        onClick.Invoke();
    }

    void SceneChange()
    {
        gameObject.GetComponent<scenechange>().OnClicked(BPrefab);
    }

    /*
    private void Update()
    {
        if (isPointerDown && !longPressTriggered)
        {
            if (Time.time - timePressStarted > durationThreshold)
            {
                longPressTriggered = true;
                onLongPress.Invoke();
                Debug.Log("OnLongPressed!!");

                AskD.SetActive(true);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        timePressStarted = Time.time;
        isPointerDown = true;
        longPressTriggered = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerDown = false;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log(name + "Game Object Clicked!", this);

        onClick.Invoke();
    }*/

    /*
    void Update()
    {
        if (btnDown)
            Debug.Log("Button Touch");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        btnDown = true;
        if (eventData.clickTime >= 0.5f)
        {
            gameObject.AddComponent<QuizList>().Active();
        }
        else
        {
            BPrefab.GetComponent<Button>()?.onClick.AddListener(() => gameObject.AddComponent<scenechange>().OnClicked(BPrefab));
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        btnDown = false;
    }*/

}
