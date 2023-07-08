using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Controller : MonoBehaviour
{
    Transform canon;
    GameObject projectile_Prefab;
    List<GameObject> L_Targets;
    public int fire_Damage = 25;
    public int fire_Rate = 5;
    float fire_Cooldown;

    void Awake()
    {
        L_Targets = new List<GameObject>();
        fire_Cooldown = 0;
        projectile_Prefab = Resources.Load<GameObject>("Defenses/Canon_Projectile");
        canon = transform.Find("weapon_cannon").Find("cannon").transform;
    }

    void Update()
    {
        // Refresh fire cooldown
        if (fire_Cooldown > 0)
            fire_Cooldown -= Time.deltaTime;

        // Check if enemies still alive and adjust target list
        if (L_Targets.Count > 0)
        {
            for (int targetNum = L_Targets.Count-1; targetNum >= 0; targetNum--)
            {
                Enemy_Controler enemy = L_Targets[targetNum].GetComponent<Enemy_Controler>();
                if (enemy.Is_Alive() == false)
                    L_Targets.RemoveAt(targetNum);
            }
        }

        // Look at target and fire if cooldown up
        if (L_Targets.Count > 0)
        {
            canon.transform.LookAt(L_Targets[0].transform.position);
            canon.rotation *= Quaternion.Euler(90,0,0);
            if (fire_Cooldown <= 0)
            {
                fire_Cooldown = (float) 1/fire_Rate;
                Fire_OnFirstTarget();
            }
        }
    }

    void Fire_OnFirstTarget()
    {
        Enemy_Controler enemy = L_Targets[0].GetComponent<Enemy_Controler>();
        GameObject projectile = Instantiate(projectile_Prefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile_Controler>().Init(L_Targets[0].transform, canon.position, fire_Damage);
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
