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

    public void Set_Damage(int DamageTaken)
    {
        health_Current -= DamageTaken;
    }

    public void Set_Heal(int HealTaken)
    {
        health_Current += HealTaken;
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
    

    void Start()
    {
        enemyCounter = 0;
    }

    public void Spawn_Enemy(int GroupID)
    {
        GameObject prefab = Resources.Load<GameObject>("Enemies/Groups/Group_" + GroupID);
        GameObject enemy = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        if (enemy != null)
        {
            enemy.transform.parent = transform;
            enemyCounter++;
        }
    }
}
