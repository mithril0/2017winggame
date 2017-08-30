using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreatorBlock : MonoBehaviour {
    public GameObject Wall;
    public bool Activated = false;

    void OnTriggerEnter(Collider c)
    {    
        if (c.gameObject.tag == "Player")
        {
            //활성화 애니메이션 출력
            if (Global.Activated == true)
            {
                Wall.SetActive(true);
                Activated = true;
            }
        }
    }
}
