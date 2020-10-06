using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class input : MonoBehaviour
{
    public void Start()
    {
        gameObject.name = transform.GetComponentInParent<Scroll>().title;
    }
}
