using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongPressButton : UIBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private UnityEvent onClick;
    private float durationThreshold = 1f;
    private UnityEvent onLongPress = new UnityEvent();
    public static string Oname;
    public static string Bname;

    public void OnPointerDown(PointerEventData eventData)
    {
        //held = false;
        if(GameObject.Find("GameSetting")==true)
            GetComponent<Button>()?.onClick.AddListener(SceneChange);
        else if (GameObject.Find("GameManager") == true)
        {
            Bname = GetComponent<Button>()?.GetComponentInChildren<Text>().text;
            GetComponent<Button>()?.onClick.AddListener(()=>ActivePanel());
            //GetComponent<Button>()?.onClick.AddListener(Change);
        }
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
        if (GameObject.Find("GameSetting") == true)
        {
            Oname=GetComponent<Button>()?.GetComponentInChildren<Text>().text;
            GameObject.Find("GameSetting")?.GetComponent<QuizList>().SetActive();
            //Hierchy에 QuizList가 2개가 되고, GameSetting에서 설정한 베이스 세팅을 AddComponent()과정에서 해주지 않아 문제 발생
        }
        else if (GameObject.Find("GameManager") == true)
        {
            //GetComponent<Button>()?.onClick.RemoveListener(Change);
            GetComponent<Button>()?.onClick.RemoveListener(ActivePanel);
            Oname = GetComponent<Button>()?.GetComponentInChildren<Text>().text;
            GameObject.Find("GameManager")?.GetComponent<CompetitionToServer>().SetActive();
        }
        onLongPress.Invoke();
        Debug.Log("OnLongPressed!!");
    }

    void SceneChange()
    {
        gameObject.GetComponent<scenechange>().OnClicked(gameObject);
    }

    public void Change()
    {
        gameObject.GetComponent<scenechange>().ChangeSceneToAdMenu(gameObject);
    }

    void ActivePanel()
    {
        //gameObject.GetComponent<PanelScript>().setNumber(2);
        GameObject.Find("GameManager")?.GetComponent<CompetitionToServer>().Active();
    }
}