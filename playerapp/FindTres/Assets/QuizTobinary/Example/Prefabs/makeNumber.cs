using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class makeNumber : MonoBehaviour
{
    //input field size:4 각각 text등록
    [SerializeField]
    private Text[] txts;
    //input field size:4 각각 text등록
    [SerializeField]
    private Text[] nos;

    private SerializeDic dic;

    // Start is called before the first frame update
    void Start()
    {
        dic = GetComponent<SerializeDic>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MakenuTXT()
    {
        for(int i = 0; i < txts.Length; i++) {
            nos[i].text = txts[i].text;
        }
    }
    public void Init()
    {
        foreach(Text text in txts)
        {
            text.text = "";
        }
    }
    public void makelist()
    {
        dic.ans.list = new string[4];
        for(int i = 0; i < nos.Length; i++)
        {
            dic.ans.list[i] = nos[i].text;
        }
    }
}
