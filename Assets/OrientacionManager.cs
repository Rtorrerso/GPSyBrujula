using UnityEngine;
using UnityEngine.UI;

public class OrientacionManager : MonoBehaviour
{
    public Text orientacionText;
    public float anguloInclinacion;
    public float direccionBrújula;

    void Start()
    {
        Input.compass.enabled = true;
        Input.gyro.enabled = true;
    }

    void Update()
    {
        direccionBrújula = Input.compass.trueHeading;
        anguloInclinacion = Input.gyro.attitude.eulerAngles.x;

        orientacionText.text = $"Brújula: {direccionBrújula:F1}° | Inclinación: {anguloInclinacion:F1}°";
    }
}

