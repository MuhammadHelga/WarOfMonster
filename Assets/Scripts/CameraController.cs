using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float normalSize = 15f;
    public float playingSize = 5f;
    public Vector3 startPos = new Vector3(-12f,-8f,-10f);

    private void Start()
    {
        Camera.main.orthographicSize = playingSize;
        transform.position = startPos;
       
    }
    public float cameraSpeed = 15f;
    public float edgeThreshold = 40f; // Ekranın kenarındaki fare hareketi için eşik değeri
    void Update()
    {
        Vector3 pos = Camera.main.transform.position;

        if (Input.mousePosition.x > Screen.width - edgeThreshold)
            pos.x += cameraSpeed * Time.deltaTime;
        else if (Input.mousePosition.x < edgeThreshold)
            pos.x -= cameraSpeed * Time.deltaTime;

        Camera.main.transform.position = pos;
    }

} 

