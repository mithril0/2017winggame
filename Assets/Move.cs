using UnityEngine;
using System.Collections;

public enum PlayerState
{
    N, E, S, W
}

public class Move : MonoBehaviour
{
    public float side = 0.5f;
    public float StickRange;
    public float WallRange;
    public float MoveSpeed;
    Vector3 lookDirection;
    public PlayerState PS = PlayerState.N;

    public Vector3 movePos;
    public float wireRange;

    void Update()
    {
        if (PS == PlayerState.N)
        {
            RaycastHit hit1;
            Ray Ray1 = new Ray(transform.position, -Vector3.up);

            RaycastHit L1;
            Ray leftray1 = new Ray(transform.position, Vector3.left);
            RaycastHit R1;
            Ray rightray1 = new Ray(transform.position, Vector3.right);

            if (Physics.Raycast(Ray1, out hit1, StickRange, 1 << 8))
            {
                float hoverError1 = hit1.distance - transform.localScale.y;
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

                if (Physics.Raycast(leftray1, out L1, WallRange, 1 << 8))
                {
                    float LeftError1 = L1.distance - side;
                    if (LeftError1 < 0.05f)
                    {
                        transform.Rotate(0, 0, -90, Space.World);
                        transform.position += new Vector3(0.5f, -0.4f, 0);
                        PS = PlayerState.E;
                    }
                }
                if (Physics.Raycast(rightray1, out R1, WallRange, 1 << 8))
                {
                    float RightError1 = R1.distance - side;
                    if (RightError1 < 0.05f)
                    {
                        transform.Rotate(0, 0, 90, Space.World);
                        transform.position += new Vector3(-0.5f, -0.4f, 0);
                        PS = PlayerState.W;
                    }
                }

            }
            else
            {
                if (lookDirection == Vector3.left)
                {
                    transform.Rotate(0, 0, 90, Space.World);
                    transform.position += new Vector3(-1, -1.1f, 0);
                    PS = PlayerState.W;
                }
                if (lookDirection == Vector3.right)
                {
                    transform.Rotate(0, 0, -90, Space.World);
                    transform.position += new Vector3(1, -1.1f, 0);
                    PS = PlayerState.E;
                }
            }
        }

        if (PS == PlayerState.E)
        {
            RaycastHit hit2;
            Ray Ray2 = new Ray(transform.position, Vector3.left);

            RaycastHit U1;
            Ray upray1 = new Ray(transform.position, Vector3.up);
            RaycastHit D1;
            Ray downray1 = new Ray(transform.position, Vector3.down);

            if (Physics.Raycast(Ray2, out hit2, StickRange, 1 << 8))
            {
                float hoverError2 = hit2.distance - transform.localScale.y;
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

                if (Physics.Raycast(upray1, out U1, WallRange, 1 << 8))
                {
                    float UpError1 = U1.distance - side;
                    if (UpError1 < 0.05f)
                    {
                        transform.Rotate(0, 0, -90, Space.World);
                        transform.position += new Vector3(-0.4f, -0.5f, 0);
                        PS = PlayerState.S;
                    }
                }
                if (Physics.Raycast(downray1, out D1, WallRange, 1 << 8))
                {
                    float DownError1 = D1.distance - side;
                    if (DownError1 < 0.05f)
                    {
                        transform.Rotate(0, 0, 90, Space.World);
                        transform.position += new Vector3(-0.4f, 0.5f, 0);
                        PS = PlayerState.N;
                    }
                }

            }
            else
            {
                if (lookDirection == Vector3.left)
                {
                    transform.Rotate(0, 0, 90, Space.World);
                    transform.position += new Vector3(-1.1f, 1, 0);
                    PS = PlayerState.N;
                }
                if (lookDirection == Vector3.right)
                {
                    transform.Rotate(0, 0, -90, Space.World);
                    transform.position += new Vector3(-1.1f, -1, 0);
                    PS = PlayerState.S;
                }
            }
        }

        if (PS == PlayerState.S)
        {
            RaycastHit hit3;
            Ray Ray3 = new Ray(transform.position, Vector3.up);

            RaycastHit L2;
            Ray leftray2 = new Ray(transform.position, Vector3.left);
            RaycastHit R2;
            Ray rightray2 = new Ray(transform.position, Vector3.right);

            if (Physics.Raycast(Ray3, out hit3, StickRange, 1 << 8))
            {
                float hoverError3 = hit3.distance - transform.localScale.y;
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

                if (Physics.Raycast(leftray2, out L2, WallRange, 1 << 8))
                {
                    float LeftError2 = L2.distance - side;
                    if (LeftError2 < 0.05f)
                    {
                        transform.Rotate(0, 0, 90, Space.World);
                        transform.position += new Vector3(0.5f, 0.4f, 0);
                        PS = PlayerState.E;
                    }
                }
                if (Physics.Raycast(rightray2, out R2, WallRange, 1 << 8))
                {
                    float RightError2 = R2.distance - side;
                    if (RightError2 < 0.05f)
                    {
                        transform.Rotate(0, 0, -90, Space.World);
                        transform.position += new Vector3(-0.5f, 0.4f, 0);
                        PS = PlayerState.W;
                    }
                }

            }
            else
            {
                if (lookDirection == Vector3.left)
                {
                    transform.Rotate(0, 0, 90, Space.World);
                    transform.position += new Vector3(1, 1.1f, 0);
                    PS = PlayerState.E;
                }
                if (lookDirection == Vector3.right)
                {
                    transform.Rotate(0, 0, -90, Space.World);
                    transform.position += new Vector3(-1, 1.1f, 0);
                    PS = PlayerState.W;
                }
            }
        }

        if (PS == PlayerState.W)
        {
            RaycastHit hit4;
            Ray Ray4 = new Ray(transform.position, Vector3.right);

            RaycastHit U2;
            Ray upray2 = new Ray(transform.position, Vector3.up);
            RaycastHit D2;
            Ray downray2 = new Ray(transform.position, Vector3.down);

            if (Physics.Raycast(Ray4, out hit4, StickRange, 1 << 8))
            {
                float hoverError4 = hit4.distance - transform.localScale.y;
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

                if (Physics.Raycast(upray2, out U2, WallRange, 1 << 8))
                {
                    float UpError2 = U2.distance - side;
                    if (UpError2 < 0.05f)
                    {
                        transform.Rotate(0, 0, 90, Space.World);
                        transform.position += new Vector3(0.4f, -0.5f, 0);
                        PS = PlayerState.S;
                    }
                }
                if (Physics.Raycast(downray2, out D2, WallRange, 1 << 8))
                {
                    float DownError2 = D2.distance - side;
                    if (DownError2 < 0.05f)
                    {
                        transform.Rotate(0, 0, -90, Space.World);
                        transform.position += new Vector3(0.4f, 0.5f, 0);
                        PS = PlayerState.N;
                    }
                }

            }
            else
            {
                if (lookDirection == Vector3.left)
                {
                    transform.Rotate(0, 0, 90, Space.World);
                    transform.position += new Vector3(1.1f, -1, 0);
                    PS = PlayerState.S;
                }
                if (lookDirection == Vector3.right)
                {
                    transform.Rotate(0, 0, -90, Space.World);
                    transform.position += new Vector3(1.1f, 1, 0);
                    PS = PlayerState.N;
                }
            }
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 1 << 8))
            {
                movePos = new Vector3(hit.point.x, hit.point.y, transform.position.z);
            }
            Vector3 wireDirection = new Vector3(movePos.x - transform.position.x, movePos.y - transform.position.y, 0f);

            Ray wire1 = new Ray(transform.position, wireDirection);
            RaycastHit wHit1;
            Vector3 A = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
            Ray wire2 = new Ray(A, wireDirection);
            RaycastHit wHit2;
            Debug.DrawRay(transform.position, wireDirection * wireRange, Color.red);

            if ((Physics.Raycast(wire1, out wHit1, wireRange, 1 << 8)) &&
                (Physics.Raycast(wire2, out wHit2, wireRange, 1 << 8)))
            {

                if (wHit1.point.x == wHit2.point.x)
                {
                    if ((wHit1.point.x - transform.position.x) > 0)
                    {
                        if (PS == PlayerState.N)
                        {
                            transform.Rotate(0, 0, 90, Space.World);
                        }
                        if (PS == PlayerState.E)
                        {
                            transform.Rotate(0, 0, 180, Space.World);
                        }
                        if (PS == PlayerState.S)
                        {
                            transform.Rotate(0, 0, -90, Space.World);
                        }
                        transform.position = new Vector3(wHit1.point.x - 1f, wHit1.point.y, transform.position.z);
                        PS = PlayerState.W;
                    }
                    else if ((wHit1.point.x - transform.position.x) < 0)
                    {
                        if (PS == PlayerState.N)
                        {
                            transform.Rotate(0, 0, -90, Space.World);
                        }
                        if (PS == PlayerState.S)
                        {
                            transform.Rotate(0, 0, 90, Space.World);
                        }
                        if (PS == PlayerState.W)
                        {
                            transform.Rotate(0, 0, 180, Space.World);
                        }
                        transform.position = new Vector3(wHit1.point.x + 1f, wHit1.point.y, transform.position.z);
                        PS = PlayerState.E;
                    }
                }

                if (wHit1.point.y == wHit2.point.y)
                {
                    if ((wHit1.point.y - transform.position.y) > 0)
                    {
                        if (PS == PlayerState.N)
                        {
                            transform.Rotate(0, 0, 180, Space.World);
                        }
                        if (PS == PlayerState.E)
                        {
                            transform.Rotate(0, 0, -90, Space.World);
                        }
                        if (PS == PlayerState.W)
                        {
                            transform.Rotate(0, 0, 90, Space.World);
                        }
                        transform.position = new Vector3(wHit1.point.x, wHit1.point.y - 1f, transform.position.z);
                        PS = PlayerState.S;
                    }
                    else if ((wHit1.point.y - transform.position.y) < 0)
                    {
                        if (PS == PlayerState.E)
                        {
                            transform.Rotate(0, 0, 90, Space.World);
                        }
                        if (PS == PlayerState.S)
                        {
                            transform.Rotate(0, 0, 180, Space.World);
                        }
                        if (PS == PlayerState.W)
                        {
                            transform.Rotate(0, 0, -90, Space.World);
                        }
                        transform.position = new Vector3(wHit1.point.x, wHit1.point.y + 1f, transform.position.z);
                        PS = PlayerState.N;
                    }
                }
            }
        }
    }
}