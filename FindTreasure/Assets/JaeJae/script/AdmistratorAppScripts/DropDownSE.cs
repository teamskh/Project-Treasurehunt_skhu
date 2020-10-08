using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using TTM.Classes;

public class DropDownSE : MonoBehaviour
{
    public string isAMPM;
    public string EndAMPM;
    public DateTime starttime;
    public DateTime endtime;

    public Dropdown startTAMPM;
    public Dropdown startH;
    public Dropdown startM;
    public Dropdown EndTAMPM;
    public Dropdown EndH;
    public Dropdown EndM;
    private List<string> AMPM = new List<string>() { "AM", "PM" };

    public GameObject Panel;
    private bool isOn;
    private bool reservStart;
    private bool reservEnd;

    public bool chon= false;
    public int cnt = 0;
    [SerializeField]
    Text sdate,edate;

    Competition compet = new Competition();
    CompetitionDictionary dic = new CompetitionDictionary();

    public void HandleInputData()
    {
        startTAMPM.options.Clear();
        startTAMPM.AddOptions(AMPM);
        EndTAMPM.options.Clear();
        EndTAMPM.AddOptions(AMPM);
    }
    public void DropDown_Change(int index)
    {
        isAMPM = AMPM[index];
    }
    public void DropDown_ChangeE(int index)
    {
        EndAMPM = AMPM[index];
    }

    void Start()
    {
        SetDropdownOptionsExample();
        HandleInputData();
        StartCoroutine(CountTime());
        string key = AdminCurState.Instance.Competition;
        dic.GetCompetitions();
        if (dic.TryGetValue(key, out compet))
        {
            if (compet.StartTime.Year==9999)
            {
                return;
            }
            else
            {
                sdate.text = compet.StartTime.Year + "-" + compet.StartTime.Month + "-" + compet.StartTime.Day.ToString();
                startM.value = compet.StartTime.Minute;
                if (compet.StartTime.Hour > 12)
                {
                    startTAMPM.value = 1;
                    startH.value = compet.StartTime.Hour - 12;
                }
                else
                {
                    startTAMPM.value = 0;
                    startH.value = compet.StartTime.Hour;
                }

            }
            if (compet.EndTime.Year == 9999)
            {
                return;
            }
            else
            {
                edate.text= compet.EndTime.Year +"-"+ compet.EndTime.Month + "-" + compet.EndTime.Day.ToString();
                EndM.value = compet.EndTime.Minute;
                if (compet.EndTime.Hour > 12)
                {
                    EndTAMPM.value = 1;
                    EndH.value = compet.EndTime.Hour - 12;
                }
                else
                {
                    EndTAMPM.value = 0;
                    EndH.value = compet.EndTime.Hour;
                }
            }
        }
    }
    
    private void SetDropdownOptionsExample()
    {
        startH.options.Clear();
        EndH.options.Clear();
        for (int j = 0; j < 12; j++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = j.ToString();
            if (j == 0)
            {
                option.text = (j + 12).ToString();
            }
            startH.options.Add(option);
           EndH.options.Add(option);
        }
        startM.options.Clear();
        EndM.options.Clear();
        for (int i = 0; i < 60; i++)
        {
            Dropdown.OptionData option2 = new Dropdown.OptionData();
            option2.text = i.ToString();
            startM.options.Add(option2);
            EndM.options.Add(option2);
        }
    }

    public void SelectButton()
    {
        reservStart = true;
        if (startTAMPM.value == 0)
        {
            starttime = Convert.ToDateTime(CalendarController._SDateString + " " + startH.value.ToString("D2") + ":" + startM.value.ToString("D2"));
        }
        else if (startTAMPM.value == 1)
        {
            starttime = Convert.ToDateTime(CalendarController._SDateString + " " + (startH.value + 12).ToString("D2") + ":" + startM.value.ToString("D2"));
        }
        compet.StartTime = starttime;

        Param param = new Param();
        param.CompetStart(starttime)
             .CompetUpdate();
    }
    public void SelectButtonStartnow()
    {
        isOn = true;
        reservStart = false;
        starttime = DateTime.Now;

        compet.StartTime = starttime;
        sdate.text = starttime.Year + "-" + starttime.Month + "-" + starttime.Day.ToString();
        startM.value = compet.StartTime.Minute;
        if (compet.StartTime.Hour > 12)
        {
            startTAMPM.value = 1;
            startH.value = compet.StartTime.Hour - 12;
        }
        else
        {
            startTAMPM.value = 0;
            startH.value = compet.StartTime.Hour;
        }
        Param param = new Param();
        param.CompetStart(starttime)
             .CompetUpdate();
    }
    public void SelectButtonE()
    {
        reservEnd = true;
        
        if (EndTAMPM.value == 0)
        {
            endtime = Convert.ToDateTime(CalendarController._EDateString+" "+EndH.value.ToString("D2") + ":" + EndM.value.ToString("D2"));
           
        }
        else if (EndTAMPM.value == 1)
        {
            endtime = Convert.ToDateTime(CalendarController._EDateString +" "+(EndH.value +12).ToString("D2") + ":" + EndM.value.ToString("D2"));
            
        }

        compet.EndTime = endtime;

        Param param = new Param();
        param.CompetEnd(endtime)
             .CompetUpdate();

    }
    public void SelectButtonEndnow()
    {
        isOn = false;
        reservEnd = false;
        endtime = DateTime.Now;
        edate.text = endtime.Year + "-" + endtime.Month + "-" + endtime.Day.ToString();
        compet.EndTime = endtime;
        EndM.value = compet.EndTime.Minute;
        if (compet.EndTime.Hour > 12)
        {
            EndTAMPM.value = 1;
            EndH.value = compet.EndTime.Hour - 12;
        }
        else
        {
            EndTAMPM.value = 0;
            EndH.value = compet.EndTime.Hour;
        }
        Param param = new Param();
        param.CompetEnd(endtime)
             .CompetUpdate();
    }

    private void FixedUpdate()
    {
        
        Panel.SetActive(!isOn);
        
        if (reservStart)
        {
            int result = DateTime.Compare(DateTime.Now, starttime);

            if (result < 0)
            {
            }
            else
            {
                isOn = true;
                reservStart = false;
                //gamean 열리고 닫히고
            }
        }
        if (isOn)
        {
            if (reservEnd)
            {
                int result = DateTime.Compare(DateTime.Now, endtime);
                Debug.Log(result);
                if (result >= 0)
                {
                    isOn = false;
                    reservEnd = false;
                    // 종료
                }
                else
                {
                    
                }
            }
        }
    }
    IEnumerator CountTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
        }
    }
}
