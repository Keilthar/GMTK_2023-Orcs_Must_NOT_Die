using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Controler : MonoBehaviour
{
    int shield = 2;
    List<GameObject> L_Orcs;

    void Awake()
    {
        L_Orcs = new List<GameObject>();
    }

    public void Shield()
    {
        foreach(GameObject orc in L_Orcs)
            if (orc != null)
                orc.GetComponent<Enemy_Controler>().Set_Shield(shield);

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && L_Orcs.Contains(other.gameObject) == false)
            L_Orcs.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy" && L_Orcs.Contains(other.gameObject) == true)
            L_Orcs.Remove(other.gameObject);
    }
}
