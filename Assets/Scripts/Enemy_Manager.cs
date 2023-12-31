using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enemy_Type
{
    public static string Warrior = "Warrior";
    public static string Shaman = "Shaman";
    public static string Villager = "Villager";

    public static Enemy Get_Enemy(string EnemyType)
    {
        Enemy enemy = null;
        if (EnemyType == Enemy_Type.Warrior)
            enemy = new Warrior();
        else if (EnemyType == Enemy_Type.Shaman)
            enemy = new Shaman();
        else if (EnemyType == Enemy_Type.Villager)
            enemy = new Villager();

        return enemy;
    }
}

public class Enemy
{
    public int health_Max;
    public int health_Current;
    public int shield_Count;
    public bool speed;


    public void Set_Damage(int DamageTaken)
    {
        if (shield_Count > 0)
            shield_Count--;
        else
            health_Current -= DamageTaken;
    }

    public void Set_Heal(int HealTaken)
    {
        health_Current += HealTaken;
        if (health_Current > health_Max)
            health_Current = health_Max;
    }

    public void Set_Shield(int ShieldCount)
    {
        shield_Count += ShieldCount;
        if (shield_Count > 2)
            shield_Count = 2;
    }

    public void Set_Speed(bool Status)
    {
        speed = Status;
    }
}

public class Villager : Enemy
{
    public Villager()
    {
        health_Max = Enemy_Manager.Singleton.villager_Health;
        health_Current = health_Max;
    }
}

public class Warrior : Enemy
{
    public Warrior()
    {
        health_Max = Enemy_Manager.Singleton.warrior_Health;
        health_Current = health_Max;
    }
}

public class Shaman : Enemy
{
    public Shaman()
    {
        health_Max = Enemy_Manager.Singleton.shaman_Health;
        health_Current = health_Max;
    }
}

public class Enemy_Manager : MonoBehaviour
{
#region Singleton
    public static Enemy_Manager Singleton;
    private void Awake()
    {
        if (Singleton!= null)
        {
            Debug.Log("Duplicated " + this.name);
            return;
        }
        else Singleton= this;
    }
#endregion
    
    [Header("Speeds")]
        public float speed = 10f;
    [Header("Villagers Stats")]
        public int villager_Health = 20;

    [Header("Warriors Stats")]
        public int warrior_Health = 100;

    [Header("Shaman Stats")]
        public int shaman_Health = 50;

    int enemyCounter;
    float spawn_Cooldown = 2f;
    float spawn_Timer;
    public List<Transform> L_Orcs;
    

    void Start()
    {
        enemyCounter = 0;
    }

    void Update()
    {
        if (spawn_Timer > 0)
            spawn_Timer -= Time.deltaTime;

        L_Orcs = new List<Transform>();
        foreach(Transform group in transform)
            foreach(Transform orc in group)
                L_Orcs.Add(orc);  
    }

    public void Spawn_Group(int GroupID)
    {
        if (spawn_Timer <= 0)
        {
            spawn_Timer = spawn_Cooldown;
            if (Game_Manager.Singleton.isGameStarted == false)
            {
                Game_Manager.Singleton.isGameStarted = true;
                UI_Manager.Singleton.Hide_Win();
            }
                

            GameObject prefab = Resources.Load<GameObject>("Enemies/Groups/Group_" + GroupID);
            GameObject enemy = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            if (enemy != null)
            {
                enemy.transform.parent = transform;
                enemyCounter++;
            }
        }
    }
}
