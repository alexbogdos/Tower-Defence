using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class Tower_TypeSelector : MonoBehaviour
{
    [Space(10)] [Tooltip("Buttons used for changing the -to be build- tower type.")]
    [SerializeField] Button[] buttons;

    [HideInInspector] public int index = 0;

    ColorBlock Colors;

    void Awake()
    {
        Colors = buttons[index].colors;
        chancgeButtonColor(index);
    }

    public void changeIndexTo(int _index) 
    {
        index = _index;
        chancgeButtonColor(_index);
    }
    void chancgeButtonColor(int _index) 
    {
        for (int i = 0; i < buttons.Length; i++) 
        {
            var colors = buttons[i].colors;

            if (i == _index)
            {
                colors.normalColor = Colors.selectedColor;
                buttons[i].colors = colors;
            }
            else 
            {
                colors.normalColor = Colors.normalColor;
                buttons[i].colors = colors;
            }
        }
    }
}
