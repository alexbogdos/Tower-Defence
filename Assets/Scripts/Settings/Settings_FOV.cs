using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Settings_FOV : MonoBehaviour
{
    [SerializeField] TMP_Text displayText;
    [SerializeField] Slider slider;
    [SerializeField] Camera mainCamera;

    float fieldOfView;

    // Start is called before the first frame update
    void Start()
    {
        fieldOfView = mainCamera.orthographicSize;
        slider.value = fieldOfView / 10;
        displayText.text = "FOV: " + fieldOfView.ToString();
    }

    public void ChangeFieldOfViewValues() {
        fieldOfView = slider.value * 10;
        mainCamera.orthographicSize = fieldOfView;
        displayText.text = "FOV: " + fieldOfView.ToString();
    }
}
