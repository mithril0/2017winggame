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
<<<<<<< HEAD:Assets/script/GravitySwitch.cs
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (transform.position - c.gameObject.transform.position) - new Vector3(0,0, (transform.position - c.gameObject.transform.position).z), out hit)) {

                if (hit.collider == Base.GetComponent<Collider>())
                {
                    print("Activated");
                    Global.State = State;
                }
            }
=======
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
>>>>>>> 3ef82b535d4c2ad8dce3f0ecba563b9cf53ce999:Assets/GravitySwitch.cs
        }
    }
}
