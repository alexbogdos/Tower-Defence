using UnityEngine;

public class Tower_Behavior : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] SpriteRenderer spriteRenderer;
    
    [Space(10)] [Tooltip("The available sprite for the tower.")]
    [SerializeField] Sprite[] sprites;

    Tower_TypeSelector typeSelector;
    string enemyTag = "Enemy";

    void Awake()
    {
        typeSelector = FindObjectOfType<Tower_TypeSelector>().GetComponent<Tower_TypeSelector>();

        int index = typeSelector.index;
        spriteRenderer.sprite = sprites[index];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag(enemyTag))
        {
            Enemy_Behavior enemy_Behavior = collision.GetComponent<Enemy_Behavior>();
            float _damage = Random.Range(0.1f, 0.3f);
            enemy_Behavior.hit(_damage);
        }
    }
}
