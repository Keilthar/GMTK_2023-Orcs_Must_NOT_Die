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
    Button BT_Group1;

    void Start()
    {
        root = transform.GetComponent<UIDocument>().rootVisualElement;
        BT_Group1 = root.Q<Button>("BT_Group1");
        BT_Group1.RegisterCallback<ClickEvent>(Spawn => Enemy_Manager.Singleton.Spawn_Group(1));
    }
}
