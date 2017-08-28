using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GravityState
{
    N, E, S, W
}

public class Gravity : MonoBehaviour {
    public static GravityState State = GravityState.S;
    public static float Speed = 2;
	
	// Update is called once per frame
	void Update () {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 v;
        switch(State)
        {
            case GravityState.N:
                v = new Vector3(0, Speed, 0);
                break;
            case GravityState.E:
                v = new Vector3(-Speed, 0, 0);
                break;
            case GravityState.S:
                v = new Vector3(0, -Speed, 0);
                break;
            case GravityState.W:
                v = new Vector3(Speed, 0, 0);
                break;
            default:
                v = new Vector3(0, 0, 0);
                break;
        }
        //rb.AddForce(v, ForceMode.Force);
        rb.velocity = v;
    }
}
