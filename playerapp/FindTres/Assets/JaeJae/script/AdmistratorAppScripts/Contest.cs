using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class Contest : MonoBehaviour
{
    public string exam; //여러개로
    public InputField ContestName_infT;
    public InputField ContestTN_infT;
    public InputField ContestPw_infT;
    public int wordNumber;
    public GameObject ContestBPrefab;
    public GameObject Content;

    static Contest instance;
    public static Contest Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {

        Content.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, wordNumber * 112);

        for (int i = 0; i < wordNumber; i++)
        {
            GameObject CBPrefab = Instantiate(ContestBPrefab, ContestBPrefab.transform.position, Quaternion.identity) as GameObject;
            //CBPrefab.transform.GetChild(0).transform.GetComponent<Text>().text = "" + (i + 1);//number
            string loadContestName = PlayerPrefs.GetString("words" + i);//내어너니언어합친거버튼에 나타낼문장
            string loadTeamMNumber = PlayerPrefs.GetString("TeamMember" + i);
            string loadContestPW = PlayerPrefs.GetString("ContestPassword" + i);
            wordNumber = PlayerPrefs.GetInt("wordNumber");
            PlayerPrefs.SetString("words" + wordNumber, loadContestName);
            PlayerPrefs.SetString("TeamMember" + wordNumber, loadTeamMNumber);
            PlayerPrefs.SetString("ContestPassword" + wordNumber, loadContestPW);
            CBPrefab.transform.GetChild(1).GetComponent<Text>().text = loadContestName; //number옆text
            CBPrefab.transform.SetParent(Content.transform, false);
        }
    }

}