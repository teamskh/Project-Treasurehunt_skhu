using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TTM.Classes;

public class competinfo : MonoBehaviour
{
    /*
    public InputField ContestName_infT;
    public Dropdown ContestTN_dbox;
    public InputField ContestPw_infT;
    public Toggle Team;
    public Toggle individual;
    public GameObject ContestBPrefab; //대회버튼
    public GameObject Content;
    public GameObject all_t;
    public void Start()
    {
        Competition compet = new Competition();
        Team.isOn = compet.Mode;
        
        if (compet.Mode)
        {
            compet.MaxMember = ContestTN_dbox.value + 2;
        }

        ContestPw_infT.text= compet.Password;
    }
    public void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))

            {
                SceneManager.LoadScene("ContestList");
            }
        }
        if (Input.GetKey(KeyCode.Escape))

        {
            SceneManager.LoadScene("ContestList");
        }
    }
    public void SaveContest()
    {
        Competition compet = new Competition();

        if (ContestName_infT.text.Length < 1)
        {
            StartCoroutine(setActiveObjinSecond(all_t, 1f));
            return;
        }

        if (ContestPw_infT.text.Length < 1)
        {
            StartCoroutine(setActiveObjinSecond(all_t, 1f));
            return;
        }

        compet.Mode = Team.isOn;

        if (compet.Mode)
        {
            compet.MaxMember = ContestTN_dbox.value + 2;
        }

        compet.Password = ContestPw_infT.text;

        GetComponent<CompetDic>().AddContest(ContestName_infT.text, compet);

    }
    IEnumerator setActiveObjinSecond(GameObject gameObject, float second)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(second);
        gameObject.SetActive(false);
    }
*/
}
