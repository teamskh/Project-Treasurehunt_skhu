using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QRCodeCreator : MonoBehaviour {

    public RawImage qrcode = null;
    
	void Start () {
        StartCoroutine(CreateQRCode());
	}
	
    IEnumerator CreateQRCode()
    {
        WWW www = new WWW("http://chart.apis.google.com/chart?cht=qr&chs=500&chl=" + "hoho");
        yield return www;
        qrcode.texture = www.texture;


    }
}
