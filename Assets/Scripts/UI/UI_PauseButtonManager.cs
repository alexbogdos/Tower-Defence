using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PauseButtonManager : MonoBehaviour
{
    [Tooltip("Button unpaused/paused sprites.")]
    [SerializeField] Sprite[] sprites;
    [Tooltip("Button unpaused/paused colors.")]
    [SerializeField] Color[] colors;

    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
        image.color = colors[0];
    }

    public void changeSprite(bool pauseState) 
    {
        int i = 0;
        if (pauseState) 
        {
            i = 1;
        }
        image.sprite = sprites[i];
        image.color = colors[i];
    }   
}
