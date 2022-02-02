using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tower_DeleteButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Dependencies")]
    [SerializeField] GameObject panel;
    [SerializeField] Animator animator;
    [SerializeField] AnimationClip exitAnimation;

    [Space(6)]
    [Tooltip("Tile position script. Is used to set the 'interactable' tile in the former tower position.")]
    [SerializeField] Tile_position tile_Position;

    [Header("Settings")]
    [Tooltip("Time to wait before destroying the tower.")]
    [SerializeField] float delay;
    
    [Tooltip("Delete Button height (y offset).")]
    [SerializeField] float yOffset;

    [HideInInspector] public bool pressed;
    [HideInInspector] public bool hovering;

    Button button;
    GameObject tower;
    Vector3 position;
    Vector3 previousPos;

    void Awake()
    {
        button = panel.GetComponentInChildren<Button>();
        panel.SetActive(false);
    }

    void Update()
    {
        if (panel.activeSelf)
        {
            moveButton();
        }

    }

    public void EnableDeleteButton(GameObject _parent, Vector3 _pos)
    {
        tower = _parent;
        position = _pos;

        moveButton();
        panel.SetActive(true);
        animator.SetTrigger("enter");

        pressed = false;
        hovering = false;
    }

    public Vector3 GetPosition()
    {
        return position;
    }
    public bool GetButtonState()
    {
        return panel.activeSelf;
    }

    public void DisableDeleteButton()
    {
        pressed = true;
        animator.SetTrigger("exit");

        StartCoroutine(_disable(exitAnimation.length));
    }

    IEnumerator _disable(float delay)
    {
        yield return new WaitForSeconds(delay);

        panel.SetActive(false);
    }

    public void DeleteTower()
    {
        GameObject.Destroy(tower, delay);

        tile_Position.setInteractableTile(position);
        DisableDeleteButton();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        hovering = false;
    }

    void moveButton()
    {
        Vector3 offsetedPos = new Vector3(position.x, position.y + yOffset, position.z);
        panel.transform.position = Camera.main.WorldToScreenPoint(offsetedPos);
    }
}
