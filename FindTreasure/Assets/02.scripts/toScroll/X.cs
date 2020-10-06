using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X : ARButtons {
    [SerializeField] GameObject O;
    public void Check()
    {
        Debug.Log("X call");
        transform.GetComponentInParent<Scroll>().setAnswer(bool.FalseString);
        GetComponent<MeshRenderer>().material.color = Color.cyan;
        O.GetComponent<MeshRenderer>().material.color = Color.white;
    }
    public void Start()
    {
        gameObject.name = transform.GetComponentInParent<Scroll>().title + '/' + bool.FalseString;
    }
}
