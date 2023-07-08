using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_Manager : MonoBehaviour
{
#region Singleton
    public static UI_Manager Singleton;
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

    VisualElement root;
    Button BT_MagicWall;
    Button BT_Heal;
    Button BT_Shield;
    Button BT_SpeedBoost;
    Label LB_GameTimer;

    void Start()
    {
        root = transform.GetComponent<UIDocument>().rootVisualElement;

        BT_MagicWall = root.Q<Button>("BT_MagicWall");
        BT_MagicWall.RegisterCallback<ClickEvent>(Spawn => Game_Manager.Singleton.MagicWall_Cast());

        BT_Heal = root.Q<Button>("BT_Heal");
        BT_Heal.RegisterCallback<ClickEvent>(Spawn => Game_Manager.Singleton.Heal_Cast());

        BT_Shield = root.Q<Button>("BT_Shield");
        BT_Shield.RegisterCallback<ClickEvent>(Spawn => Game_Manager.Singleton.Shield_Cast());

        BT_SpeedBoost = root.Q<Button>("BT_SpeedBoost");
        BT_SpeedBoost.RegisterCallback<ClickEvent>(Spawn => Game_Manager.Singleton.SpeedBoost_Cast());

        LB_GameTimer = root.Q<Label>("LB_GameTimer");
    }

    void Update()
    {
        if (Game_Manager.Singleton.isGameStarted == true)
        {
            string formatedScore = Get_FormatedScore(Game_Manager.Singleton.game_Timer);
            LB_GameTimer.text = "Timer : " + formatedScore + " sec";
        }
        else
        {
            if (Game_Manager.Singleton.L_Scores.Count > 0)
            {
                float bestScore = 999999f;
                foreach (float score in Game_Manager.Singleton.L_Scores)
                    if (bestScore > score)
                        bestScore = score;
                        
                string formatedScore = Get_FormatedScore(bestScore);
                LB_GameTimer.text = "Best score : " + formatedScore + " sec";

            }
            else LB_GameTimer.text = "Save 20 orcs, quick !";
        }
    }

    string Get_FormatedScore(float Score)
    {
        string sec = ((int) Score).ToString();
        for (int i = 3 - sec.Length; i > 0; i--)
        sec = "0" + sec;

        string msec = ((int) ((Score - (int) Score)*100)).ToString();

        return sec + "." + msec;
    }
}
