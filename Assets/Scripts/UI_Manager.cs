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
    Button BT_Spawn;
    Button BT_MagicWall;
    Button BT_Heal;
    Button BT_Shield;

    void Start()
    {
        root = transform.GetComponent<UIDocument>().rootVisualElement;
        BT_Spawn = root.Q<Button>("BT_Spawn");
        BT_Spawn.RegisterCallback<ClickEvent>(Spawn => Enemy_Manager.Singleton.Spawn_Group(1));

        BT_MagicWall = root.Q<Button>("BT_MagicWall");
        BT_MagicWall.RegisterCallback<ClickEvent>(Spawn => Game_Manager.Singleton.MagicWall_Cast());

        BT_Heal = root.Q<Button>("BT_Heal");
        BT_Heal.RegisterCallback<ClickEvent>(Spawn => Game_Manager.Singleton.Heal_Cast());

        BT_Shield = root.Q<Button>("BT_Shield");
        BT_Shield.RegisterCallback<ClickEvent>(Spawn => Game_Manager.Singleton.Shield_Cast());
    }
}
