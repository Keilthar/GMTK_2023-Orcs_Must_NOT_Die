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
    Button BT_Warrior;
    Button BT_Shaman;

    void Start()
    {
        root = transform.GetComponent<UIDocument>().rootVisualElement;
        BT_Warrior = root.Q<Button>("BT_Warrior");
        BT_Warrior.RegisterCallback<ClickEvent>(Spawn => Enemy_Manager.Singleton.Spawn_Enemy(1));
    }
}
