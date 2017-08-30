using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Global
{
    public static float acceltime=1;
    public static float time()
    {
        return Time.deltaTime * acceltime;
    }
    static float ptu(float pixel)// translate unit pixel to unit
    {
        return pixel * 0.01f;
    }
    static float utp(float unit)
    {
        return unit * 100;
    }
    public static bool Activated;
    public static GravityState State = GravityState.S;
    public static int IsGravitychanged = 0;
    public static GravityState LastState = GravityState.S;
}
public enum GravityState
{
    N, E, S, W
}

public class Gravity : MonoBehaviour {
    
    public static float Speed = 2;
	
	// Update is called once per frame
	void Update () {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 v;
        switch(Global.State)
        {
            case GravityState.N:
                v = new Vector3(0, Speed, 0);
                break;
            case GravityState.E:
                v = new Vector3(Speed, 0, 0);
                break;
            case GravityState.S:
                v = new Vector3(0, -Speed, 0);
                break;
            case GravityState.W:
                v = new Vector3(-Speed, 0, 0);
                break;
            default:
                v = new Vector3(0, 0, 0);
                break;
        }
        //rb.AddForce(v, ForceMode.Force);
        rb.velocity = v;
     
    }
}
