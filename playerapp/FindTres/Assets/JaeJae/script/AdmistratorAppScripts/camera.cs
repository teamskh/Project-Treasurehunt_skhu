using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class camera : MonoBehaviour
{
    //UI
    public GameObject OtherContents;
    public GameObject TakePicture_b;
    public RawImage rawImage;
    public AspectRatioFitter fit;
    // 카메라 입력을 위한 WebCamTexture
    protected WebCamTexture textureWebCam = null;

    

    // Start is called before the first frame update
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        int selectedCameraIndx = -1;

        if (devices.Length < 0)
        {
            Debug.Log("cam Doesn't exist!");
        }
        else
        {
            selectedCameraIndx = 0;
            Debug.Log("cam Exist!");
        }
        /*
        for(int i = 0; i < devices.Length; i++)
        {

            Debug.LogFormat("{0}번 카메라", i);
            if (devices[i].isFrontFacing == false)
            {
                selectedCameraIndx = i;
                break;
            }
        }*/

        if (selectedCameraIndx >= 0)
        {
            // 선택된 카메라에 대한 새로운 WebCamTexture를 생성
            Debug.LogFormat("{0} : device name", devices[selectedCameraIndx].name);
            textureWebCam = new WebCamTexture(devices[selectedCameraIndx].name);

            // 원하는 FPS를 설정
            if (textureWebCam != null)
            {
                textureWebCam.requestedFPS = 60;
                Debug.Log("Set FPS!");
                rawImage.texture = textureWebCam;
                rawImage.material.mainTexture = textureWebCam;
            }
        }

    }

    private void OnDestroy()
    {
        if (textureWebCam != null)
        {
            textureWebCam.Stop();
            WebCamTexture.Destroy(textureWebCam);
        }
    }

    //camera 활성화 버튼 
    public void OnPlayButtonClick()
    {
        Debug.Log("Playbutton Clicked");
        if (textureWebCam != null)
        {
            Debug.Log("Cam is Active");
            OtherContents.SetActive(false);
            TakePicture_b.SetActive(true);
            textureWebCam.Play();

            float ratio = (float)textureWebCam.width / (float)textureWebCam.height;
            fit.aspectRatio = ratio;

            float scaleY = textureWebCam.videoVerticallyMirrored ? -1f : 1f;
            rawImage.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

            int orient = -textureWebCam.videoRotationAngle;
            rawImage.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
        }
    }

    //TakePicture_b의 Onclick Method
    public void OnCapture()
    {
        Texture2D texture = new Texture2D(rawImage.texture.width, rawImage.texture.height, TextureFormat.ARGB32, false);

        texture.SetPixels(textureWebCam.GetPixels());
        texture.Apply();

        rawImage.texture = texture;

        OnDestroy();
        TakePicture_b.SetActive(false);
        OtherContents.SetActive(true);
    }
}
