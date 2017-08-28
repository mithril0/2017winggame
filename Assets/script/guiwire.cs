using UnityEngine;
using System.Collections;

public class guiwire : MonoBehaviour {
    public GameObject Player;

    public void OnMouseUp()
    {
		float a;
        Moveplayer wire = (Moveplayer)Player.GetComponent("Moveplayer");
        wire.activewire = true;
    }

}
    