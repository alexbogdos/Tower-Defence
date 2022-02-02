using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Settings_panel : MonoBehaviour
{
    string valueToString(float value) 
    {
        string stringValue = "OFF";
        if (value == 1) 
        {
            stringValue = "ON";
        }
        return stringValue;
    }

    bool valueToBool(float value)
    {
        bool boolValue = false;
        if (value == 1)
        {
            boolValue = true;
        }
        return boolValue;
    }

    // Start Camera Settings
    [Header("Camera")]
    [SerializeField] PanZoom panZoom;
    [SerializeField] TMP_Text zoomBoundsText;

    public void ChangeCamZoomBounds(float value)
    {
        var v = Mathf.CeilToInt(value * 10);
        value = (float)v / 10;
        panZoom.zoomOutMin = value;
        zoomBoundsText.text = value.ToString() + "/1.4";
    }
    // End Camera Settings



    // Start FPS Count Settings
    [Header("Camera")]
    [SerializeField] GameObject FPSCount;
    [SerializeField] TMP_Text fpsCountText;

    public void ShowFpsCount(float value)
    {
        bool boolState = valueToBool(value);
        FPSCount.SetActive(boolState);
        
        string stringState = valueToString(value);
        fpsCountText.text = stringState;
    }
    // End FPS Count Settings



    // Start Post Processing Effects Settings
    [Header("Post Processing")]
    [SerializeField] Volume volume;
    [SerializeField] TMP_Text bloomText;
    [SerializeField] TMP_Text motionBlurText;
    [SerializeField] TMP_Text vignetteText;
    [SerializeField] TMP_Text chromaticAberrationText;

    public void ChangeBloom(float value)
    {
        volume.profile.TryGet<Bloom>(out var bloom);

        bool state = valueToBool(value);

        bloom.active = state;

        string stringState = valueToString(value);
        bloomText.text = stringState;
    }

    public void ChangeMotionBlur(float value)
    {
        volume.profile.TryGet<MotionBlur>(out var motionBlur);

        bool state = valueToBool(value);

        motionBlur.active = state;

        string stringState = valueToString(value);
        motionBlurText.text = stringState;
    }

    public void ChangeVignette(float value)
    {
        volume.profile.TryGet<Vignette>(out var vignette);

        bool state = valueToBool(value);

        vignette.active = state;

        string stringState = valueToString(value);
        vignetteText.text = stringState;
    }

    public void ChangeChromaticAberration(float value)
    {
        volume.profile.TryGet<ChromaticAberration>(out var chromaticAberration);

        bool state = valueToBool(value);

        chromaticAberration.active = state;

        string stringState = valueToString(value);
        chromaticAberrationText.text = stringState;
    }

    // End Post Processing Effects Settings




}
