using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newparticle : MonoBehaviour {
    public int MapWide, MapHeight;
    public float elapseTime=0;
    const float timeunit=0.25f;//1칸전진하는데 필요한 시간
    public int movepixel = 0;
    public int phase = 1;
    public float particleSpeed = 0.01f;
    public Vector3 v,temp;
    private SpriteRenderer spriteRenderer;
    public Sprite particleSprite;
    // Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        }
        elapseTime += Time.deltaTime;
        while (elapseTime > timeunit) {
            elapseTime -= timeunit;
            movepixel++;
        }
        phase += movepixel;
        if (Global.IsGravitychanged == 1)
        {
            if (phase > 9)
            {
                phase = 1;
                Global.IsGravitychanged = 0;
            }
        }
        if (Global.IsGravitychanged == 0)
        {
            phase = (phase % 10);
            spriteRenderer.sprite = Resources.Load<Sprite>("texture/"+Global.State.ToString() + Global.State.ToString() + phase.ToString());
        }
        else
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("texture/"+Global.LastState.ToString() + Global.State.ToString() + phase.ToString());
        }
        transform.position += (v * movepixel);/* +유저변위/2 */
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
            temp.y -= (MapHeight + 2);
        }//아래로 벗어나면 보정해준다
        transform.position = temp;
	}
}
