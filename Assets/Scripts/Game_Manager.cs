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

    public Transform cam;
    const int mana_Max = 100;
    public int mana_Current;
    public int enemy_Kill_Counter;

    bool mouse_left_pressed;
    bool mouse_right_pressed;
    bool rotation_Pressed;

    void Start()
    {
        enemy_Kill_Counter = 0;
        cam = GameObject.Find("Main Camera").transform;
    }

    void Update()
    {
        mouse_left_pressed = false;
        mouse_right_pressed = false;
        rotation_Pressed = false;
    
        if (Input.GetKeyDown(KeyCode.Mouse0))
            mouse_left_pressed = true;
        if (Input.GetKeyDown(KeyCode.Mouse1))
            mouse_right_pressed = true;
        if (Input.GetKeyDown(KeyCode.R))
            rotation_Pressed = true;
    }

    public void MagicWall_Cast()
    {
        StartCoroutine(MagicWall_Placement());
    }

    IEnumerator MagicWall_Placement()
    {
        bool isMagicDeployed = false;
        GameObject prefab = Resources.Load<GameObject>("Magics/MagicWall");
        GameObject magicWall = Instantiate(prefab, Vector3.down * 10, Quaternion.identity);
        magicWall.transform.rotation = Quaternion.Euler(0,0,90);

        while (isMagicDeployed == false)
        {
            if (mouse_left_pressed == true)
                isMagicDeployed = true;
            if (mouse_right_pressed == true)
                break;

            Vector3 rotation = Vector3.zero;
            if (rotation_Pressed)
                rotation = new Vector3(45,0,0);
                

            Vector3 mousePosition = Mouse_Get_Position();
            Vector3 offsetPosition = Vector3.up * 2.5f;
            magicWall.transform.position = mousePosition + offsetPosition;
            magicWall.transform.rotation *=  Quaternion.Euler(rotation);

            yield return new WaitForEndOfFrame();
        }

        if (isMagicDeployed == true)
            magicWall.GetComponent<MagicWall_Controler>().Init();
        else
            Destroy(magicWall);
        
        yield break;
    }

    public void Heal_Cast()
    {
        StartCoroutine(Heal_Placement());
    }

    IEnumerator Heal_Placement()
    {
        bool isMagicDeployed = false;
        GameObject prefab = Resources.Load<GameObject>("Magics/Heal");
        GameObject magicWall = Instantiate(prefab, Vector3.down * 10, Quaternion.identity);
        magicWall.transform.rotation = Quaternion.Euler(0,0,90);

        while (isMagicDeployed == false)
        {
            if (mouse_left_pressed == true)
                isMagicDeployed = true;
            if (mouse_right_pressed == true)
                break;

            Vector3 rotation = Vector3.zero;
            if (rotation_Pressed)
                rotation = new Vector3(45,0,0);
                

            Vector3 mousePosition = Mouse_Get_Position();
            Vector3 offsetPosition = Vector3.up * 0f;
            magicWall.transform.position = mousePosition + offsetPosition;
            magicWall.transform.rotation *=  Quaternion.Euler(rotation);

            yield return new WaitForEndOfFrame();
        }

        if (isMagicDeployed == true)
            magicWall.GetComponent<Heal_Controler>().Heal();
        else
            Destroy(magicWall);
        
        yield break;
    }

    public void Shield_Cast()
    {
        StartCoroutine(Shield_Placement());
    }

    IEnumerator Shield_Placement()
    {
        bool isMagicDeployed = false;
        GameObject prefab = Resources.Load<GameObject>("Magics/Shield");
        GameObject spell = Instantiate(prefab, Vector3.down * 10, Quaternion.identity);
        spell.transform.rotation = Quaternion.Euler(0,0,90);

        while (isMagicDeployed == false)
        {
            if (mouse_left_pressed == true)
                isMagicDeployed = true;
            if (mouse_right_pressed == true)
                break;

            Vector3 rotation = Vector3.zero;
            if (rotation_Pressed)
                rotation = new Vector3(45,0,0);
                

            Vector3 mousePosition = Mouse_Get_Position();
            Vector3 offsetPosition = Vector3.up * 0f;
            spell.transform.position = mousePosition + offsetPosition;
            spell.transform.rotation *=  Quaternion.Euler(rotation);

            yield return new WaitForEndOfFrame();
        }

        if (isMagicDeployed == true)
            spell.GetComponent<Shield_Controler>().Shield();
        else
            Destroy(spell);
        
        yield break;
    }

    public Vector3 Mouse_Get_Position()
    {
        // Return mouse position on grid ground
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hit = Physics.RaycastAll(ray);
        Vector3 position = Vector3.up;
        for (int hitNum = 0; hitNum<hit.Length; hitNum++)
        {
            if (hit[hitNum].transform.tag == "Ground")
                position = hit[hitNum].point;
        }
        return position;
    }

}
