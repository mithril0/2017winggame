using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    public GameObject Player;
    Vector3 Camera;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Camera = Player.transform.position;
        Camera.z = -10;
        transform.position = Camera;
	}
}
