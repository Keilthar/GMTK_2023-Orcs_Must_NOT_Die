using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense_Manager : MonoBehaviour
{
#region Singleton
    public static Defense_Manager Singleton;
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
    
    public List<GameObject> L_Defenses;

    void Start()
    {
        L_Defenses = new List<GameObject>();
        foreach(Transform child in transform)
            L_Defenses.Add(child.gameObject);
    }
}
