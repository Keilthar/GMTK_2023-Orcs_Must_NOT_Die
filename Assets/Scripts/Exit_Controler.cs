using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Exit_Controler : MonoBehaviour
{
    public TMP_Text score;

    void Update()
    {
        string text = Game_Manager.Singleton.orcs_Saved + "/ " + Game_Manager.Singleton.orcs_ToSave;
        score.text = text;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Game_Manager.Singleton.Orc_Saved();
            Destroy(other.gameObject);
        }
            
    }
}
