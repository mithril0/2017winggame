using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GravitySwitch : MonoBehaviour {

    public GravityState State;
    public GameObject HoldingBlock;
    public bool u = false;
    public bool Active = true;

    void OnTriggerStay(Collider c)
    {
        print(c.gameObject);

        print("Activated");
        
        if (c.gameObject.GetComponent("Gravity") != null && Active)
        {
            Gravity.State = State;
            HoldingBlock = c.gameObject;
            ((Behaviour)HoldingBlock.GetComponent("Gravity")).enabled = false;
            HoldingBlock.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }

    void Update()
    {
        if (HoldingBlock != null && Active)
        {
            HoldingBlock.transform.position = transform.position + new Vector3(0, 0, HoldingBlock.transform.position.z - transform.position.z);
        }

        if (u == true)
        {
            Release();
            u = false;
        }
    }

    void Release()
    {
        ((Behaviour)HoldingBlock.GetComponent("Gravity")).enabled = true;
        Active = false;
        
    }

    private void OnTriggerExit(Collider c)
    {
        if (!Active && c.gameObject == HoldingBlock) { 
            HoldingBlock = null;
            Active = true;
        }
    }
}
