using UnityEngine;
using System.Collections;

public enum PlayerStates
{
    N, E, S, W
}

public class Moveplayer : MonoBehaviour
{
    public float height;                        // 캡슐 size는 (x,y,z) 1:1:1 비율로 정의됨. (1,1,1) 이라면 height = 1
    public bool activewire = false;
    public float side;                          // height의 1/2. (1,1,1) 이라면 side = 0.5
    public float StickRange;                    // 바닥 감지 레이저 범위. 블록 간 낙차(절벽 높이(ㄱ))보다 작아야함.
    public float WallRange;                     // 벽 감지 레이저 범위
    public float MoveSpeed;                     /* 이동속도. 너무 빠르면 우발적인 버그 발생. 레이저 검출 후 범위 확인하기 전에 프레임이 급하게 넘어가버리면 그런듯. 20 때는 버그 자주 발생. 10에서 안정.
                                                   그 외의 속도는 wasd 키 난타해보면서 테스트 해보면 확인가능. 갑자기 공중 비보잉만 안하면 괜찮음. */
    public float Approach = 0.05f;              // 벽 감지해서 (감지거리-side) 값이 이 값보다 작으면 회전 후 부착
    public float Revision = 0.1f;               // 회전후 부착할 때 위치 보정값. 회전후 원래붙어있던 면에 옆면이 딱 붙지 않도록 하는 역할. 반드시 Approach보다 커야함.  
    public float time()
    {
        if (activewire == true)
        {
            return Time.deltaTime / 100;
        }
        else
        {
            return Time.deltaTime;
        }
    }
    Vector3 lookDirection;
    public PlayerStates PS = PlayerStates.N;
    public Vector3 movePos;
    public float wireRange;

    public GameObject debugSphere;

    void Update()
    {
        if (activewire == false)
        {
            if (PS == PlayerStates.N)
            {
                RaycastHit hit1;
                Ray Ray1 = new Ray(transform.position, -Vector3.up);

                RaycastHit L1;
                Ray leftray1 = new Ray(transform.position, Vector3.left);
                RaycastHit R1;
                Ray rightray1 = new Ray(transform.position, Vector3.right);

                if (Physics.Raycast(Ray1, out hit1, StickRange))
                {
                    float hoverError1 = hit1.distance - height;
                    if (hoverError1 != 0)
                    {
                        transform.position -= new Vector3(0, hoverError1, 0);
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
                        lookDirection = Vector3.left;
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
                        lookDirection = Vector3.right;
                    }

                    if (Physics.Raycast(leftray1, out L1, WallRange))
                    {
                        float LeftError1 = L1.distance - side;
                        if (LeftError1 < Approach)              // Approach가 Revision 보다 크면 회전 후 부착하고 원래 붙어있던 면이 다시 감지되서 코너 사이에서 무한히 왔다갔다거림.
                        {                                       // (윗줄 주석 이어서) Approach가 Revision 보다 작아야하는 이유.  
                            transform.Rotate(0, 0, -90, Space.World);
                            transform.position += new Vector3(side, -side + Revision, 0);     // 이 부분 캡슐사이즈 바뀌어도 작동하도록 변수 도입.
                            PS = PlayerStates.E;
                        }
                    }
                    if (Physics.Raycast(rightray1, out R1, WallRange))
                    {
                        float RightError1 = R1.distance - side;
                        if (RightError1 < Approach)
                        {
                            transform.Rotate(0, 0, 90, Space.World);
                            transform.position += new Vector3(-side, -side + Revision, 0);    // 이 부분 캡슐사이즈 바뀌어도 작동하도록 변수 도입.
                            PS = PlayerStates.W;                                              // 이하 다른 모드 부분 수정사항 같음.
                        }
                    }

                }
                else
                {
                    if (lookDirection == Vector3.left)
                    {
                        transform.Rotate(0, 0, 90, Space.World);
                        transform.position += new Vector3(-height, -height - Revision, 0);
                        PS = PlayerStates.W;
                    }
                    if (lookDirection == Vector3.right)
                    {
                        transform.Rotate(0, 0, -90, Space.World);
                        transform.position += new Vector3(height, -height - Revision, 0);
                        PS = PlayerStates.E;
                    }
                }
            }

            if (PS == PlayerStates.E)
            {
                RaycastHit hit2;
                Ray Ray2 = new Ray(transform.position, Vector3.left);

                RaycastHit U1;
                Ray upray1 = new Ray(transform.position, Vector3.up);
                RaycastHit D1;
                Ray downray1 = new Ray(transform.position, Vector3.down);

                if (Physics.Raycast(Ray2, out hit2, StickRange))
                {
                    float hoverError2 = hit2.distance - height;
                    if (hoverError2 != 0)
                    {
                        transform.position -= new Vector3(hoverError2, 0, 0);
                    }

                    if (Input.GetKey(KeyCode.W))
                    {
                        transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
                        lookDirection = Vector3.left;
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
                        lookDirection = Vector3.right;
                    }

                    if (Physics.Raycast(upray1, out U1, WallRange))
                    {
                        float UpError1 = U1.distance - side;
                        if (UpError1 < Approach)
                        {
                            transform.Rotate(0, 0, -90, Space.World);
                            transform.position += new Vector3(-side + Revision, -side, 0);
                            PS = PlayerStates.S;
                        }
                    }
                    if (Physics.Raycast(downray1, out D1, WallRange))
                    {
                        float DownError1 = D1.distance - side;
                        if (DownError1 < Approach)
                        {
                            transform.Rotate(0, 0, 90, Space.World);
                            transform.position += new Vector3(-side + Revision, side, 0);
                            PS = PlayerStates.N;
                        }
                    }

                }
                else
                {
                    if (lookDirection == Vector3.left)
                    {
                        transform.Rotate(0, 0, 90, Space.World);
                        transform.position += new Vector3(-height - Revision, height, 0);
                        PS = PlayerStates.N;
                    }
                    if (lookDirection == Vector3.right)
                    {
                        transform.Rotate(0, 0, -90, Space.World);
                        transform.position += new Vector3(-height - Revision, -height, 0);
                        PS = PlayerStates.S;
                    }
                }
            }

            if (PS == PlayerStates.S)
            {
                RaycastHit hit3;
                Ray Ray3 = new Ray(transform.position, Vector3.up);

                RaycastHit L2;
                Ray leftray2 = new Ray(transform.position, Vector3.left);
                RaycastHit R2;
                Ray rightray2 = new Ray(transform.position, Vector3.right);

                if (Physics.Raycast(Ray3, out hit3, StickRange))
                {
                    float hoverError3 = hit3.distance - height;
                    if (hoverError3 != 0)
                    {
                        transform.position += new Vector3(0, hoverError3, 0);
                    }

                    if (Input.GetKey(KeyCode.D))
                    {
                        transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
                        lookDirection = Vector3.left;
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
                        lookDirection = Vector3.right;
                    }

                    if (Physics.Raycast(leftray2, out L2, WallRange))
                    {
                        float LeftError2 = L2.distance - side;
                        if (LeftError2 < Approach)
                        {
                            transform.Rotate(0, 0, 90, Space.World);
                            transform.position += new Vector3(side, side - Revision, 0);
                            PS = PlayerStates.E;
                        }
                    }
                    if (Physics.Raycast(rightray2, out R2, WallRange))
                    {
                        float RightError2 = R2.distance - side;
                        if (RightError2 < Approach)
                        {
                            transform.Rotate(0, 0, -90, Space.World);
                            transform.position += new Vector3(-side, side - Revision, 0);
                            PS = PlayerStates.W;
                        }
                    }

                }
                else
                {
                    if (lookDirection == Vector3.left)
                    {
                        transform.Rotate(0, 0, 90, Space.World);
                        transform.position += new Vector3(height, height + Revision, 0);
                        PS = PlayerStates.E;
                    }
                    if (lookDirection == Vector3.right)
                    {
                        transform.Rotate(0, 0, -90, Space.World);
                        transform.position += new Vector3(-height, height + Revision, 0);
                        PS = PlayerStates.W;
                    }
                }
            }

            if (PS == PlayerStates.W)
            {
                RaycastHit hit4;
                Ray Ray4 = new Ray(transform.position, Vector3.right);

                RaycastHit U2;
                Ray upray2 = new Ray(transform.position, Vector3.up);
                RaycastHit D2;
                Ray downray2 = new Ray(transform.position, Vector3.down);

                if (Physics.Raycast(Ray4, out hit4, StickRange))
                {
                    float hoverError4 = hit4.distance - height;
                    if (hoverError4 != 0)
                    {
                        transform.position += new Vector3(hoverError4, 0, 0);
                    }

                    if (Input.GetKey(KeyCode.S))
                    {
                        transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
                        lookDirection = Vector3.left;
                    }
                    if (Input.GetKey(KeyCode.W))
                    {
                        transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
                        lookDirection = Vector3.right;
                    }

                    if (Physics.Raycast(upray2, out U2, WallRange))
                    {
                        float UpError2 = U2.distance - side;
                        if (UpError2 < Approach)
                        {
                            transform.Rotate(0, 0, 90, Space.World);
                            transform.position += new Vector3(side - Revision, -side, 0);
                            PS = PlayerStates.S;
                        }
                    }
                    if (Physics.Raycast(downray2, out D2, WallRange))
                    {
                        float DownError2 = D2.distance - side;
                        if (DownError2 < Approach)
                        {
                            transform.Rotate(0, 0, -90, Space.World);
                            transform.position += new Vector3(side - Revision, side, 0);
                            PS = PlayerStates.N;
                        }
                    }

                }
                else
                {
                    if (lookDirection == Vector3.left)
                    {
                        transform.Rotate(0, 0, 90, Space.World);
                        transform.position += new Vector3(height + Revision, -height, 0);
                        PS = PlayerStates.S;
                    }
                    if (lookDirection == Vector3.right)
                    {
                        transform.Rotate(0, 0, -90, Space.World);
                        transform.position += new Vector3(height + Revision, height, 0);
                        PS = PlayerStates.N;
                    }
                }
            }
        }


        if (activewire == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    movePos = new Vector3(hit.point.x, hit.point.y, transform.position.z);
                    debugSphere.transform.position = movePos;
                }
                Vector3 wireDirection = new Vector3(movePos.x - transform.position.x, movePos.y - transform.position.y, 0f);

                Ray wire1 = new Ray(transform.position, wireDirection);
                RaycastHit wHit1;
                Vector3 A = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
                Ray wire2 = new Ray(A, wireDirection);
                RaycastHit wHit2;
                Debug.DrawRay(transform.position, wireDirection * wireRange, Color.red);

                if ((Physics.Raycast(wire1, out wHit1, wireRange)) &&
                    (Physics.Raycast(wire2, out wHit2, wireRange)))
                {

                    if (wHit1.point.x == wHit2.point.x)
                    {
                        if ((wHit1.point.x - transform.position.x) > 0)
                        {
                            if (PS == PlayerStates.N)
                            {
                                transform.Rotate(0, 0, 90, Space.World);
                            }
                            if (PS == PlayerStates.E)
                            {
                                transform.Rotate(0, 0, 180, Space.World);
                            }
                            if (PS == PlayerStates.S)
                            {
                                transform.Rotate(0, 0, -90, Space.World);
                            }
                            transform.position = new Vector3(wHit1.point.x - height, wHit1.point.y, 0);
                            PS = PlayerStates.W;
                        }
                        else if ((wHit1.point.x - transform.position.x) < 0)
                        {
                            if (PS == PlayerStates.N)
                            {
                                transform.Rotate(0, 0, -90, Space.World);
                            }
                            if (PS == PlayerStates.S)
                            {
                                transform.Rotate(0, 0, 90, Space.World);
                            }
                            if (PS == PlayerStates.W)
                            {
                                transform.Rotate(0, 0, 180, Space.World);
                            }
                            transform.position = new Vector3(wHit1.point.x + height, wHit1.point.y, 0);
                            PS = PlayerStates.E;
                        }
                    }

                    if (wHit1.point.y == wHit2.point.y)
                    {
                        if ((wHit1.point.y - transform.position.y) > 0)
                        {
                            if (PS == PlayerStates.N)
                            {
                                transform.Rotate(0, 0, 180, Space.World);
                            }
                            if (PS == PlayerStates.E)
                            {
                                transform.Rotate(0, 0, -90, Space.World);
                            }
                            if (PS == PlayerStates.W)
                            {
                                transform.Rotate(0, 0, 90, Space.World);
                            }
                            transform.position = new Vector3(wHit1.point.x, wHit1.point.y - height, 0);
                            PS = PlayerStates.S;
                        }
                        else if ((wHit1.point.y - transform.position.y) < 0)
                        {
                            if (PS == PlayerStates.E)
                            {
                                transform.Rotate(0, 0, 90, Space.World);
                            }
                            if (PS == PlayerStates.S)
                            {
                                transform.Rotate(0, 0, 180, Space.World);
                            }
                            if (PS == PlayerStates.W)
                            {
                                transform.Rotate(0, 0, -90, Space.World);
                            }
                            transform.position = new Vector3(wHit1.point.x, wHit1.point.y + height, 0);
                            PS = PlayerStates.N;
                        }
                    }
                }
                activewire = false;
            }

        }
    }
}

