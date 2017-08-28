using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GravitySwitch : MonoBehaviour {

    public GravityState State;
    public GameObject Base;

	// Use this for initialization
	void Start () {
        State = GravityState.N;
    }

    void OnCollisionStay(Collision c)
    {
        print(c.gameObject);
        if (true)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (transform.position - c.gameObject.transform.position) - new Vector3(0,0, (transform.position - c.gameObject.transform.position).z), out hit)) {

                if (hit.collider == Base.GetComponent<Collider>())
                {
                    print("Activated");
                    Gravity.State = State;
                }
            }
        }
    }
}
