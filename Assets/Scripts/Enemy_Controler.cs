using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Controler : MonoBehaviour
{
    const float health_Display_Timer = 0.2f;
    Enemy enemy;
    Transform healthBar;
    Image health;
    Transform cam;

    void Awake()
    {
        enemy = Enemy_Type.Get_Enemy(transform.name);
        healthBar = transform.Find("Health_Bar");
        health = healthBar.Find("Health_Amount").GetComponent<Image>();
        cam = GameObject.Find("Main Camera").transform;
    }

    void Update()
    {
        healthBar.LookAt(cam.position);
    }

    public void Set_Damage(int DamageTaken)
    {
        enemy.Set_Damage(DamageTaken);
        StopAllCoroutines();
        StartCoroutine(Display_Health());

        if (enemy.health_Current <= 0)
            Death();
    }

    IEnumerator Display_Health()
    {
        float timer = 0f;
        float fillAmount_Current = health.fillAmount;
        float fillAmount_New = (float) enemy.health_Current / enemy.health_Max;

        while (timer < health_Display_Timer)
        {
            health.fillAmount = fillAmount_Current - (fillAmount_Current - fillAmount_New) * timer / health_Display_Timer;
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        health.fillAmount = fillAmount_New;

        yield break;
    }

    public bool Is_Alive()
    {
        if (enemy.health_Current > 0)
            return true;
        else
            return false;
    }

    public void Death()
    {
        Game_Manager.Singleton.enemy_Kill_Counter++;
        Debug.Log("RIP " + transform.name);
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(health_Display_Timer);
        Destroy(gameObject);
    }
}
