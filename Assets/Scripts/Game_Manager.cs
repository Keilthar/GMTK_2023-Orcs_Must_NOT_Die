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


    [Header("Game Status")]
        public bool isGameStarted;
        public float game_Timer;
        public int orcs_Saved;
        public int orcs_ToSave = 20;
        public List<float> L_Scores;

    [Header("Spell Cooldowns")]
        public float cooldown_Wall;
        float timer_Wall;
        public float cooldown_Heal;
        float timer_Heal;
        public float cooldown_Shield;
        float timer_Shield;
        public float cooldown_Speed;
        float timer_Speed;

    [Header("Divers")]
        public Transform cam;
        Transform magics_Parent;
        Transform orcs_Parent;
        bool mouse_left_pressed;
        bool mouse_right_pressed;

    void Start()
    {
        L_Scores = new List<float>();
        cam = GameObject.Find("Main Camera").transform;
        magics_Parent = transform.Find("Magics");
        orcs_Parent = transform.Find("Orcs");
        Init_Game();
    }

    void Update()
    {
        if (isGameStarted == true)
        {
            game_Timer += Time.deltaTime;
            timer_Wall += Time.deltaTime;
            timer_Heal += Time.deltaTime;
            timer_Shield += Time.deltaTime;
            timer_Speed += Time.deltaTime;
        }
            

        mouse_left_pressed = false;
        mouse_right_pressed = false;
    
        if (Input.GetKeyDown(KeyCode.Mouse0))
            mouse_left_pressed = true;
        if (Input.GetKeyDown(KeyCode.Mouse1))
            mouse_right_pressed = true;

        if (Input.GetKeyDown(KeyCode.Space))
            Enemy_Manager.Singleton.Spawn_Group(1);
        if (Input.GetKeyDown(KeyCode.Alpha1) && timer_Wall >= cooldown_Wall)
            MagicWall_Cast();
        if (Input.GetKeyDown(KeyCode.Alpha2) && timer_Heal >= cooldown_Heal)
            Heal_Cast();
        if (Input.GetKeyDown(KeyCode.Alpha3) && timer_Shield >= cooldown_Shield)
            Shield_Cast();
        if (Input.GetKeyDown(KeyCode.Alpha4) && timer_Speed >= cooldown_Speed)
            SpeedBoost_Cast(); 
    }

    void Init_Game()
    {
        isGameStarted = false;
        Clear_Magic();
        Clear_Orcs();
        orcs_Saved = 0;
        game_Timer = 0f;
    }

    public void Orc_Saved()
    {
        orcs_Saved ++;
        if (orcs_Saved >= orcs_ToSave)
        {
            L_Scores.Add(game_Timer);
            UI_Manager.Singleton.Display_Win();
            Init_Game();
        }
    }

    void Clear_Magic()
    {
        StopAllCoroutines();
        foreach (Transform child in magics_Parent)
        {
            MagicWall_Controler wall = child.GetComponent<MagicWall_Controler>();
            if (wall != null)
            {
                if (wall.isDeployed == false)
                    Destroy(child.gameObject);
            }
            else Destroy(child.gameObject);
        } 
    }

    void Clear_Orcs()
    {
        foreach (Transform child in orcs_Parent)
            Destroy(child.gameObject);
    }
    
    public void MagicWall_Cast()
    {
        timer_Wall = 0;
        Clear_Magic();
        StartCoroutine(MagicWall_Placement());
    }

    IEnumerator MagicWall_Placement()
    {
        bool isMagicDeployed = false;
        GameObject prefab = Resources.Load<GameObject>("Magics/MagicWall");
        GameObject spell = Instantiate(prefab, Vector3.down * 10, Quaternion.identity);
        spell.transform.rotation = Quaternion.Euler(0,0,90);
        spell.transform.parent = magics_Parent;

        while (isMagicDeployed == false)
        {
            if (mouse_left_pressed == true)
                isMagicDeployed = true;
            if (mouse_right_pressed == true)
                break;
                

            Vector3 mousePosition = Mouse_Get_Position();
            Vector3 offsetPosition = Vector3.up * 2.5f;
            spell.transform.position = mousePosition + offsetPosition;

            yield return new WaitForEndOfFrame();
        }

        if (isMagicDeployed == true)
            spell.GetComponent<MagicWall_Controler>().Init();
        else
            Destroy(spell);
        
        yield break;
    }

    public void Heal_Cast()
    {
        timer_Heal = 0;
        Clear_Magic();
        StartCoroutine(Heal_Placement());
    }

    IEnumerator Heal_Placement()
    {
        bool isMagicDeployed = false;
        GameObject prefab = Resources.Load<GameObject>("Magics/Heal");
        GameObject spell = Instantiate(prefab, Vector3.down * 10, Quaternion.identity);
        spell.transform.rotation = Quaternion.Euler(0,0,90);
        spell.transform.parent = magics_Parent;

        while (isMagicDeployed == false)
        {
            if (mouse_left_pressed == true)
                isMagicDeployed = true;
            if (mouse_right_pressed == true)
                break;

            Vector3 mousePosition = Mouse_Get_Position();
            Vector3 offsetPosition = Vector3.up * 0f;
            spell.transform.position = mousePosition + offsetPosition;

            yield return new WaitForEndOfFrame();
        }

        if (isMagicDeployed == true)
            spell.GetComponent<Heal_Controler>().Heal();
        else
            Destroy(spell);
        
        yield break;
    }

    public void Shield_Cast()
    {
        timer_Shield = 0;
        Clear_Magic();
        StartCoroutine(Shield_Placement());
    }

    IEnumerator Shield_Placement()
    {
        bool isMagicDeployed = false;
        GameObject prefab = Resources.Load<GameObject>("Magics/Shield");
        GameObject spell = Instantiate(prefab, Vector3.down * 10, Quaternion.identity);
        spell.transform.rotation = Quaternion.Euler(0,0,90);
        spell.transform.parent = magics_Parent;

        while (isMagicDeployed == false)
        {
            if (mouse_left_pressed == true)
                isMagicDeployed = true;
            if (mouse_right_pressed == true)
                break;

            Vector3 mousePosition = Mouse_Get_Position();
            Vector3 offsetPosition = Vector3.up * 0f;
            spell.transform.position = mousePosition + offsetPosition;

            yield return new WaitForEndOfFrame();
        }

        if (isMagicDeployed == true)
            spell.GetComponent<Shield_Controler>().Shield();
        else
            Destroy(spell);
        
        yield break;
    }

    public void SpeedBoost_Cast()
    {
        timer_Speed = 0;
        Clear_Magic();
        StartCoroutine(SpeedBoost_Placement());
    }

    IEnumerator SpeedBoost_Placement()
    {
        bool isMagicDeployed = false;
        GameObject prefab = Resources.Load<GameObject>("Magics/SpeedBoost");
        GameObject spell = Instantiate(prefab, Vector3.down * 10, Quaternion.identity);
        spell.transform.rotation = Quaternion.Euler(0,0,90);
        spell.transform.parent = magics_Parent;

        while (isMagicDeployed == false)
        {
            if (mouse_left_pressed == true)
                isMagicDeployed = true;
            if (mouse_right_pressed == true)
                break;

            Vector3 mousePosition = Mouse_Get_Position();
            Vector3 offsetPosition = Vector3.up * 0f;
            spell.transform.position = mousePosition + offsetPosition;

            yield return new WaitForEndOfFrame();
        }

        if (isMagicDeployed == true)
            spell.GetComponent<SpeedBoost_Controler>().SpeedBoost();
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
