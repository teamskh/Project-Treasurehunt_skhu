using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listA : ARButtons
{
    public List<GameObject> lists;

    protected override void Start()
    {
        base.Start();
        lists = new List<GameObject>();
        foreach(var item in transform.parent.GetComponentsInChildren<listA>())
        {
            if (item.gameObject != gameObject)
                lists.Add(item.gameObject);
        }
    }

    public override void CheckAns()
    {
        Debug.Log($"{answer} Call");
        transform.GetComponentInParent<Scroll>().setAnswer(answer);
        setSelection();
    }

    private void setSelection()
    {
        foreach(var item in lists)
        {
            item.GetComponent<Renderer>().material = nonselected;
        }
        GetComponent<Renderer>().material = selected;
    }
}
