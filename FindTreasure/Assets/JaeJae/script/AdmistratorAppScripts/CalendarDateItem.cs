using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CalendarDateItem : MonoBehaviour {
    public void OnDateItemClick(Text text)
    {
        string show;
        CalendarController._calendarInstance.OnDateItemClick(gameObject.GetComponentInChildren<Text>().text,out show);
        text.text = show;
    }
    public void OnDateItemClickE(Text text)
    {
        string show;
        CalendarController._calendarInstance.OnDateItemClickE(gameObject.GetComponentInChildren<Text>().text, out show);
        text.text = show;
    }


}
