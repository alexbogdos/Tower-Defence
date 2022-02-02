using UnityEngine;
using TMPro;

public class UI_FPSCounter : MonoBehaviour
{
    [Tooltip("Reset the count variable after $avrgFpsCount.")]
    [SerializeField] int avrgFpsCount = 100;

    TMP_Text Text;
    float sum = 0;
    float count = 0;
    float avrgSum = 0;

    void Awake()
    {
        Text = GetComponent<TMP_Text>();        
    }

    void Update()
    {
        float fps = (1 / Time.unscaledDeltaTime);

        if (count >= avrgFpsCount) 
        {
            sum = 0;
            count = 1;
        }

        count++;
        sum += fps;
        avrgSum = sum / count;

        int v = Mathf.CeilToInt(avrgSum * 100);
        float s = (float)v / 100;

        Text.text = s.ToString();
    }
}
