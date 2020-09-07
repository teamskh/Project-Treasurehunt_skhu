using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeletePanel : MonoBehaviour
{
    //QuizList내용에서 같이 돌리기 보다 자체 프리펩 내에서 돌릴 수 있게 스크립트 분리
    Button Y;
    Button N;
    public void OnEnable()
    {
        Y = transform.Find("Y").GetComponent<Button>();
        N = transform.Find("N").GetComponent<Button>();
    }
}
