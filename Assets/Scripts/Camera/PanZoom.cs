using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoom : MonoBehaviour
{
    UI_Pause ui_Pause;
    Camera cam;
    
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    public Vector2 posBounds;
    public float zoomStep = 0.01f;

    void Awake()
    {
        ui_Pause = FindObjectOfType<UI_Pause>().GetComponent<UI_Pause>();

        cam = GetComponent<Camera>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (ui_Pause.GetPausedState() == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = cam.ScreenToWorldPoint(Input.mousePosition);
            }
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;

                zoom(difference * zoomStep);
            }
            else if (Input.GetMouseButton(0))
            {
                pan();
            }

            zoom(Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    void zoom(float increment)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }

    void pan() 
    {
        Vector3 direction = touchStart - cam.ScreenToWorldPoint(Input.mousePosition);
        cam.transform.position += direction;

        var xClamp = Mathf.Clamp(cam.transform.position.x, -posBounds.x, posBounds.x);
        var yClamp = Mathf.Clamp(cam.transform.position.y, -posBounds.y, posBounds.y);

        cam.transform.position = new Vector3(xClamp, yClamp, cam.transform.position.z);
    }
}
