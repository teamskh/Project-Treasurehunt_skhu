using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldUtil : MonoBehaviour
{
    private List<GameObject> contents = new List<GameObject>();
    Toggle toggle;
    Slider slider;
    Text score;

    private void OnEnable()
    {
        StartCoroutine(InitInput());
    }

    //hierarchy 안에서 원하는 gameobject 찾기
    IEnumerator InitInput()
    {
        yield return null;
        var Bpanel = GetComponent<QuizFactory>().BasePanel;

        contents?.Add(Bpanel.Find("Title").gameObject ?? null);
        contents?.Add(Bpanel.Find("Context").gameObject ?? null);
        toggle = Bpanel.Find("Toggle").GetComponent<Toggle>() ?? null;
        slider = Bpanel.Find("Slider").GetComponent<Slider>() ?? null;
        score = Bpanel.Find("Score")?.GetComponent<Text>() ?? null;

        //초기화
        toggle.isOn = false;
        slider.value = 1;

        //toggle에 리스너 달기
        toggle.onValueChanged.AddListener((bool bOn)=>IsTrueFalse(bOn));
        slider.onValueChanged.AddListener((float f) =>SliderVale(f));
    }

    //inputFied 채워 넣는 용
    //0번은 Title, 1번은 내용
    public void SetInputFieldString(int i, string context)
    {
        if (i < 0) Debug.LogError($"i : {i}");
        if (contents[i] != null)
        {
            InputField iput = contents[0].GetComponent<InputField>();
            if (iput == null) Debug.LogError("Can't DownCasting to InputField");
            iput.SetTextWithoutNotify(context);
        }
        else
        {
            string input_name = (i == 0) ? "Title" : "Context";
            Debug.LogError($"{input_name} is null");
        }
    }

    //inputField 내용을 받아오는 함수.
    public string GetInputFieldString(int i)
    {
        return contents[i].GetComponent<InputField>().textComponent.text;
    }

    public int GetScore()
    {
        return int.Parse(score.text);
    }

    // 토글을 누를때 마다 불러오는 함수
    void IsTrueFalse(bool On)
    {
        slider.interactable = !On;
        if (On)
            score.text = "30";
        else
            SliderVale(slider.value);
    }

    //값이 변할 때마다 불러오는 함수
    void SliderVale(float f)
    {
        int i = (int)f; //소수점 아래 버림
        slider.value = i;
        score.text = i.ToString();
    }

}
