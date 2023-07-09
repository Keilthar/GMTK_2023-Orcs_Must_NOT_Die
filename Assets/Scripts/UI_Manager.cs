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
    Button BT_Credits;
    Button BT_Help;
    Button BT_Quit;
    Label LB_GameTimer;
    VisualElement VE_Credits;
    VisualElement VE_Help;
    VisualElement VE_Win;
    Label LB_Win;

    bool isCreditsDisplayed;
    bool isHelpDisplayed;
    void Start()
    {
        root = transform.GetComponent<UIDocument>().rootVisualElement;

        BT_MagicWall = root.Q<Button>("BT_MagicWall");
        BT_MagicWall.RegisterCallback<ClickEvent>(Event => Game_Manager.Singleton.MagicWall_Cast());

        BT_Heal = root.Q<Button>("BT_Heal");
        BT_Heal.RegisterCallback<ClickEvent>(Event => Game_Manager.Singleton.Heal_Cast());

        BT_Shield = root.Q<Button>("BT_Shield");
        BT_Shield.RegisterCallback<ClickEvent>(Event => Game_Manager.Singleton.Shield_Cast());

        BT_SpeedBoost = root.Q<Button>("BT_SpeedBoost");
        BT_SpeedBoost.RegisterCallback<ClickEvent>(Event => Game_Manager.Singleton.SpeedBoost_Cast());

        LB_GameTimer = root.Q<Label>("LB_GameTimer");

        VE_Credits = root.Q<VisualElement>("VE_Credits");
        VE_Help = root.Q<VisualElement>("VE_Help");

        BT_Credits = root.Q<Button>("BT_Credits");
        BT_Credits.RegisterCallback<ClickEvent>(Event =>
        {
            if (isCreditsDisplayed == true)
                Hide_Credits();
            else
                Display_Credits();
        });
        Hide_Credits();

        BT_Help = root.Q<Button>("BT_Help");
        BT_Help.RegisterCallback<ClickEvent>(Event =>
        {
            if (isHelpDisplayed == true)
                Hide_Help();
            else
                Display_Help();
        });
        Display_Help();

        BT_Quit = root.Q<Button>("BT_Quit");
        BT_Quit.RegisterCallback<ClickEvent>(Event => Application.Quit());

        VE_Win = root.Q<VisualElement>("VE_Win");
        LB_Win = root.Q<Label>("LB_Win");
        Hide_Win();
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
                LB_GameTimer.text = "Best score : " + Get_BestScore() + " sec";
            else LB_GameTimer.text = "Save 20 orcs, quick !";
        }
    }

    public void Display_Win()
    {
        VE_Win.style.opacity = 1;
        LB_Win.text = "You saved 20 orcs in " + Get_FormatedScore(Game_Manager.Singleton.game_Timer) + " sec. <br>" +
        "Your best score is " + Get_BestScore() + " sec. <br><br>" +
        "       Press SPACE to restart !";
    }

    public void Hide_Win()
    {
        VE_Win.style.opacity = 0;
    }

    void Display_Credits()
    {
        Hide_Help();
        VE_Credits.style.opacity = 1;
        isCreditsDisplayed = true;
    }

    void Hide_Credits()
    {
        VE_Credits.style.opacity = 0;
        isCreditsDisplayed = false;
    }

    void Display_Help()
    {
        Hide_Credits();
        VE_Help.style.opacity = 1;
        isHelpDisplayed = true;
    }

    void Hide_Help()
    {
        VE_Help.style.opacity = 0;
        isHelpDisplayed = false;
    }

    string Get_FormatedScore(float Score)
    {
        string sec = ((int) Score).ToString();
        string msec = ((int) ((Score - (int) Score)*100)).ToString();

        return sec + "." + msec;
    }

    string Get_BestScore()
    {
        string formated_BestScore = "";
        float bestScore = 999.99f;
        foreach (float score in Game_Manager.Singleton.L_Scores)
            if (bestScore > score)
                bestScore = score;
        formated_BestScore = Get_FormatedScore(bestScore);

        return formated_BestScore;
    }
}
