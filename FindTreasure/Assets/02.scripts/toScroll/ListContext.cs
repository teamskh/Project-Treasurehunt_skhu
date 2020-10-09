using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListContext : MonoBehaviour
{
    [SerializeField]
    List<TextMesh> nums;
    
    public void setList(string[] list)
    {
        for(int i = 0; i < 4; i++)
        {
            nums[i].text = list[i];
        }
    }
}
