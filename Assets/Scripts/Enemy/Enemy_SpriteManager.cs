using UnityEngine;

public class Enemy_SpriteManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] Enemy_Behavior enemy_Behavior;

    [Space(10)] [Tooltip("Enemy sprites to use.")]
    [SerializeField] Sprite[] SpriteList;

    SpriteRenderer spriteRenderer;
    [Space(10)] [Tooltip("All sprite renderers in the enemy object.")]
    [SerializeField] SpriteRenderer[] spriteRenderers;

    string previousInfo = "";
    Enemy_Movement enemy_Movement;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemy_Movement = enemy_Behavior.enemy_Movement;
    }

    void Update()
    {
        //Adjust sorting layer
        int sortValue = (int)(transform.position.y * -10.0f);
        spriteRenderer.sortingOrder = sortValue;
        foreach (var renderer in spriteRenderers)
        {
            renderer.sortingOrder = sortValue + 1;
        }

        //Change enemy sprite acording to direction
        string currentDirection = enemy_Movement.GetCurrentWayPointInfo().Direction;
        if (previousInfo != currentDirection)
        {
            changeSprite(currentDirection);

            previousInfo = enemy_Movement.GetCurrentWayPointInfo().Direction;
        }
    }

    void changeSprite(string direction)
    {
        if (direction == "T")
        {
            spriteRenderer.sprite = SpriteList[1];
        }
        else if (direction == "B")
        {
            spriteRenderer.sprite = SpriteList[2];
        }
        else
        {
            spriteRenderer.sprite = SpriteList[0];
        }
    }
}
