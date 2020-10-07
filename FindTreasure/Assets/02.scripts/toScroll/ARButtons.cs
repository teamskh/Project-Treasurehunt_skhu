using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ARButtons : MonoBehaviour
{
    protected Material selected, nonselected;
    public string answer;
    protected virtual void Start()
    {
        selected = transform.GetComponentInParent<Scroll>().seleted;
        nonselected = transform.GetComponentInParent<Scroll>().nonseleted;
    }
    public virtual void CheckAns() { }

}
