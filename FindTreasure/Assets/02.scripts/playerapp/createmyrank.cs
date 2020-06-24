using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class createmyrank : MonoBehaviour
{
    public InputField myLanguage, otherLanguage; //입력값2개
    public GameObject prefab, Content;//대회버튼, content화면
    int nextNumber, wordNumber;//대회버튼에 숫자, 단어개수인듯,,?
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //deleted all playerprefs

        wordNumber = PlayerPrefs.GetInt("wordNumber");

        Content.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, wordNumber * 112);

        for (int i = 0; i < wordNumber; i++)
        {
            GameObject go = Instantiate(prefab, prefab.transform.position, Quaternion.identity) as GameObject;
            go.transform.GetChild(0).transform.GetComponent<Text>().text = "" + (i + 1);//number
            string wordString = PlayerPrefs.GetString("words" + i);//내어너니언어합친거버튼에 나타낼문장
            go.transform.GetChild(1).GetComponent<Text>().text = wordString; //number옆text
            go.transform.SetParent(Content.transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void addWords()
    {
        if (myLanguage.text != "" && otherLanguage.text != "")
        {
            if (PlayerPrefs.HasKey("nextNumber"))
                nextNumber = PlayerPrefs.GetInt("nextNumber");

            nextNumber++;
            GameObject go = Instantiate(prefab, prefab.transform.position, Quaternion.identity) as GameObject;
            go.transform.GetChild(0).transform.GetComponent<Text>().text = "" + nextNumber;//number
            string wordString = otherLanguage.text + "=" + myLanguage.text;//내어너니언어합친거버튼에 나타낼문장
            PlayerPrefs.SetString("words" + wordNumber, wordString);

            go.transform.GetChild(1).GetComponent<Text>().text = wordString; //number옆text
            myLanguage.text = "";
            otherLanguage.text = "";
            go.transform.SetParent(Content.transform, false);
            PlayerPrefs.SetInt("nextNumber", nextNumber);
            wordNumber++;
            PlayerPrefs.SetInt("wordNumber" + wordNumber, wordNumber);
            Content.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, wordNumber * 112);

        }
        else
        {
            Debug.Log("empty");
        }
    }

}