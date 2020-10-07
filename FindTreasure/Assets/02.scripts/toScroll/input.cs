using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class input : ARButtons
{
    private inputUI Panel;
    protected override void Start()
    {
        Panel = GameObject.Find("Canvas").transform.Find("WordAnw")?.GetComponent<inputUI>();
    }
    public override void CheckAns()
    {
        if (Panel != null)
        {
            var scroll = transform.GetComponentInParent<Scroll>();
            Panel.SetActive(scroll.context, scroll.setAnswer);
        }
    }
}
