using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Controller : MonoBehaviour
{
    public AudioSource audioFire;
    public AudioClip fireClip;
    Transform canon;
    GameObject projectile_Prefab;
    Transform target;
    public int fire_Damage = 25;
    public int fire_Rate = 5;
    float fire_Cooldown;
    float fire_Range = 10f;

    void Awake()
    {
        fire_Cooldown = 0;
        projectile_Prefab = Resources.Load<GameObject>("Defenses/Canon_Projectile");
        canon = transform.Find("weapon_cannon").Find("cannon").transform;
    }

    void Update()
    {
        if (Game_Manager.Singleton.isGameStarted == false)
            return;
            
        // Refresh fire cooldown
        if (fire_Cooldown > 0)
            fire_Cooldown -= Time.deltaTime;

        // Check if enemies still alive and adjust target list
        if (target == null)
            Get_Target();

        // Look at target and fire if cooldown up
        if (target != null)
        {
            canon.transform.LookAt(target.transform.position);
            canon.rotation *= Quaternion.Euler(90,0,0);
            if (fire_Cooldown <= 0)
            {
                fire_Cooldown = (float) 1/fire_Rate;
                Fire_OnFirstTarget();
                Get_Target();
            }
        }
    }

    void Get_Target()
    {
        List<Transform> L_Orcs = new List<Transform>();
        foreach (Transform orc in Enemy_Manager.Singleton.L_Orcs)
        {
            if (orc != null)
            {
                float distance = Vector3.Distance(orc.position, transform.position);
                if (distance <= fire_Range)
                {
                    if (orc.GetComponent<Enemy_Controler>().Is_Alive() == true)
                        L_Orcs.Add(orc);
                }
            }   
        }

        if (L_Orcs.Count > 0)
        {
            int targetID = Random.Range(0, L_Orcs.Count);
            target = L_Orcs[targetID];
        }
        else target = null;

    }

    void Fire_OnFirstTarget()
    {
        audioFire.PlayOneShot(fireClip);
        Enemy_Controler enemy = target.GetComponent<Enemy_Controler>();
        GameObject projectile = Instantiate(projectile_Prefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile_Controler>().Init(target.transform, canon.position, fire_Damage);
    }
}
