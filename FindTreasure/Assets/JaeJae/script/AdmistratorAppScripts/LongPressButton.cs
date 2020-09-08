using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongPressButton : UIBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public UnityEvent onClick;
    public float durationThreshold = 1f;
    public UnityEvent onLongPress = new UnityEvent();
    public static string Oname;

    public void OnPointerDown(PointerEventData eventData)
    {
        //held = false;
        if(GameObject.Find("GameSetting")==true)
            GetComponent<Button>()?.onClick.AddListener(SceneChange);
        else if(GameObject.Find("GameManager")==true)
            GetComponent<Button>()?.onClick.AddListener(Change);
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
            GetComponent<Button>()?.onClick.RemoveListener(SceneChange);
            Oname=GetComponent<Button>()?.GetComponentInChildren<Text>().text;
            GameObject.Find("GameSetting")?.GetComponent<QuizList>().SetActive();
            //Hierchy에 QuizList가 2개가 되고, GameSetting에서 설정한 베이스 세팅을 AddComponent()과정에서 해주지 않아 문제 발생
        }
        else if (GameObject.Find("GameManager") == true)
        {
            GetComponent<Button>()?.onClick.RemoveListener(Change);
            Oname = GetComponent<Button>()?.GetComponentInChildren<Text>().text;
            GameObject.Find("GameManager")?.GetComponent<CompetitionToServer>().SetActive();
        }
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
        gameObject.GetComponent<scenechange>().OnClicked(gameObject);
    }

    void Change()
    {
        gameObject.GetComponent<scenechange>().ChangeSceneToAdMenu(gameObject);
    }
}