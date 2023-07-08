using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Controler : MonoBehaviour
{
    int shield = 2;
    List<GameObject> L_Targets;

    void Awake()
    {
        L_Targets = new List<GameObject>();
    }

    public void Shield()
    {
        foreach(GameObject unit in L_Targets)
            unit.GetComponent<Enemy_Controler>().Set_Shield(shield);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && L_Targets.Contains(other.gameObject) == false)
            L_Targets.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy" && L_Targets.Contains(other.gameObject) == true)
            L_Targets.Remove(other.gameObject);
    }
}
