using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
    public Renderer rend;
    public Texture Inactive_Image;
    public Shader shader;
    // Use this for initialization
    void Start() {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        Inactive_Image = Resources.Load("DOOR/DOOR01", typeof(Texture)) as Texture;
        shader = Shader.Find("Unlit/Transparent");
        rend.material.shader = shader;
    }
	// Update is called once per frame
	void Update () {
        
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            print("난 완전해졌다");
            rend.material.mainTexture = Inactive_Image;
            //활성화 애니메이션 출력
        }
    }
}
