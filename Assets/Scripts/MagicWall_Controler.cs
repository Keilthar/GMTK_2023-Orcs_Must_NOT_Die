using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWall_Controler : MonoBehaviour
{
    float lifeTime = 5f;
    public bool isDeployed;
    float timer;

    public void Init()
    {
        isDeployed = true;
        timer = 0f;
        transform.GetComponent<CapsuleCollider>().enabled = true;
    }


    void Update()
    {
        // Rotate to closest defense when placing
        if (isDeployed == false)
        {
            Vector3 positionToLookAt = Vector3.up;
            float closestDistance = 999999f;
            foreach(GameObject defense in Defense_Manager.Singleton.L_Defenses)
            {
                float distance = Vector3.Distance(transform.position, defense.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    positionToLookAt = new Vector3(defense.transform.position.x, 3, defense.transform.position.z);
                }
            }
            transform.LookAt(positionToLookAt);
            transform.rotation *= Quaternion.Euler(0,0,90);
        }
        // Lifetime decreasing when placed
        else if (isDeployed == true)
        {
            timer += Time.deltaTime;
            if (timer >= lifeTime)
                Destroy(gameObject);
        }
    }
}
