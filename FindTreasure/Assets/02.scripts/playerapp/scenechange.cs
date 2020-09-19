using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scenechange :MonoBehaviour
{ 
    public static string Qname;
    public static int QButtonClick = -1;

    //public static Stack mUIStack = new Stack();

    public void changeMainScene()
    {
        //gameman.Instance.updatecompet();
        SceneManager.LoadScene("02.Main");
        //mUIStack.Push(1);
        BackSpace.Instance.Clear();
        //mUIStack.Clear();
    }

    public void changeadminScene()
    {
        //mUIStack.Push(SceneManager.GetActiveScene().name);
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("ContestList");
        //mUIStack.Push(2);
    }

    public void ChangeSceneToAdMenu()
    {
        //mUIStack.Push(SceneManager.GetActiveScene().name);
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("administratorMenu"); //관리자 메뉴 화면
        //mUIStack.Push(3);
    }

    public void ChangeSceneToQuizMenu()
    {
        //mUIStack.Push(SceneManager.GetActiveScene().name);
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("QuizMenu");//Quiz리스트 화면
        //mUIStack.Push(4);
    }

    public void ChangeSceneToContestSetting()
    {
        //mUIStack.Push(SceneManager.GetActiveScene().name);
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("ContestMenu");//대회 세팅 화면
        //mUIStack.Push(5);
    }

    public void ChangeSceneToCStartEnd()
    {
        //mUIStack.Push(SceneManager.GetActiveScene().name);
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("CStartEnd");//시작끝시간 페이지 화면
        //mUIStack.Push(6);
    }

    public void ChangeSceneToRealTimeR()
    {
        //mUIStack.Push(SceneManager.GetActiveScene().name);
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("RealTimeR");//실시간 랭킹 화면
        //mUIStack.Push(7);
    }

    
    public void ChangeSceneToAdMenu(GameObject gameObject)
    {
        Qname = gameObject.GetComponentInChildren<Text>().text;
        Debug.Log(Qname);
        ChangeSceneToAdMenu();
    }
    /*
    public void ChangeSceneToPWChange()
    {
        SceneManager.LoadScene("PWChange");//비밀번호 변경화면
        mUIStack.Push(7);
    }


    public void ChangeSceneToModeChange()
    {
        SceneManager.LoadScene("ModeChange");//모드 변경화면
        mUIStack.Push(8);
    }
    */
    public void ChangeSceneToContestClosed()
    {
        //mUIStack.Push(SceneManager.GetActiveScene().name);
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("ContestClosed");//대회폐쇄화면
        //mUIStack.Push(8);
    }

    public void ChangeSceneToQuizType()
    {
        //mUIStack.Push(SceneManager.GetActiveScene().name);
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("QuizType");//Quiz type설정 화면
        //mUIStack.Push(9);
    }

    public void ChangeSceneToQuizAdd()
    {
        //mUIStack.Push(SceneManager.GetActiveScene().name);
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("QuizAdd");//Quiz Add 화면
        //mUIStack.Push(10);
    }
    
    public void ChangeSceneToQuizAddToChange()
    {
        //mUIStack.Push(SceneManager.GetActiveScene().name);
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("QuizAdd");//QuizMenu에서 바로 Quiz Add 화면
        //mUIStack.Push(11);
    }//onclicks에 quizchage랑 연결

    public void OnClicked(GameObject gameObject)
    {
        Qname = gameObject.GetComponentInChildren<Text>().text;
        Debug.Log(Qname);
        ChangeSceneToQuizAddToChange();
    }

    public void Loading()
    {
        //mUIStack.Push(SceneManager.GetActiveScene().name);
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("loading");
    }

    public void ChangeSceneToQuizAdd(int kind)
    {
        PlayerPrefs.SetInt("ButtonClick", kind);
        ChangeSceneToQuizAdd();
    }

    private void Start()
    {

        

    }
    private void Update()
    {/*
        foreach (var item in mUIStack)
        {
            Debug.Log(item + "+++++++++++++++++++++++++"); // 3 번째 2 번째 1 번째  
        }*/
        if (Input.GetKey(KeyCode.Escape))
        {
            BackSpace.Instance.ToString();
            string name= BackSpace.Instance.Pop().ToString();
            SceneManager.LoadScene(name);
            //SceneManager.GetActiveScene().name;
            /*
            string name = mUIStack.Pop().ToString();
            Debug.Log(name);
            switch (name)
            {
                case "1":
                    Application.Quit();
                    break;
                case "2":
                    changeMainScene();
                    break;
                case "3":
                    changeadminScene();
                    //SceneManager.LoadScene("ContestList");
                    break;
                case "4":
                    ChangeSceneToAdMenu();
                    break;
                case "5":
                    ChangeSceneToAdMenu();
                    break;
                case "6":
                    ChangeSceneToAdMenu();
                    break;
                case "7":
                    ChangeSceneToAdMenu();
                    break;
                case "8":
                    ChangeSceneToContestSetting();
                    break;
                case "9":
                    ChangeSceneToQuizMenu();
                    break;
                case "10":
                    ChangeSceneToQuizType();
                    break;
                case "11":
                    ChangeSceneToQuizMenu();
                    break;
            }*/

        }

    }
}
