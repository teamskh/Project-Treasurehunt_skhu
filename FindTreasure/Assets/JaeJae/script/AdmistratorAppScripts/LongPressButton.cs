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

    public void OnPointerDown(PointerEventData eventData)
    {
        if(GameObject.Find("GameSetting")==true)
            GetComponent<Button>()?.onClick.AddListener(SceneChange);
        else if (GameObject.Find("GameManager") == true)
        {
            AdminCurState.Instance.Competition = GetComponent<Button>()?.GetComponentInChildren<Text>().text;
            GetComponent<Button>()?.onClick.AddListener(()=>ActivePanel());
        }
        Invoke("OnLongPress", durationThreshold);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke("OnLongPress");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CancelInvoke("OnLongPress");
    }

    private void OnLongPress()
    {
        if (GameObject.Find("GameSetting") == true)
        {
            AdminCurState.Instance.Quiz = GetComponent<Button>()?.GetComponentInChildren<Text>().text;
            GameObject.Find("GameSetting")?.GetComponent<QuizList>().SetActive();
        }
        else if (GameObject.Find("GameManager") == true)
        {
            GetComponent<Button>()?.onClick.RemoveListener(ActivePanel);
            AdminCurState.Instance.Competition= GetComponent<Button>()?.GetComponentInChildren<Text>().text;
            GameObject.Find("GameManager")?.GetComponent<CompetitionToServer>().SetActive();
        }
        onLongPress.Invoke();
        Debug.Log("OnLongPressed!!");
    }

    void SceneChange()
    {
        gameObject.GetComponent<scenechange>().OnClicked(gameObject);
    }
    void ActivePanel()
    {
        GameObject.Find("GameManager")?.GetComponent<CompetitionToServer>().Active();
    }
}