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
    int targetID;

    void Awake()
    {
        L_Targets = new List<GameObject>();
        fire_Cooldown = 0;
        projectile_Prefab = Resources.Load<GameObject>("Defenses/Canon_Projectile");
        canon = transform.Find("weapon_cannon").Find("cannon").transform;
    }

    void Update()
    {
        if (Game_Manager.Singleton.isGameStarted == false)
        {
            L_Targets = new List<GameObject>();
            return;
        }
            
        // Refresh fire cooldown
        if (fire_Cooldown > 0)
            fire_Cooldown -= Time.deltaTime;

        // Check if enemies still alive and adjust target list
        bool isTargetListChanged = false;
        if (L_Targets.Count > 0)
        {
            for (int targetNum = L_Targets.Count-1; targetNum >= 0; targetNum--)
            {
                Enemy_Controler enemy = L_Targets[targetNum].GetComponent<Enemy_Controler>();
                if (enemy == null)
                {
                    L_Targets.RemoveAt(targetNum);
                    isTargetListChanged = true;
                }                    
                else if (enemy.Is_Alive() == false)
                {
                    L_Targets.RemoveAt(targetNum);
                    isTargetListChanged = true;
                }
            }
        }

        // Look at target and fire if cooldown up
        if (L_Targets.Count > 0)
        {
            if (isTargetListChanged == true)
                targetID = Random.Range(0, L_Targets.Count);

            canon.transform.LookAt(L_Targets[targetID].transform.position);
            canon.rotation *= Quaternion.Euler(90,0,0);
            if (fire_Cooldown <= 0)
            {
                fire_Cooldown = (float) 1/fire_Rate;
                Fire_OnFirstTarget(targetID);
            }
        }
    }

    void Fire_OnFirstTarget(int TargetID)
    {
        Enemy_Controler enemy = L_Targets[TargetID].GetComponent<Enemy_Controler>();
        GameObject projectile = Instantiate(projectile_Prefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile_Controler>().Init(L_Targets[TargetID].transform, canon.position, fire_Damage);
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
