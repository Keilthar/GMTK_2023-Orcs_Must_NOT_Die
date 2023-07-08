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
        if (isDeployed == true)
        {
            timer += Time.deltaTime;
            if (timer >= lifeTime)
                Destroy(gameObject);
        }
    }
}
