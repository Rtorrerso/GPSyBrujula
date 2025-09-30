using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using TMPro;


public class GPSManager : MonoBehaviour
{
    public TMP_Text gpsText; // asigna desde el Inspector
    private Vector2 gpsPosition;
    private bool serviceRunning = false;

    IEnumerator Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        // 1) Solicitar permisos en tiempo de ejecución
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.FineLocation))
        {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.FineLocation);
            // También puedes pedir coarse si te sirve:
            // UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CoarseLocation);

            // Espera unos frames a que el usuario responda
            int frames = 0;
            while (frames < 60 && 
                   !UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.FineLocation))
            {
                yield return null;
                frames++;
            }
        }
#endif

        // 2) Verificar si el usuario tiene habilitada la ubicación (y permiso concedido)
        if (!Input.location.isEnabledByUser)
        {
            if (gpsText) gpsText.text = "GPS/Permiso no activado por el usuario.";
            yield break;
        }

        // 3) Iniciar el servicio de ubicación (puedes ajustar precisión/distancia)
        // desiredAccuracyInMeters, updateDistanceInMeters
        Input.location.Start(5f, 1f);

        // 4) Esperar inicialización con timeout
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1f);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            if (gpsText) gpsText.text = "Tiempo de espera agotado inicializando GPS.";
            yield break;
        }

        // 5) Validar resultado
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            if (gpsText) gpsText.text = "No se pudo obtener la ubicación (Failed).";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Running)
        {
            serviceRunning = true;
            var data = Input.location.lastData;
            gpsPosition = new Vector2(data.latitude, data.longitude);
            if (gpsText) gpsText.text = $"Lat: {gpsPosition.x:F6} Lon: {gpsPosition.y:F6}";
        }
        else
        {
            if (gpsText) gpsText.text = "Servicio de ubicación no está en Running.";
            yield break;
        }
    }

    void Update()
    {
        if (!serviceRunning) return;

        // Solo leer si el servicio sigue activo
        if (Input.location.status == LocationServiceStatus.Running)
        {
            var data = Input.location.lastData;
            gpsPosition = new Vector2(data.latitude, data.longitude);
            // (Opcional) refrescar texto cada cierto intervalo si cambia
        }
        else
        {
            serviceRunning = false;
            if (gpsText) gpsText.text = "El servicio de ubicación se detuvo.";
        }
    }

    void OnDisable()
    {
        if (serviceRunning)
        {
            Input.location.Stop();
            serviceRunning = false;
        }
    }
}



