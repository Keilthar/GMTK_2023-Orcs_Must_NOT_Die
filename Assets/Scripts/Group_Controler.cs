using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group_Controler : MonoBehaviour
{
    float moving_Distance;
    float speedBoost_Duration = 3f;
    float speedBoost_Timer;
    float speedBoost_Force = 2f;

    void Awake()
    {
        moving_Distance = 0;
        transform.position = Path_Manager.Singleton.Path_GetPosition(moving_Distance);
    }

    public void Set_SpeedBoost()
    {
        speedBoost_Timer = speedBoost_Duration;
    }

    void Update()
    {
        float speed = Enemy_Manager.Singleton.speed;
        if (speedBoost_Timer > 0)
        {
            speed *= speedBoost_Force;
            speedBoost_Timer -= Time.deltaTime;
        }

        moving_Distance += Time.deltaTime * speed;
        Vector3 position_New = Path_Manager.Singleton.Path_GetPosition(moving_Distance);
        Vector3 direction_Facing = position_New - transform.position;
        transform.position = position_New;
        if (direction_Facing != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction_Facing, Vector3.up);
    }
}
