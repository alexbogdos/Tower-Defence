using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    [HideInInspector] public List<Transform> wayPointsList;
    [HideInInspector] public List<WayPoint_Info> wayPoints_InfoList;
    [HideInInspector] public Transform lastPosition;

    int lastIndex;

    void Awake()
    {
        Transform[] tempTransformList = GetComponentsInChildren<Transform>();
        foreach (var item in tempTransformList)
        {
            if (item.CompareTag("WayPoint"))
            {
                wayPointsList.Add(item);
            }
        }

        // Last index & position in list
        lastIndex = wayPointsList.Count - 1;
        lastPosition = wayPointsList[lastIndex];

        WayPoint_Info[] tempInfoList = GetComponentsInChildren<WayPoint_Info>();
        foreach (var item in tempInfoList)
        {
            if (item.CompareTag("WayPoint"))
            {
                wayPoints_InfoList.Add(item);
            }
        }
    }
}
