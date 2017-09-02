using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Playerstates2
{
    MovingCCW,
    MovingCW,
    CastingCW,
    CastingCCW,
    TeleportingCW,
    TeleportingCCW,
    DeadCW,
    DeadCCW,
    IdleCW,
    IdleCCW,
    Clear,
    RotateCW,
    RotateCCW
}
public class PlayerMove : MonoBehaviour
{
    public GameObject Player;
    public Renderer rend;
    public Shader shader;
    public Texture[] MovingCW = new Texture[2];
    public Texture[] MovingCCW = new Texture[2];
    public Texture[] IdleCW = new Texture[2];
    public Texture[] IdleCCW = new Texture[2];
    public Texture[] RotateCW=new Texture[7];
    public Texture[] RotateCCW = new Texture[7];
    public Texture[] CastingCW = new Texture[4];
    public Texture[] CastingCCW = new Texture[4];
    public Texture[] TeleportCW = new Texture[24];
    public Texture[] TeleportCCW = new Texture[24];
    public Texture[] DeathCW = new Texture[21];
    public Texture[] DeathCCW = new Texture[21];
    public float StickRange, RotateRange;
    public float Speed;
    public Playerstates2 PS2;
    public float Ani_Speed, _now_ani_time;
    public int Ani_Count, i;
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
        for (i = 0; i < 7; i++)
        {
            print(i);
            RotateCW[i] = Resources.Load("Player/Rotate/CW" + i.ToString(), typeof(Texture)) as Texture;
        }
        for (i = 0; i < 7; i++)
        {
            RotateCCW[i] = Resources.Load("Player/Rotate/CCW" + i.ToString(), typeof(Texture)) as Texture;
        }
        for (i = 0; i < 4; i++)
        {
            CastingCW[i] = Resources.Load("Player/Casting/CW" + i.ToString(), typeof(Texture)) as Texture;
        }
        for (i = 0; i < 4; i++)
        {
            CastingCCW[i] = Resources.Load("Player/Casting/CCW" + i.ToString(), typeof(Texture)) as Texture;
        }
        for (i = 0; i < 10; i++)
        {
            TeleportCW[i] = Resources.Load("Player/Teleport/cw" + i.ToString(), typeof(Texture)) as Texture;
        }
        for (i = 10; i < 20; i++)
        {
            TeleportCW[i] = Resources.Load("Player/Teleport/cw" + (19 - i).ToString(), typeof(Texture)) as Texture;
        }
        for (i = 20; i < 24; i++)
        {
            TeleportCW[i] = Resources.Load("Player/Casting/CW" + (23 - i).ToString(), typeof(Texture)) as Texture;
        }
        for (i = 0; i < 10; i++)
        {
            TeleportCCW[i] = Resources.Load("Player/Teleport/ccw" + i.ToString(), typeof(Texture)) as Texture;
        }
        for (i = 10; i < 20; i++)
        {
            TeleportCCW[i] = Resources.Load("Player/Teleport/cw" + (19 - i).ToString(), typeof(Texture)) as Texture;
        }
        for (i = 20; i < 24; i++)
        {
            TeleportCCW[i] = Resources.Load("Player/Casting/CW" + (23 - i).ToString(), typeof(Texture)) as Texture;
        }
        for (i = 0; i < 21; i++)
        {
            DeathCW[i] = Resources.Load("Player/Death/cw" + i.ToString(), typeof(Texture)) as Texture;
        }
        for (i = 0; i < 21; i++)
        {
            DeathCCW[i] = Resources.Load("Player/Death/ccw" + i.ToString(), typeof(Texture)) as Texture;
        }
        shader = Shader.Find("Unlit/Transparent");
        rend.material.shader = shader;
    }

    // Update is called once per frame
    void Update()
    {
        //print(PS2);
        //이동키에 방향 배정
        Dictionary<KeyCode, Vector3> dict = new Dictionary<KeyCode, Vector3>(4);
        dict.Add(KeyCode.A, Vector3.left);
        dict.Add(KeyCode.D, Vector3.right);
        dict.Add(KeyCode.W, Vector3.up);
        dict.Add(KeyCode.S, -Vector3.up);
        if (((PS2 == Playerstates2.CastingCW) || (PS2 == Playerstates2.CastingCCW)) && (Ani_Count == 4))
        {   
            //ui이동이랑 상호작용 끄고 
            //터치받고
            //여전히 상태가ps 캐스팅이고 gs플레이이면 텔레포트(상태가 바꼈단건 ui위치에 터치했단거)
            //텔레포트하고나서 시간의흐름돌려놓고 아이콘색깔복구
        }
        if ((PS2 == Playerstates2.IdleCW) || (PS2 == Playerstates2.IdleCCW) || (PS2 == Playerstates2.MovingCW) || (PS2 == Playerstates2.MovingCCW))//
        {
            foreach (KeyValuePair<KeyCode, Vector3> entry in dict)
            { //키가 눌러졌고, 방향이 플레이어 기준 좌측 혹은 우측일 때
                if (Input.GetKey(entry.Key) && transform.up != entry.Value && transform.up != -entry.Value)
                {
                    if (transform.right == entry.Value)
                    {
                        if (PS2 != Playerstates2.MovingCW)
                        {
                            Ani_Count = 0;

                            MovingCW_On();
                            print("시계방향진행으로 상태변경");
                        }

                        //print(PS2);
                    }
                    else if (transform.right == -entry.Value)
                    {
                        if (PS2 != Playerstates2.MovingCCW)
                        {
                            MovingCCW_On();
                            Ani_Count = 0;
                            print("반시계방향진행으로 상태변경");
                        }
                    }
                    if (PS2 == Playerstates2.MovingCW)//문제의 여지있음
                    {
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, entry.Value, out hit, RotateRange, 1 << 8))
                        {
                            RotateCW_On();
                        }
                        else
                        {
                            //이동
                            transform.position = transform.position + entry.Value * Speed * Time.deltaTime;
                        }
                    }
                    else if (PS2 == Playerstates2.MovingCCW)
                    {
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, entry.Value, out hit, RotateRange, 1 << 8))
                        {
                            RotateCCW_On();
                        }
                        else
                        {
                            //이동
                            transform.position = transform.position + entry.Value * Speed * Time.deltaTime;
                        }
                    }

                }
                
                //플레이어의 발쪽으로 레이캐스트
            }
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) == false)
            {
                if (PS2 == Playerstates2.MovingCCW)
                {
                    PS2 = Playerstates2.IdleCCW;
                }
                else if (PS2 == Playerstates2.MovingCW)
                {
                    PS2 = Playerstates2.IdleCW;
                }
            }



        }
        
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
            StickToWall(rightHit.distance > leftHit.distance ? leftHit : rightHit);
        }
        //print(PS2);

        //print(PS2);
        _now_ani_time += Time.deltaTime;
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
            if (PS2 == Playerstates2.TeleportingCW)
            {
                TeleportingCW_Ing();
            }
            if (PS2 == Playerstates2.TeleportingCCW)
            {
                TeleportingCCW_Ing();
            }
            if (PS2 == Playerstates2.RotateCW)
            {
                RotateCW_Ing();
            }
            if (PS2 == Playerstates2.RotateCCW)
            {
                RotateCCW_Ing();
            }
            if (PS2 == Playerstates2.CastingCW)
            {
                CastingCW_Ing();
            }
            if (PS2 == Playerstates2.CastingCCW)
            {
                CastingCCW_Ing();
            }
            _now_ani_time -= Ani_Speed;
        }//애니재생
    }

    void StickToWall(RaycastHit hit)
    {
        //레이캐스트 정보를 바탕으로 벽에 붙음
        if (hit.collider != null)
        {
            Vector3 normal = hit.normal;
            Vector3 o = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, -transform.eulerAngles.z + Mathf.Rad2Deg * Mathf.Atan2(normal.y, normal.x) - 90);
            transform.position = hit.point;
            transform.Translate(Vector3.up * transform.localScale.y / 2);
        }
    }
    void MovingCW_On()
    {
        _now_ani_time = 0;
        Ani_Speed = 0.3f;
        Time.timeScale = 1;
        PS2 = Playerstates2.MovingCW;
        Ani_Count = 0;
    }
    void MovingCCW_On()
    {
        _now_ani_time = 0;
        Ani_Speed = 0.3f;
        Time.timeScale = 1;
        PS2 = Playerstates2.MovingCCW;
        Ani_Count = 0;
    }
    void CastingCW_On()
    {
        _now_ani_time = 0;
        Ani_Speed = 0.05f;
        Time.timeScale = 0.2f;
        PS2 = Playerstates2.CastingCW;
        Ani_Count = 0;
    }
    void CastingCCW_On()
    {
        _now_ani_time = 0;
        Ani_Speed = 0.05f;
        Time.timeScale = 0.2f;
        PS2 = Playerstates2.CastingCCW;
        Ani_Count = 0;
    }
    void TeleportingCW_On()
    {
        _now_ani_time = 0;
        PS2 = Playerstates2.TeleportingCW;
        Ani_Count = 0;
    }
    void TeleportingCCW_On()
    {
        _now_ani_time = 0;
        PS2 = Playerstates2.TeleportingCCW;
        Ani_Count = 0;
    }
    void RotateCW_On()
    {
        _now_ani_time = 0;
        Ani_Speed = 0.12f;
        Time.timeScale = 1;
        PS2 = Playerstates2.RotateCW;
       // transform.localScale += new Vector3(0.25f, 0, 0);
        Ani_Count = 0;
    }
    void RotateCCW_On()
    {
        _now_ani_time = 0;
        Ani_Speed = 0.12f;
        Time.timeScale = 1;
        print("변경");
        PS2 = Playerstates2.RotateCCW;
        //transform.localScale += new Vector3(0.25f, 0, 0);
        Ani_Count = 0;
    }
    void DeadCW_On()
    {
        _now_ani_time = 0;
        PS2 = Playerstates2.DeadCW;
        Ani_Count = 0;
    }
    void DeadCCW_On()
    {
        _now_ani_time = 0;
        PS2 = Playerstates2.DeadCCW;
        Ani_Count = 0;
    }
    void IdleCW_On()
    {
        _now_ani_time = 0;
        Ani_Speed = 0.6f;
        Time.timeScale = 1;
        PS2 = Playerstates2.IdleCW;
        Ani_Count = 0;
    }
    void IdleCCW_On()
    {
        _now_ani_time = 0;
        Ani_Speed = 0.6f;
        Time.timeScale = 1;
        PS2 = Playerstates2.IdleCCW;
        Ani_Count = 0;
    }
    void Clear_On()
    {
        PS2 = Playerstates2.Clear;
        Ani_Count = 0;
    }



    void MovingCW_Ing()
    {
        if (Ani_Count == 0)
        {
            transform.localScale = new Vector3(0.72f, 1.4f, 1f);
        }
        rend.material.mainTexture = MovingCW[Ani_Count];
        Ani_Count++;
        if (Ani_Count > 1)
        {
            Ani_Count %= 2;
        }
    }
    void MovingCCW_Ing()
    {
        if (Ani_Count == 0)
        {
            transform.localScale = new Vector3(0.72f, 1.4f, 1f);
        }
        rend.material.mainTexture = MovingCCW[Ani_Count];
        Ani_Count++;
        if (Ani_Count > 1)
        {
            Ani_Count %= 2;
        }
    }
    void CastingCW_Ing()
    {
        transform.localScale = new Vector3(1f, 1.4f, 1f);
        rend.material.mainTexture = CastingCW[Ani_Count];
        if (Ani_Count < 3)
        {
            Ani_Count++;
        }
    }
    void CastingCCW_Ing()
    {
        transform.localScale = new Vector3(1f, 1.4f, 1f);
        rend.material.mainTexture = CastingCCW[Ani_Count];
        if (Ani_Count < 3)
        {
            Ani_Count++;
        }
    }
    void TeleportingCW_Ing()
    {
        transform.localScale = new Vector3(1f, 1.4f, 1f);
        rend.material.mainTexture = TeleportCW[Ani_Count];
        Ani_Count++;
        if (Ani_Count == 10)
        {
            //이동
            Ani_Count++;
        }
        if (Ani_Count == 20)
        {
            Time.timeScale = 1;
            IdleCW_On();
        }
    }
    void TeleportingCCW_Ing()
    {
        transform.localScale = new Vector3(1f, 1.4f, 1f);
        rend.material.mainTexture = TeleportCCW[Ani_Count];
        Ani_Count++;
        if (Ani_Count == 10)
        {
            //이동
            Ani_Count++;
        }
        if (Ani_Count == 20)
        {
            Time.timeScale = 1;
            IdleCW_On();
        }
    }
    void DeadCW_Ing()
    {
        transform.localScale = new Vector3(0.72f, 1.4f, 1f);
        rend.material.mainTexture = DeathCW[Ani_Count];
        Ani_Count++;
    }
    void DeadCCW_Ing()
    {
        transform.localScale = new Vector3(0.72f, 1.4f, 1f);
        rend.material.mainTexture = DeathCCW[Ani_Count];
        Ani_Count++;
    }
    void IdleCW_Ing()
    {
        transform.localScale = new Vector3(0.72f, 1.4f, 1f);
        rend.material.mainTexture = IdleCW[Ani_Count];
        Ani_Count++;
        if (Ani_Count > 1)
        {
            Ani_Count %= 2;
        }
    }
    void IdleCCW_Ing()
    {
        transform.localScale = new Vector3(0.72f, 1.4f, 1f);
        rend.material.mainTexture = IdleCCW[Ani_Count];
        Ani_Count++;
        if (Ani_Count > 1)
        {
            Ani_Count %= 2;
        }
    }
    void RotateCW_Ing()
    {
        if (Ani_Count == 7)
        {

            RaycastHit Hit;
            Physics.Raycast(transform.position, transform.right, out Hit, 1, 1 << 8);
            StickToWall(Hit);
            //transform.localScale = new Vector3(0.72f, 1.4f, 1f);
            //transform.localScale += new Vector3(-0.25f, 0, 0);
            IdleCW_On();
        }
        if (Ani_Count == 0)
        {
            transform.localScale = new Vector3(1.32f, 1.4f, 1f);
        }
        rend.material.mainTexture = RotateCW[Ani_Count];
        Ani_Count++;
        
    }
    void RotateCCW_Ing()
    {
        if (Ani_Count == 7)
        {

            RaycastHit Hit;
            Physics.Raycast(transform.position, -transform.right, out Hit, 1, 1 << 8);
            StickToWall(Hit);
            //transform.localScale += new Vector3(-0.25f, 0, 0);
            IdleCCW_On();
        }
        if (Ani_Count == 0)
        {
            transform.localScale = new Vector3(1.32f, 1.4f, 1f);
        }
        rend.material.mainTexture = RotateCCW[Ani_Count];
        Ani_Count++;
        
    }
    void Clear_Ing()
    {

    }
    public void TeleportButton()
    {
        if((PS2==Playerstates2.IdleCW)||(PS2 == Playerstates2.MovingCW))
        {
            CastingCW_On();
        }
        else if ((PS2 == Playerstates2.IdleCCW) || (PS2 == Playerstates2.MovingCCW))
        {
            CastingCCW_On();
        }
    }
}
