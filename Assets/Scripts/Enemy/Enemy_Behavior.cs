using System.Collections;
using UnityEngine;

public class Enemy_Behavior : MonoBehaviour
{
    [Header("Dependencies")]
    public Enemy_Movement enemy_Movement;

    [Header("Behavior Settings")] [Tooltip("Time to wait before deleting.")]
    [SerializeField] float deletionDelay = 1;

    [Header("Health Bar Dependencies")]
    [SerializeField] GameObject sliderFill;
    [SerializeField] GameObject sliderBackgroundFill;

    float life = 1;
    [HideInInspector] public bool isAlive = true;

    public void DeleteEnemy()
    {
        Destroy(gameObject, deletionDelay);
    }

    public void hit(float damage) 
    {
        if (life - damage <= 0)
        {
            life = 0;
            setDead();
        }
        else 
        {
            life -= damage;
        }
        adjustHealthBar();
    }

    void adjustHealthBar() 
    {
        sliderFill.transform.localScale = new Vector3(life, 1, 1);
        sliderBackgroundFill.transform.localScale = new Vector3(1 - life, 1, 1);
    }

    void setDead() 
    {
        isAlive = false;
    }
}