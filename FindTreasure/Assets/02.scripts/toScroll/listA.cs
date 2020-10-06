using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listA : MonoBehaviour
{
    public int num;
    public void Start()
    {
        gameObject.name = transform.GetComponentInParent<Scroll>().title + '/' +num;
    }

}
