using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newparticle : MonoBehaviour {
    GravityState number_to_direction(int num)
    {
        switch (num)
        {
            case 0:
                return GravityState.N;
            case 1:
                return GravityState.S;
            case 2:
                return GravityState.N;
            case 3:
                return GravityState.E;
        }
        return GravityState.E;
    }
    public int direction, lastdirection;
    public int direction_to_number(GravityState state)
    {
        switch (state)
        {
            case GravityState.N:
                return 0;
            case GravityState.E:
                return 3;
            case GravityState.S:
                return 1;
            case GravityState.W:
                return 2;
        }
        return -1;
    }
    public int lastdirection_to_number(GravityState state)
        {
            switch (state)
            {
                case GravityState.N:
                    return 0;
                case GravityState.E:
                    return 3;
                case GravityState.S:
                    return 1;
                case GravityState.W:
                    return 2;
            }
        return -1;
    }
    string[] index ={"N","S","W","E"};
    public int MapWide, MapHeight;
    public float elapseTime=0;
    private int j, k, l;
    const float timeunit=0.25f;//1칸전진하는데 필요한 시간
    public int movepixel = 0;
    public int phase = 1;
    public float particleSpeed = 0.01f;
    public Vector3 v,temp;
    private SpriteRenderer spriteRenderer;
    public Sprite[,,]particleSprite= new Sprite[4,4,11];
    public Renderer rend;
    //public Sprite currentSprite;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        for (j = 0; j < 4; j++)
        {
            for (k = 0; k < 4; k++)
            {
                for (l = 0; l <= 9; l++)
                {
                    particleSprite[j, k, l] = Resources.Load<Sprite>("Sprites/" + index[j] + index[k] + l.ToString());
                    //print(particleSprite[j, k, l]);
                    //print("texture/" + index[j] + index[k] + l.ToString());
                }
            }
        }
    }
    // Update is called once per frame
    void Update() {
        switch (Global.State)
        {
            case GravityState.N:
                v = new Vector3(0, particleSpeed, 0);
                break;
            case GravityState.E:
                v = new Vector3(-particleSpeed, 0, 0);
                break;
            case GravityState.S:
                v = new Vector3(0, -particleSpeed, 0);
                break;
            case GravityState.W:
                v = new Vector3(particleSpeed, 0, 0);
                break;
            default:
                v = new Vector3(0, 0, 0);
                break;
        }//v값설정
        elapseTime += Time.deltaTime;
        while (elapseTime > timeunit) {
            elapseTime -= timeunit;
            movepixel++;
        }
        //spriteRenderer.sprite = Resources.Load<Sprite>("texture/SS1");
        phase += movepixel;
        if (Global.IsGravitychanged == 1)
        {
            if (phase > 8)
            {
                phase = 0;
                Global.IsGravitychanged = 0;
            }
        }
        if (Global.IsGravitychanged == 0)
        {
            phase = (phase % 10);
            spriteRenderer.sprite = particleSprite[direction_to_number(Global.State), direction_to_number(Global.State), phase];

        }
            //print(direction_to_number(Global.State).ToString() + " " + direction_to_number(Global.State).ToString() + " " + phase.ToString());
        else
        {
            spriteRenderer.sprite = particleSprite[direction_to_number(Global.State), direction_to_number(Global.State), phase];
        }
        //print("texture/" + Global.State.ToString() + Global.State.ToString() + phase.ToString());
        transform.position += (v * movepixel);
        movepixel = 0;
        temp = transform.position;
        if (temp.x > (MapWide + 3))
        {
            temp.x -= (MapWide + 2);
        }//맵오른쪽으로 벗어나거나
        if(temp.x < -3)
        {
            temp.x += (MapWide + 2);
        }//왼쪽으로벗어나거나
        if(temp.y > (MapHeight + 3))
        {
            temp.x -= (MapHeight + 2);
        }//위로벗어나거나
        if(temp.y < -3)
        {
            temp.y += (MapHeight + 2);
        }//아래로 벗어나면 보정해준다
        transform.position = temp;
        print(temp.ToString());
	}
}
