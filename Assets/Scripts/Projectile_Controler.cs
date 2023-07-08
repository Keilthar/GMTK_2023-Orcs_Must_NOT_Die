using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Controler : MonoBehaviour
{
    public AnimationCurve trajectory;
    int damage;
    public float speed = 20;
    Vector3 position_Initial;
    Vector3 position_Final;
    Transform target;
    float travel_Time;
    float timer;

    public void Init(Transform Target, Vector3 PositionInitial, int Damage)
    {
        target = Target;
        position_Initial = PositionInitial;
        damage = Damage;
        transform.position = position_Initial;
        travel_Time = Vector3.Distance(position_Initial, target.position) / speed;
        timer = 0;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(position_Initial, target.position, timer / travel_Time);
            timer += Time.deltaTime;
        }
            
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MagicWall")
            Destroy(gameObject);

        if (other.gameObject == target.gameObject)
        {
            Enemy_Controler enemy = other.gameObject.GetComponent<Enemy_Controler>();
            enemy.Set_Damage(damage);
            Destroy(gameObject);
        }
    }
}
