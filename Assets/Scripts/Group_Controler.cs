using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group_Controler : MonoBehaviour
{
    float moving_Distance;

    void Awake()
    {
        moving_Distance = 0;
        transform.position = Path_Manager.Singleton.Path_GetPosition(moving_Distance);
    }

    void Update()
    {
        moving_Distance += Time.deltaTime * Enemy_Manager.Singleton.speed;
        Vector3 position_New = Path_Manager.Singleton.Path_GetPosition(moving_Distance);
        Vector3 direction_Facing = position_New - transform.position;
        transform.position = position_New;
        if (direction_Facing != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction_Facing, Vector3.up);
    }
}
