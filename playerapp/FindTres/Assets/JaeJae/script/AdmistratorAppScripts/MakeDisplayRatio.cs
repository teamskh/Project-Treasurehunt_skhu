using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MakeDisplayRatio : MonoBehaviour
{
    float width, height;
    [SerializeField]
    Text log;
    [SerializeField]
    GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        width = Camera.main.pixelWidth;
        height = Camera.main.pixelHeight;
        log.text = string.Format("Width : {0}\nHeight: {1}\nRatio: {2}", width, height, height / width);
        Rect Rec = panel.GetComponent<RectTransform>().rect;
        panel.GetComponent<RectTransform>().rect.Set(Rec.x, Rec.y, Rec.width, height - Rec.height);
        Canvas.ForceUpdateCanvases();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
