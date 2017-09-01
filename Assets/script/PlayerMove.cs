using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public Renderer rend;
    public Shader shader;
    public Texture[] MovingCW=new Texture[2];
    public Texture[] MovingCCW = new Texture[2];
    public Texture[] IdleCW = new Texture[2];
    public Texture[] IdleCCW = new Texture[2];
    public Texture[] Casting;
    public Texture[] Teleporting;
    public Texture[] Dead;
    public Texture[] Clear;
    public Texture[] Rotate;
    public float StickRange;
    public float Speed;
    public Playerstates2 PS2;
    public float Ani_Speed, _now_ani_time;
    public int Ani_Count,i;
    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        for (i = 0; i < 2; i++)
        {
            MovingCW[i] = Resources.Load("Player/CW_Walk" + i.ToString(), typeof(Texture)) as Texture;
        }
        for (i = 0; i < 2; i++)
        {
            MovingCCW[i] = Resources.Load("Player/CCW_Walk" + i.ToString(), typeof(Texture)) as Texture;
        }
        for (i = 0; i < 2; i++)
        {
            IdleCW[i] = Resources.Load("Player/CW_Idle" + i.ToString(), typeof(Texture)) as Texture;
        }
        for (i = 0; i < 2; i++)
        {
            IdleCCW[i] = Resources.Load("Player/CCW_Idle" + i.ToString(), typeof(Texture)) as Texture;
        }
        shader = Shader.Find("Unlit/Transparent");
        rend.material.shader = shader;
    }

    // Update is called once per frame
    void Update () {
        //print(PS2);
        //이동키에 방향 배정
        Dictionary<KeyCode, Vector3> dict = new Dictionary<KeyCode, Vector3>(4);
        dict.Add(KeyCode.A, Vector3.left);
        dict.Add(KeyCode.D, Vector3.right);
        dict.Add(KeyCode.W, Vector3.up);
        dict.Add(KeyCode.S, -Vector3.up);

        foreach (KeyValuePair<KeyCode, Vector3> entry in dict)
        {
            //키가 눌러졌고, 방향이 플레이어 기준 좌측 혹은 우측일 때
            if (Input.GetKey(entry.Key) && transform.up != entry.Value && transform.up != -entry.Value)
            {
                if (transform.right == entry.Value)
                {   
                    if(PS2 != Playerstates2.MovingCW)
                    {
                        Ani_Count = 0;
                        print("시계방향진행으로 상태변경");
                    }
                    MovingCW_On();
                    //print(PS2);
                }
                else if (transform.right == -entry.Value)
                {
                    if (PS2 != Playerstates2.MovingCCW)
                    {
                        Ani_Count = 0;
                        print("반시계방향진행으로 상태변경");
                    }
                    MovingCCW_On();
                }
                RaycastHit hit;
                if (Physics.Raycast(transform.position, entry.Value, out hit, StickRange, 1 << 8))
                {
                    //가려는 방향에 벽이 있으면 그 벽에 붙음
                    StickToWall(hit);
                }
                else
                {
                    //이동
                    transform.position = transform.position + entry.Value * Speed * Time.deltaTime;
                }
            }
            if (_now_ani_time >= Ani_Speed)
            {
                if (PS2 == Playerstates2.IdleCW)
                {
                    IdleCW_Ing();
                }
                if (PS2 == Playerstates2.IdleCCW)
                {
                    IdleCCW_Ing();
                }
                if (PS2 == Playerstates2.MovingCW)
                {
                    MovingCW_Ing();
                    print("한다 나는 갱신");
                }
                if (PS2 == Playerstates2.MovingCCW)
                {
                    MovingCCW_Ing();
                    print("한다 나는 갱신2");
                }
                if (PS2 == Playerstates2.Teleporting)
                {
                    Teleporting_Ing();
                }
                _now_ani_time -= Ani_Speed;
            }//애니재생
            else
            {
                _now_ani_time += Time.deltaTime;
            }
            
                
        }
        if((Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D))==false) {
            if (PS2 == Playerstates2.MovingCCW)
            {
                PS2 = Playerstates2.IdleCCW;
            }
            else if(PS2 == Playerstates2.MovingCW)
            {
                PS2 = Playerstates2.IdleCW;
            }
        }
        //플레이어의 발쪽으로 레이캐스트
        RaycastHit groundHit;
        if (Physics.Raycast(transform.position, -transform.up, out groundHit, StickRange, 1 << 8))
        {
            StickToWall(groundHit);
        }
        else
        {
            //바닥이 없으면 우측, 좌측으로 레이캐스트 하고 거리가 짧은 쪽 벽에 붙음
            RaycastHit rightHit;
            RaycastHit leftHit;

            if (!Physics.Raycast(transform.position - transform.up * (transform.localScale.y / 2 + 0.01f), transform.right, out rightHit, StickRange, 1 << 8))
            {
                rightHit.distance = Mathf.Infinity;
            }
            if (!Physics.Raycast(transform.position - transform.up * (transform.localScale.y / 2 + 0.01f), -transform.right, out leftHit, StickRange, 1 << 8))
            {
                leftHit.distance = Mathf.Infinity;
            }
            StickToWall(rightHit.distance > leftHit.distance? leftHit : rightHit);
        }
        //print(PS2);
       
        //print(PS2);

    }

    void StickToWall(RaycastHit hit)
    {
        //레이캐스트 정보를 바탕으로 벽에 붙음
        if (hit.collider != null) {
            Vector3 normal = hit.normal;
            Vector3 o = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            transform.eulerAngles = transform.eulerAngles + new Vector3(0,0, - transform.eulerAngles.z + Mathf.Rad2Deg * Mathf.Atan2(normal.y, normal.x)-90);
            transform.position = hit.point;
            transform.Translate(Vector3.up*transform.localScale.y/2);
        }
    }
    void MovingCW_On()
    {
        PS2 = Playerstates2.MovingCW;
    }
    void MovingCCW_On()
    {
        PS2 = Playerstates2.MovingCCW;
        //Ani_Count = 0;
    }
    void Casting_On()
    {
        PS2 = Playerstates2.Casting;
        Ani_Count = 0;
    }
    void Teleporting_On()
    {
        PS2 = Playerstates2.Teleporting;
        Ani_Count = 0;
    }
    void Dead_On()
    {
        PS2 = Playerstates2.Dead;
        Ani_Count = 0;
    }
    void IdleCW_On()
    {
        PS2 = Playerstates2.IdleCW;
        Ani_Count = 0;
    }
    void IdleCCW_On()
    {
        PS2 = Playerstates2.IdleCCW;
    }
    void Clear_On()
    {
        PS2 = Playerstates2.Clear;
        Ani_Count = 0;
    }
    void MovingCW_Ing()
    {
        rend.material.mainTexture = MovingCW[Ani_Count];
        Ani_Count++;
        print("카운트");
        if(Ani_Count > 1)
        {
            Ani_Count %= 2;
            print(Ani_Count);
        }
    }
    void MovingCCW_Ing()
    {
        rend.material.mainTexture = MovingCCW[Ani_Count];
        Ani_Count++;
        if (Ani_Count > 1)
        {
            Ani_Count %= 2;
        }
    }
    void Casting_Ing()
    {

    }
    void Teleporting_Ing()
    {

    }
    void Dead_Ing()
    {

    }
    void IdleCW_Ing()
    {
        rend.material.mainTexture = IdleCW[Ani_Count];
        Ani_Count++;
        if (Ani_Count > 1)
        {
            Ani_Count %= 2;
        }
    }
    void IdleCCW_Ing()
    {
        rend.material.mainTexture = IdleCCW[Ani_Count];
        Ani_Count++;
        if (Ani_Count > 1)
        {
            Ani_Count %= 2;
        }
    }
    void Clear_Ing()
    {

    }
}
