using UnityEngine;
using System.Collections;

public class guiwire : MonoBehaviour {
    public GameObject Player;

    public void OnMouseUp()
    {
		float a;
        Move wire = (Move)Player.GetComponent("Move");
        wire.activewire = true;
    }

}
    