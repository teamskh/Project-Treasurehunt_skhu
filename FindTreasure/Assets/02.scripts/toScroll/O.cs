
using UnityEngine;
using UnityEngine.EventSystems;

public class O : ARButtons 
{
    [SerializeField] GameObject X;
    public void Check() {
        Debug.Log("O Call");
        transform.GetComponentInParent<Scroll>().setAnswer(bool.TrueString);
        GetComponent<MeshRenderer>().material.color = Color.cyan;
        X.GetComponent<MeshRenderer>().material.color = Color.white;
    }
    public void Start()
    {
        gameObject.name = transform.GetComponentInParent<Scroll>().title + '/' + bool.TrueString;
    }
}
