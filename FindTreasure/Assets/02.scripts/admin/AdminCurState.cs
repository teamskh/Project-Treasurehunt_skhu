using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminCurState 
{
    public string Competition { get; set; }
    public string Quiz { get; set; }

    private static AdminCurState instance;
    public static AdminCurState Instance { 
        get
        {
            if (instance == null) instance = new AdminCurState();
            return instance;
        }
    }
}
