using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public float StickRange;
    public float Speed;

	// Update is called once per frame
	void Update () {

        Dictionary<KeyCode, Vector3> dict = new Dictionary<KeyCode, Vector3>(4);
        dict.Add(KeyCode.A, Vector3.left);
        dict.Add(KeyCode.D, Vector3.right);
        dict.Add(KeyCode.W, Vector3.up);
        dict.Add(KeyCode.S, -Vector3.up);

        foreach (KeyValuePair<KeyCode, Vector3> entry in dict)
        {
            if (Input.GetKey(entry.Key) && transform.up != entry.Value && transform.up != -entry.Value)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, entry.Value, out hit, StickRange))
                {
                    StickToWall(hit);
                }
                else
                {
                    transform.position = transform.position + entry.Value * Speed;
                }
            }
        }

        RaycastHit groundHit;
        if (Physics.Raycast(transform.position, -transform.up, out groundHit, StickRange))
        {
            StickToWall(groundHit);
        } else
        {
            RaycastHit rightHit;
            RaycastHit leftHit;

            if (!Physics.Raycast(transform.position - transform.up * (transform.localScale.y / 2 + 0.2f), transform.right, out rightHit, StickRange))
            {
                rightHit.distance = Mathf.Infinity;
            }
            if (!Physics.Raycast(transform.position - transform.up * (transform.localScale.y / 2 + 0.2f), -transform.right, out leftHit, StickRange))
            {
                leftHit.distance = Mathf.Infinity;
            }
            StickToWall(rightHit.distance > leftHit.distance? leftHit : rightHit);
        }

        
    }

    void StickToWall(RaycastHit hit)
    {
        if (hit.collider != null) {
            Vector3 normal = hit.normal;
            Vector3 o = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            transform.eulerAngles = transform.eulerAngles + new Vector3(0,0, - transform.eulerAngles.z + Mathf.Rad2Deg * Mathf.Atan2(normal.y, normal.x)-90);
            transform.position = hit.point;
            transform.Translate(Vector3.up*transform.localScale.y/2);
        }
    }
}
