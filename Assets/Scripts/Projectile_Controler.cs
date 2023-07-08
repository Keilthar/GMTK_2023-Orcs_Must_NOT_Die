using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Controler : MonoBehaviour
{
    int damage;
    public float speed = 20;
    Vector3 position_Initial;
    Vector3 position_Final;
    float travel_Time;
    float timer;

    public void Init(Transform Target, Vector3 PositionInitial, int Damage)
    {
        position_Initial = PositionInitial;
        position_Final = Target.position;
        damage = Damage;
        transform.position = position_Initial;
        travel_Time = Vector3.Distance(position_Initial, Target.position) / speed;
        timer = 0;
    }

    void Update()
    {
        if (position_Final != null)
        {
            transform.position = Vector3.Lerp(position_Initial, position_Final, timer / travel_Time);
            if (transform.position == position_Final)
                Destroy(gameObject);
            timer += Time.deltaTime;
        }
            
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MagicWall" || other.tag == "Ground")
            Destroy(gameObject);

        if (other.tag == "Enemy")
        {
            Enemy_Controler enemy = other.gameObject.GetComponent<Enemy_Controler>();
            enemy.Set_Damage(damage);
        }
    }
}
