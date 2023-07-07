using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Controller : MonoBehaviour
{
    List<GameObject> L_Targets;
    public int fire_Damage = 25;
    public int fire_Rate = 5;
    float fire_Cooldown;

    void Awake()
    {
        L_Targets = new List<GameObject>();
        fire_Cooldown = 0;
    }

    void Update()
    {
        if (fire_Cooldown > 0)
            fire_Cooldown -= Time.deltaTime;

        if (L_Targets.Count > 0 && fire_Cooldown <= 0)
        {
            fire_Cooldown = (float) 1/fire_Rate;
            Fire_OnFirstTarget();
        }
    }

    void Fire_OnFirstTarget()
    {
        while (L_Targets.Count > 0)
        {
            if (L_Targets[0] != null)
            {
                Enemy_Controler enemy = L_Targets[0].GetComponent<Enemy_Controler>();
                if (enemy.Is_Alive() == true)
                    enemy.Set_Damage(fire_Damage);

                if (enemy.Is_Alive() == false)
                    L_Targets.RemoveAt(0);

                break;
            }
            else
                L_Targets.RemoveAt(0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && L_Targets.Contains(other.gameObject) == false)
            L_Targets.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy" && L_Targets.Contains(other.gameObject) == true)
            L_Targets.Remove(other.gameObject);
    }
}
