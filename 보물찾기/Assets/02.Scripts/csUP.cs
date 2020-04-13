using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csUP : MonoBehaviour
{
    Vector3 target = new Vector3(396, 900f, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,target, 10f);
    }
}
