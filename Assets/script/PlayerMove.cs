using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public float StickRange;
    public float Speed;

	// Update is called once per frame
	void Update () {

        //이동키에 방향 배정
        Dictionary<KeyCode, Vector3> dict = new Dictionary<KeyCode, Vector3>(4);
        dict.Add(KeyCode.A, Vector3.left);
        dict.Add(KeyCode.D, Vector3.right);
        dict.Add(KeyCode.W, Vector3.up);
        dict.Add(KeyCode.S, -Vector3.up);

        foreach (KeyValuePair<KeyCode, Vector3> entry in dict)
        {
            //키가 눌러졌고, 방향이 플레이어 기준 좌측 혹은 우측일 때
            if (Input.GetKey(entry.Key) && transform.up != entry.Value && transform.up != -entry.Value)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, entry.Value, out hit, StickRange, 1 << 8))
                {
                    //가려는 방향에 벽이 있으면 그 벽에 붙음
                    StickToWall(hit);
                }
                else
                {
                    //이동
                    transform.position = transform.position + entry.Value * Speed * Time.deltaTime;
                }
            }
        }

        //플레이어의 발쪽으로 레이캐스트
        RaycastHit groundHit;
        if (Physics.Raycast(transform.position, -transform.up, out groundHit, StickRange, 1 << 8))
        {
            StickToWall(groundHit);
        } else
        {
            //바닥이 없으면 우측, 좌측으로 레이캐스트 하고 거리가 짧은 쪽 벽에 붙음
            RaycastHit rightHit;
            RaycastHit leftHit;

            if (!Physics.Raycast(transform.position - transform.up * (transform.localScale.y / 2 + 0.2f), transform.right, out rightHit, StickRange, 1 << 8))
            {
                rightHit.distance = Mathf.Infinity;
            }
            if (!Physics.Raycast(transform.position - transform.up * (transform.localScale.y / 2 + 0.2f), -transform.right, out leftHit, StickRange, 1 << 8))
            {
                leftHit.distance = Mathf.Infinity;
            }
            StickToWall(rightHit.distance > leftHit.distance? leftHit : rightHit);
        }

        
    }

    void StickToWall(RaycastHit hit)
    {
        //레이캐스트 정보를 바탕으로 벽에 붙음
        if (hit.collider != null) {
            Vector3 normal = hit.normal;
            Vector3 o = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            transform.eulerAngles = transform.eulerAngles + new Vector3(0,0, - transform.eulerAngles.z + Mathf.Rad2Deg * Mathf.Atan2(normal.y, normal.x)-90);
            transform.position = hit.point;
            transform.Translate(Vector3.up*transform.localScale.y/2);
        }
    }
}
