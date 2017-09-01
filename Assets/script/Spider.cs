using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SpiderState
{
    IdleCW,
    IdleCCW,
    MovingCW,
    MovingCCW,
    DeadCW,
    DeadCCW,
    StopCW,
    StopCCW
}
public class Spider : MonoBehaviour {
    public SpiderState SS;
    public Renderer rend;
    public Shader shader;
    public Texture[] IdleCW = new Texture[2];
    public Texture[] IdleCCW = new Texture[2];
    public Texture[] MovingCW = new Texture[10];
    public Texture[] MovingCCW = new Texture[10];
    public Texture[] DeadCW = new Texture[8];
    public Texture[] DeadCCW = new Texture[8];
    public Texture[] StopCW = new Texture[4];
    public Texture[] StopCCW = new Texture[4];
    public Vector3 Direction;
    public float StickRange;
    public float Speed;
    public float Scale;//얼마나 앞에 와이어쏠지 실직적으로 거미의 너비와 동일해야한다
    public float Ani_Speed, _now_ani_time;
    public int Ani_Count, i;
    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        shader = Shader.Find("Unlit/Transparent");
        rend.material.shader = shader;
        for (i = 0; i < 2; i++)
        {
            IdleCW[i] = Resources.Load("Spider/CW" + i.ToString(), typeof(Texture)) as Texture;
        }
        for (i = 0; i < 2; i++)
        {
            IdleCCW[i] = Resources.Load("Spider/CCW" + i.ToString(), typeof(Texture)) as Texture;
        }
        for (i = 0; i < 10; i++)
        {
            MovingCW[i] = Resources.Load("Spider/CW" + (i+1).ToString(), typeof(Texture)) as Texture;
        }
        for (i = 0; i < 10; i++)
        {
            MovingCCW[i] = Resources.Load("Spider/CCW" + (i+1).ToString(), typeof(Texture)) as Texture;
        }
        for (i = 0; i < 8; i++)
        {
            DeadCW[i] = Resources.Load("Spider/DCW" + i.ToString(), typeof(Texture)) as Texture;
        }
        for (i = 0; i < 8; i++)
        {
            DeadCCW[i] = Resources.Load("Spider/DCCW" + i.ToString(), typeof(Texture)) as Texture;
        }
        for (i = 0; i < 4; i++)
        {
            StopCW[i] = Resources.Load("Spider/CW" + ((i+11)%12).ToString(), typeof(Texture)) as Texture;
        }
        for (i = 0; i < 4; i++)
        {
            StopCCW[i] = Resources.Load("Spider/CCW" + ((i + 11) % 12).ToString(), typeof(Texture)) as Texture;
        }
    }//초기화
	
	// Update is called once per frame
	void Update () {
        if (_now_ani_time >= Ani_Speed)
        {
            if (SS == SpiderState.IdleCW)
            {
                IdleCW_Ing();
            }
            if (SS == SpiderState.IdleCCW)
            {
                IdleCCW_Ing();
            }
            if (SS == SpiderState.MovingCW)
            {
                MovingCW_Ing();
                print("한다 나는 갱신");
            }
            if (SS == SpiderState.MovingCCW)
            {
                MovingCCW_Ing();
                print("한다 나는 갱신2");
            }
            if (SS == SpiderState.DeadCW)
            {
                DeadCW_Ing();
            }
            if(SS == SpiderState.DeadCCW)
            {
                DeadCCW_Ing();
            }
            if(SS == SpiderState.StopCW)
            {
                StopCW_Ing();
            }
            if(SS == SpiderState.StopCCW)
            {
                StopCCW_Ing();
            }
            _now_ani_time -= Ani_Speed;
        }//애니재생
        else
        {
            _now_ani_time += Time.deltaTime;
        }
        RaycastHit forwardHit;
        RaycastHit groundHit;
        RaycastHit hit;
        if ((Physics.Raycast(transform.position + (Direction * Scale), -transform.up, out forwardHit, StickRange, 1 << 8)!=true)|| (Physics.Raycast(transform.position, Direction, out hit, Scale, 1 << 8)))
        {
            print("장비를 정지합니다");
            if (SS == SpiderState.MovingCW&&Ani_Count==4)
            {
                print("방향변경");
                StopCW_On();
            }
            else if (SS == SpiderState.MovingCCW && Ani_Count == 4)
            {
                StopCCW_On();
            }
        }//바닥이없거나 앞에벽이있으면 멈추고 반대로감
        if((Physics.Raycast(transform.position + (Direction * Scale), -transform.up, out forwardHit, StickRange, 1 << 8) == true)&&((Physics.Raycast(transform.position, Direction, out hit, Scale, 1 << 8))!=true))
        {
            Physics.Raycast(transform.position, -transform.up, out groundHit, StickRange, 1 << 8);
            StickToWall(groundHit);
            transform.position = (transform.position + Direction * Speed * Time.deltaTime);
        } //그외의경우 바닥부착
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
    }//벽에붙는함수
    void MovingCW_On()
    {
        SS = SpiderState.MovingCW;
        Direction = new Vector3(1,0,0);
        Ani_Count = 0;
        
    }
    void MovingCCW_On()
    {
        Direction = new Vector3(-1, 0, 0);
        SS = SpiderState.MovingCCW;
        Ani_Count = 0;
        print("가동");
    }
    void DeadCW_On()
    {
        SS = SpiderState.DeadCW;
    }
    void IdleCW_On()
    {
        SS = SpiderState.IdleCW;
        Ani_Count = 0;
    }
    void IdleCCW_On()
    {
        SS = SpiderState.IdleCCW;
        Ani_Count = 0;
    }
    void StopCW_On()
    {
        Ani_Count = 0;
        SS = SpiderState.StopCW;
    }
    void StopCCW_On()
    {
        Ani_Count = 0;
        SS = SpiderState.StopCCW;
    }
    void MovingCW_Ing()
    {
        rend.material.mainTexture = MovingCW[Ani_Count];
        Ani_Count++;
       // print("카운트");
        if (Ani_Count > 9)
        {
            Ani_Count -= 6;
            print(Ani_Count);
        }
    }
    void MovingCCW_Ing()
    {
        rend.material.mainTexture = MovingCCW[Ani_Count];
        Ani_Count++;
        if (Ani_Count > 9)
        {
            Ani_Count -= 6;
        }
    }
    void DeadCW_Ing()
    {
        rend.material.mainTexture = DeadCW[Ani_Count];
        Ani_Count++;
        if (Ani_Count > 7)
        {
            Ani_Count %= 8;
        }
    }
    void DeadCCW_Ing()
    {
        rend.material.mainTexture = DeadCCW[Ani_Count];
        Ani_Count++;
        if (Ani_Count > 7)
        {
            Ani_Count %= 8;
        }
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
    void StopCW_Ing()
    {
        rend.material.mainTexture = StopCW[Ani_Count];
        Ani_Count++;
        if (Ani_Count > 3)
        {
            MovingCCW_On();
        }
    }
    void StopCCW_Ing()
    {
        rend.material.mainTexture = StopCCW[Ani_Count];
        Ani_Count++;
        if (Ani_Count > 3)
        {
            MovingCW_On();
        }
    }
    void Reverse()
    {
        if(SS == SpiderState.MovingCW)
        {
            MovingCCW_On();
        }
        else if(SS == SpiderState.MovingCCW)
        {
            MovingCW_On();
        }
    }
    
}
