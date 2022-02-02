using UnityEngine;

public class ScreenToCanvas : MonoBehaviour
{
    [SerializeField] Camera m_camera;
    [SerializeField] Canvas m_canvas;
    [SerializeField] RectTransform m_object;
    RectTransform m_parent;

    [HideInInspector] public Vector2 position;

    void Awake()
    {
        m_parent = m_canvas.GetComponent<RectTransform>();
    }

    public void MoveObject()
    {
        //position = Input.mousePosition;

        Vector2 anchoredPos;

        var pos = RectTransformUtility.WorldToScreenPoint(m_camera, position);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_parent, pos, null, out anchoredPos);
        m_object.anchoredPosition = anchoredPos;
    }
}
