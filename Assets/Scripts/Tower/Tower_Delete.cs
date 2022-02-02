using System.Collections;
using UnityEngine;

public class Tower_Delete : MonoBehaviour
{
    UI_Pause ui_Pause;

    [Header("Dependencies")] [Tooltip("Tower Object. Used to get the object's position.")]
    [SerializeField] GameObject parent;
    [Tooltip("Outline Object. Used to enable or disable the tower outline.")]
    [SerializeField] GameObject outline;
    Tower_DeleteButton Tower_DeleteButton;

    bool mouseUp = true;
    bool deselected = true;

    void Awake()
    {
        ui_Pause = FindObjectOfType<UI_Pause>().GetComponent<UI_Pause>();

        Tower_DeleteButton = FindObjectOfType<Tower_DeleteButton>();
    }

    void LateUpdate()
    {
        if (!deselected)
        {
            
            if (Tower_DeleteButton.GetButtonState() && Tower_DeleteButton.GetPosition() != outline.transform.position)
            {
                outline.SetActive(false);
                deselected = true;
            }
            else if (mouseUp && Input.GetMouseButtonDown(0) && !Tower_DeleteButton.hovering)
            {
                outline.SetActive(false);
                Tower_DeleteButton.DisableDeleteButton();
                deselected = true;
            }
        }
    }

    void OnMouseDown()
    {
        if (ui_Pause.GetPausedState() == false)
        {
            outline.SetActive(true);

            mouseUp = false;
            deselected = false;

            Tower_DeleteButton.EnableDeleteButton(parent, outline.transform.position);
        }
        
        
        //StartCoroutine(Select(0f));
    }

    void OnMouseUp()
    {
        mouseUp = true;
    }

   /* IEnumerator Select(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (ui_Pause.GetPausedState() == false)
        {
            outline.SetActive(true);
        }
        mouseUp = false;
        deselected = false;

        Tower_DeleteButton.EnableDeleteButton(parent, outline.transform.position);

    }*/
}
