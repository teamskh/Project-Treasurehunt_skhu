using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TTM.Classes;

public class competinfo : MonoBehaviour
{
    public InputField ContestName_infT;
    public Dropdown ContestTN_dbox;
    public InputField ContestPw_infT;
    public Toggle Team;
    public Toggle individual;
    public GameObject ContestBPrefab; //대회버튼
    public GameObject Content;
    public GameObject all_t;
    Competition compet = new Competition();
    public void Start()
    {
        string key = scenechange.Qname;
        if (adminManager.Instance.CallCompetDic().TryGetValue(key, out compet))
        {
            Debug.Log(compet.Mode);
            ContestPw_infT.text = compet.Password;
            Debug.Log(compet.Password);
            ContestName_infT.text = key;
        }
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
   public void PasswordLoad()
    {
        ContestPw_infT.text= compet.Password;
    }
    public void PasswordSave()
    {
        if (ContestPw_infT.text.Length < 1)
        {
            StartCoroutine(setActiveObjinSecond(all_t, 1f));
            return;
        }
        compet.Password= ContestPw_infT.text;
    }
    IEnumerator setActiveObjinSecond(GameObject gameObject, float second)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(second);
        gameObject.SetActive(false);
    }
}
