using UnityEngine;

public class O : ARButtons 
{
    [SerializeField] GameObject X;
    public override void CheckAns()
    {
        Debug.Log("O Call");
        transform.GetComponentInParent<Scroll>().setAnswer(bool.TrueString);
        GetComponent<Renderer>().material = selected;
        X.GetComponent<Renderer>().material = nonselected;
    }

}
