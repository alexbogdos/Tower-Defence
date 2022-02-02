using System.Collections;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    UI_Pause ui_Pause;

    [Header("Dependencies")]

    [Tooltip("The enemy prefab.")]
    [SerializeField] GameObject enemy;

    [Header("Spawner Settings")]

    [Tooltip("Number of enemies to be spawned.")]
    [SerializeField] int enemyPopulation = 3;

    [Tooltip("Enemy movement step.")]
    [SerializeField] float step;

    [Tooltip("If true all enemies spawned have the same step given above.")]
    [SerializeField] bool useGlobalStep;

    [Tooltip("The transform where enemies will be spawned at.")]
    [SerializeField] Transform spawnerTransform;

    [Tooltip("Time delay between spawns.")]
    [SerializeField] float delay = 1;


    // Start is called before the first frame update
    void Start()
    {
        ui_Pause = FindObjectOfType<UI_Pause>().GetComponent<UI_Pause>();

        GameObject[] EnemyList = new GameObject[enemyPopulation];

        SpawnEnemies(enemyPopulation, EnemyList);

        StartCoroutine(EnableEnemies(enemyPopulation, EnemyList, delay));
    }

    GameObject InstantiateEnemie(int index)
    {
        GameObject enemyInstance = Instantiate(enemy, spawnerTransform.position, Quaternion.Euler(0, 0, 0));
        if (useGlobalStep)
        {
            enemyInstance.GetComponent<Enemy_Movement>().step = step;
        }

        return enemyInstance;
    }

    void SpawnEnemies(int _population, GameObject[] _enemyList) 
    {
        for (int i = 0; i < _population; i++)
        {
            GameObject _enemyInstance = InstantiateEnemie(i);
            _enemyList[i] = _enemyInstance;

            _enemyList[i].SetActive(false);
        }
    }

    IEnumerator EnableEnemies(int _population, GameObject[] _enemyList, float delay)
    {
        for (int i = 0; i < _population; i++)
        {
            //yield return new WaitUntil(() => ui_Pause.GetPausedState() == false);
            yield return new WaitForSeconds(delay);

            _enemyList[i].SetActive(true);
            
        }
    }
}
