using System;
using UnityEngine;

public class AvailableAR : MonoBehaviour
{
    [SerializeField]
    GameObject ARSessionOrigin;

    [SerializeField]
    GameObject ARSession;

    public static event Action MakeActive;

    public void Start()
    {
        MakeActive = () => Active(true);
    }

    void Active(bool a)
    {
        ARSessionOrigin.SetActive(a);
        ARSession.SetActive(a);
    }

    public static void MakeAct()
    {
        MakeActive();
    }
}
