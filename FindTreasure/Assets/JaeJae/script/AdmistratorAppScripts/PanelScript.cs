using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelScript : MonoBehaviour
{
    [SerializeField]
    GameObject Panel;

    private void Start()
    {
        Panel.SetActive(false);
    }

    private void Update()
    {
        Forupdate();
    }

    void Forupdate()
    {
        if(GameObject.Find("Panel_pw")|| GameObject.Find("Panel_comp") || GameObject.Find("Panel_mode"))
        {
            Panel.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                string obj_name=EventSystem.current.currentSelectedGameObject.name;
                Debug.Log(obj_name);
                if (obj_name == "Panel_T")
                {
                    Panel.SetActive(false);
                }
            }
        }
    }
}
