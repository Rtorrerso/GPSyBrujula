using UnityEngine;
using UnityEngine.UI;

public class WebcamTexture : MonoBehaviour
{
    public RawImage rawImage;

    void Start()
    {
        WebCamTexture webCam = new WebCamTexture();
        rawImage.texture = webCam;
        rawImage.material.mainTexture = webCam;
        webCam.Play();
    }
}
