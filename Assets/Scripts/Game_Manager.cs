using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
#region Singleton
    public static Game_Manager Singleton;
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

    public int enemy_Kill_Counter;

    void Start()
    {
        enemy_Kill_Counter = 0;
    }
}
