using UnityEngine;
using UnityEngine.UI;

public class Tower_SpriteUsedShow : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;

    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void setSprite(int _index) 
    {
        image.sprite = sprites[_index];
    }
}
