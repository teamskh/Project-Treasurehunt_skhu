using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X : ARButtons {
    [SerializeField] GameObject O;
    public override void CheckAns()
    {
        Debug.Log("X call");
        transform.GetComponentInParent<Scroll>().setAnswer(bool.FalseString);
        GetComponent<Renderer>().material = selected;
        O.GetComponent<Renderer>().material = nonselected;
    }
}
