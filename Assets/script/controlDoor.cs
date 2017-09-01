using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorState
{
    Inactive,
    Active
}
public class controlDoor : MonoBehaviour
{
    public DoorState DS;
    public Renderer rend;
    public Shader shader;
    public Texture Inactive_Image;
    public Texture[] Active_Images = new Texture[7];
    public float Ani_Speed;
    public float _now_ani_time;
    public int Ani_count = 0, i = 0;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        Inactive_Image = Resources.Load("DOOR/DOOR0", typeof(Texture)) as Texture;
        for (i = 0; i < 7; i++)
        {
            print(i);
            Active_Images[i] = Resources.Load("DOOR/DOOR" + i.ToString(), typeof(Texture)) as Texture;
            print(i);
        }
        shader = Shader.Find("Unlit/Transparent");
        rend.material.shader = shader;
        rend.material.mainTexture = Inactive_Image;
    }
    void Update()
    {
        if (_now_ani_time >= Ani_Speed)
        {
            if (DS == DoorState.Active)
            {
                Active_Ing();
            }
            _now_ani_time -= Ani_Speed;
        }
        _now_ani_time += Time.deltaTime;
    }
    public void Active_On()
    {
        DS = DoorState.Active;
        Ani_count = 0;
    }
    public void Active_Ing()
    {
        rend.material.mainTexture = Active_Images[Ani_count];
        Ani_count++;
        if (Ani_count == 7)
        {
            Ani_count -= 2;
        }
    }
    public void Inactive_On()
    {
        rend.material.mainTexture = Inactive_Image;
        Ani_count = 0;
        DS = DoorState.Inactive;
    }
    void OnTriggerEnter(Collider c)
    {
        print(c.gameObject);
        if (c.gameObject.tag == "Player")
        {
            print("난 완전해졌다");
            Active_On();
        }
    }
    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            if (Global.Activated == true)
            {
                //clear
            }
        }
    }
    void OnTriggerExit(Collider c)
    {
        print("난 불완전해졌다");
        if (c.gameObject.tag == "Player")
        {
            Inactive_On();
        }
    }
}