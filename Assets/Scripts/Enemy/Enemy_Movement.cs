using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    UI_Pause ui_Pause;

    [Header("Movement Settings")]
    public float step;

    [Header("WayPoint Dependencies Settings")] 
    [Tooltip("WayPoint manager tag.")]
    [SerializeField] string wayPointTag = "WayPoints List";

    Enemy_Behavior enemy_Behavior;

    WayPoints wayPoints;
    public int nextWayPointIndex = 0;
    Transform nextWayPoint;
    bool finished = false;

    void Awake()
    {
        ui_Pause = FindObjectOfType<UI_Pause>().GetComponent<UI_Pause>();

        enemy_Behavior = GetComponent<Enemy_Behavior>();
    }

    // Get Way points
    void Start()
    {
        wayPoints = GameObject.FindGameObjectWithTag(wayPointTag).GetComponent<WayPoints>();
        ChangeWayPoint();
    }

    // Move to next way point
    void FixedUpdate()
    {
        if (enemy_Behavior.isAlive && ui_Pause.GetPausedState() == false)
        {
            if (IsOnPosition(nextWayPoint.position))
            {
                if (IsNotLastPosition())
                {
                    SetNextPositionAsTarget();
                }
                else if (!finished)
                {
                    enemy_Behavior.DeleteEnemy();
                    finished = true;
                }
            }
            else
            {
                MoveToWayPoint();
            }
        }
    }

    void MoveToWayPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, nextWayPoint.position, step);
    }

    bool IsOnPosition(Vector3 position)
    {
        return transform.position == position;
    }

    bool IsNotLastPosition()
    {
        return transform.position != wayPoints.lastPosition.position;
    }

    void SetNextPositionAsTarget()
    {
        nextWayPointIndex++;
        ChangeWayPoint();
    }

    void ChangeWayPoint()
    {
        nextWayPoint = wayPoints.wayPointsList[nextWayPointIndex];
    }

    public WayPoint_Info GetCurrentWayPointInfo() 
    {
        return wayPoints.wayPoints_InfoList[nextWayPointIndex];
    }
}
