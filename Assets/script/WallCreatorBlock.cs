using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreatorBlock : MonoBehaviour {
    public GameObject Wall;
    public bool Activated = false;

    void OnTriggerStay(Collider c)
    {
        if (Input.GetKeyDown(KeyCode.Z) && c.gameObject.tag == "Player")
        {
            Wall.SetActive(true);
            Activated = true;
        }
    }
}
