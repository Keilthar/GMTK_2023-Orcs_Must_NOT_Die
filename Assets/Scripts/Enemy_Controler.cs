using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Controler : MonoBehaviour
{
    const float health_Display_Timer = 0.2f;
    Enemy enemy;
    Transform healthBar;
    Transform buffBar;
    Image health;

    void Awake()
    {
        enemy = Enemy_Type.Get_Enemy(transform.name);
        healthBar = transform.Find("Health_Bar");
        health = healthBar.Find("Health_Amount").GetComponent<Image>();
        buffBar = transform.Find("Buff_Bar");
        Display_Buff();
    }

    void Update()
    {
        healthBar.LookAt(Game_Manager.Singleton.cam.position);
        buffBar.LookAt(Game_Manager.Singleton.cam.position);
    }

    public void Set_Damage(int DamageTaken)
    {
        enemy.Set_Damage(DamageTaken);
        StopAllCoroutines();
        StartCoroutine(Display_Health());

        if (enemy.health_Current <= 0)
            Death();
    }

    public void Set_Heal(int HealTaken)
    {
        enemy.Set_Heal(HealTaken);
        StopAllCoroutines();
        StartCoroutine(Display_Health());
    }

    public void Set_Shield(int ShieldCount)
    {
        enemy.Set_Shield(ShieldCount);
        StopAllCoroutines();
        StartCoroutine(Display_Health());
    }

    public void Set_SpeedBoost()
    {
        transform.parent.GetComponent<Group_Controler>().Set_SpeedBoost();
    }

    IEnumerator Display_Health()
    {
        float timer = 0f;
        float fillAmount_Current = health.fillAmount;
        float fillAmount_New = (float) enemy.health_Current / enemy.health_Max;

        Display_Buff();

        while (timer < health_Display_Timer)
        {
            health.fillAmount = fillAmount_Current - (fillAmount_Current - fillAmount_New) * timer / health_Display_Timer;
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        health.fillAmount = fillAmount_New;

        yield break;
    }

    void Display_Buff()
    {
        if (enemy.shield_Count >= 1)
            buffBar.Find("Shield_1").GetComponent<Image>().enabled = true;
        else
            buffBar.Find("Shield_1").GetComponent<Image>().enabled = false;

        if (enemy.shield_Count == 2)
            buffBar.Find("Shield_2").GetComponent<Image>().enabled = true;
        else
            buffBar.Find("Shield_2").GetComponent<Image>().enabled = false;

        buffBar.Find("Speed").GetComponent<Image>().enabled = enemy.speed;
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
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(health_Display_Timer);
        Destroy(gameObject);
    }
}
