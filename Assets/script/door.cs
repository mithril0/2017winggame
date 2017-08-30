using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DoorState
{
    Inactive,
    Active
} 
public class Door : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    public Renderer rend;
    public DoorState DS;
    public Sprite Inactive_Image;
    public Sprite[] Active_Images;
    public float Ani_Speed;
    public float _now_ani_time;
    int Ani_count;
	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (DS == DoorState.Active)
        {
            Active_Ing();
        }
	}
    public void Active_On()
    {
        DS = DoorState.Active;
        Ani_count = 0;
    }
    public void Active_Ing()
    {
        spriteRenderer.sprite = Active_Images[Ani_count];
        Ani_count++;
        if (Ani_count == 7)
        {
            Ani_count -= 2;
        }
    }
    public void Inactive_On()
    {
        spriteRenderer.sprite = Inactive_Image;
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
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
        if (c.gameObject.tag == "Player")
        {
            Inactive_On();
        }
    } 
}
