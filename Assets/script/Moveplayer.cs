using UnityEngine;
using System.Collections;

public enum PlayerStates
{
	N, E, S, W
}

public class Moveplayer : MonoBehaviour {
	public float height = 1.0f;
	public bool activewire=false;
	public float side = 0.5f;
	public float StickRange;
	public float WallRange;
	public float MoveSpeed;
    public float time()
    {
        if (activewire == true) {
						return Time.deltaTime / 100;
		}
		else {
						return Time.deltaTime;
		}
    }
	Vector3 lookDirection;
    public Playerstates2 PS2 = Playerstates2.MovingW;
	public PlayerStates PS = PlayerStates.N;
	public Vector3 movePos;
	public float wireRange;

    public GameObject debugSphere;

    void Update()
    {
        if ((PS2 == Playerstates2.MovingN)|| (PS2 == Playerstates2.MovingS)|| (PS2 == Playerstates2.MovingE)|| (PS2 == Playerstates2.MovingW)) {
            if (PS == PlayerStates.N) {
                RaycastHit hit1;
                Ray Ray1 = new Ray(transform.position, -Vector3.up);

                RaycastHit L1;
                Ray leftray1 = new Ray(transform.position, Vector3.left);
                RaycastHit R1;
                Ray rightray1 = new Ray(transform.position, Vector3.right);

                if (Physics.Raycast(Ray1, out hit1, StickRange)) {
                    float hoverError1 = hit1.distance - height;
                    if (hoverError1 != 0) {
                        transform.position -= new Vector3(0, hoverError1, 0);
                    }

                    if (PS2 == Playerstates2.MovingW) {
                        transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
                        lookDirection = Vector3.left;
                    }
                    if (PS2 == Playerstates2.MovingE) {
                        transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
                        lookDirection = Vector3.right;
                    }

                    if (Physics.Raycast(leftray1, out L1, WallRange)) {
                        float LeftError1 = L1.distance - side;
                        if (LeftError1 < 0.05f) {
                            transform.Rotate(0, 0, -90, Space.World);
                            transform.position += new Vector3(0.5f, -0.4f, 0);
                            PS = PlayerStates.E;
                        }
                    }
                    if (Physics.Raycast(rightray1, out R1, WallRange)) {
                        float RightError1 = R1.distance - side;
                        if (RightError1 < 0.05f) {
                            transform.Rotate(0, 0, 90, Space.World);
                            transform.position += new Vector3(-0.5f, -0.4f, 0);
                            PS = PlayerStates.W;
                        }
                    }

                }
                else {
                    if (lookDirection == Vector3.left) {
                        transform.Rotate(0, 0, 90, Space.World);
                        transform.position += new Vector3(-1, -1.1f, 0);
                        PS = PlayerStates.W;
                    }
                    if (lookDirection == Vector3.right) {
                        transform.Rotate(0, 0, -90, Space.World);
                        transform.position += new Vector3(1, -1.1f, 0);
                        PS = PlayerStates.E;
                    }
                }
            }

            if (PS == PlayerStates.E) {
                RaycastHit hit2;
                Ray Ray2 = new Ray(transform.position, Vector3.left);

                RaycastHit U1;
                Ray upray1 = new Ray(transform.position, Vector3.up);
                RaycastHit D1;
                Ray downray1 = new Ray(transform.position, Vector3.down);

                if (Physics.Raycast(Ray2, out hit2, StickRange)) {
                    float hoverError2 = hit2.distance - height;
                    if (hoverError2 != 0) {
                        transform.position -= new Vector3(hoverError2, 0, 0);
                    }

                    if (PS2 == Playerstates2.MovingN) {
                        transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
                        lookDirection = Vector3.left;
                    }
                    if (PS2 == Playerstates2.MovingS) {
                        transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
                        lookDirection = Vector3.right;
                    }

                    if (Physics.Raycast(upray1, out U1, WallRange)) {
                        float UpError1 = U1.distance - side;
                        if (UpError1 < 0.05f) {
                            transform.Rotate(0, 0, -90, Space.World);
                            transform.position += new Vector3(-0.4f, -0.5f, 0);
                            PS = PlayerStates.S;
                        }
                    }
                    if (Physics.Raycast(downray1, out D1, WallRange)) {
                        float DownError1 = D1.distance - side;
                        if (DownError1 < 0.05f) {
                            transform.Rotate(0, 0, 90, Space.World);
                            transform.position += new Vector3(-0.4f, 0.5f, 0);
                            PS = PlayerStates.N;
                        }
                    }

                } else {
                    if (lookDirection == Vector3.left) {
                        transform.Rotate(0, 0, 90, Space.World);
                        transform.position += new Vector3(-1.1f, 1, 0);
                        PS = PlayerStates.N;
                    }
                    if (lookDirection == Vector3.right) {
                        transform.Rotate(0, 0, -90, Space.World);
                        transform.position += new Vector3(-1.1f, -1, 0);
                        PS = PlayerStates.S;
                    }
                }
            }

            if (PS == PlayerStates.S) {
                RaycastHit hit3;
                Ray Ray3 = new Ray(transform.position, Vector3.up);

                RaycastHit L2;
                Ray leftray2 = new Ray(transform.position, Vector3.left);
                RaycastHit R2;
                Ray rightray2 = new Ray(transform.position, Vector3.right);

                if (Physics.Raycast(Ray3, out hit3, StickRange)) {
                    float hoverError3 = hit3.distance - height;
                    if (hoverError3 != 0) {
                        transform.position += new Vector3(0, hoverError3, 0);
                    }

                    if (PS2 == Playerstates2.MovingE) {
                        transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
                        lookDirection = Vector3.left;
                    }
                    if (PS2 == Playerstates2.MovingW) {
                        transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
                        lookDirection = Vector3.right;
                    }

                    if (Physics.Raycast(leftray2, out L2, WallRange)) {
                        float LeftError2 = L2.distance - side;
                        if (LeftError2 < 0.05f) {
                            transform.Rotate(0, 0, 90, Space.World);
                            transform.position += new Vector3(0.5f, 0.4f, 0);
                            PS = PlayerStates.E;
                        }
                    }
                    if (Physics.Raycast(rightray2, out R2, WallRange)) {
                        float RightError2 = R2.distance - side;
                        if (RightError2 < 0.05f) {
                            transform.Rotate(0, 0, -90, Space.World);
                            transform.position += new Vector3(-0.5f, 0.4f, 0);
                            PS = PlayerStates.W;
                        }
                    }

                } else {
                    if (lookDirection == Vector3.left) {
                        transform.Rotate(0, 0, 90, Space.World);
                        transform.position += new Vector3(1, 1.1f, 0);
                        PS = PlayerStates.E;
                    }
                    if (lookDirection == Vector3.right) {
                        transform.Rotate(0, 0, -90, Space.World);
                        transform.position += new Vector3(-1, 1.1f, 0);
                        PS = PlayerStates.W;
                    }
                }
            }

            if (PS == PlayerStates.W) {
                RaycastHit hit4;
                Ray Ray4 = new Ray(transform.position, Vector3.right);

                RaycastHit U2;
                Ray upray2 = new Ray(transform.position, Vector3.up);
                RaycastHit D2;
                Ray downray2 = new Ray(transform.position, Vector3.down);

                if (Physics.Raycast(Ray4, out hit4, StickRange)) {
                    float hoverError4 = hit4.distance - height;
                    if (hoverError4 != 0) {
                        transform.position += new Vector3(hoverError4, 0, 0);
                    }

                    if (PS2 == Playerstates2.MovingS) {
                        transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
                        lookDirection = Vector3.left;
                    }
                    if (PS2 == Playerstates2.MovingN) {
                        transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
                        lookDirection = Vector3.right;
                    }

                    if (Physics.Raycast(upray2, out U2, WallRange)) {
                        float UpError2 = U2.distance - side;
                        if (UpError2 < 0.05f) {
                            transform.Rotate(0, 0, 90, Space.World);
                            transform.position += new Vector3(0.4f, -0.5f, 0);
                            PS = PlayerStates.S;
                        }
                    }
                    if (Physics.Raycast(downray2, out D2, WallRange)) {
                        float DownError2 = D2.distance - side;
                        if (DownError2 < 0.05f) {
                            transform.Rotate(0, 0, -90, Space.World);
                            transform.position += new Vector3(0.4f, 0.5f, 0);
                            PS = PlayerStates.N;
                        }
                    }

                } else {
                    if (lookDirection == Vector3.left) {
                        transform.Rotate(0, 0, 90, Space.World);
                        transform.position += new Vector3(1.1f, -1, 0);
                        PS = PlayerStates.S;
                    }
                    if (lookDirection == Vector3.right) {
                        transform.Rotate(0, 0, -90, Space.World);
                        transform.position += new Vector3(1.1f, 1, 0);
                        PS = PlayerStates.N;
                    }
                }
            }
        }

       
        if (PS2==Playerstates2.Casting){
            if (Input.GetMouseButtonDown(0)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {
                    movePos = new Vector3(hit.point.x, hit.point.y, transform.position.z);
                    //debugSphere.transform.position = movePos;
                }
                Vector3 wireDirection = new Vector3(movePos.x - transform.position.x, movePos.y - transform.position.y, 0f);

                Ray wire1 = new Ray(transform.position, wireDirection);
                RaycastHit wHit1;
                Vector3 A = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
                Ray wire2 = new Ray(A, wireDirection);
                RaycastHit wHit2;
                //Debug.DrawRay(transform.position, wireDirection * wireRange, Color.red);

                if ((Physics.Raycast(wire1, out wHit1, wireRange)) &&(Physics.Raycast(wire2, out wHit2, wireRange))) {
                    if (wHit1.point.x == wHit2.point.x) {
                        if ((wHit1.point.x - transform.position.x) > 0) {
                            if (PS == PlayerStates.N) {
                                transform.Rotate(0, 0, 90, Space.World);
                            }
                            if (PS == PlayerStates.E) {
                                transform.Rotate(0, 0, 180, Space.World);
                            }
                            if (PS == PlayerStates.S) {
                                transform.Rotate(0, 0, -90, Space.World);
                            }
                            transform.position = new Vector3(wHit1.point.x - 1f, wHit1.point.y, 0);
                            PS = PlayerStates.W;
                        }
                        else if ((wHit1.point.x - transform.position.x) < 0) {
                            if (PS == PlayerStates.N) {
                                transform.Rotate(0, 0, -90, Space.World);
                            }
                            if (PS == PlayerStates.S) {
                                transform.Rotate(0, 0, 90, Space.World);
                            }
                            if (PS == PlayerStates.W) {
                                transform.Rotate(0, 0, 180, Space.World);
                            }
                            transform.position = new Vector3(wHit1.point.x + 1f, wHit1.point.y, 0);
                            PS = PlayerStates.E;
                        }
                    }
                    if (wHit1.point.y == wHit2.point.y) {
                        if ((wHit1.point.y - transform.position.y) > 0) {
                            if (PS == PlayerStates.N) {
                                transform.Rotate(0, 0, 180, Space.World);
                            }
                            if (PS == PlayerStates.E) {
                                transform.Rotate(0, 0, -90, Space.World);
                            }
                            if (PS == PlayerStates.W) {
                                transform.Rotate(0, 0, 90, Space.World);
                            }
                            transform.position = new Vector3(wHit1.point.x, wHit1.point.y - 1f, 0);
                            PS = PlayerStates.S;
                        }
                        else if ((wHit1.point.y - transform.position.y) < 0) {
                            if (PS == PlayerStates.E) {
                                transform.Rotate(0, 0, 90, Space.World);
                            }
                            if (PS == PlayerStates.S) {
                                transform.Rotate(0, 0, 180, Space.World);
                            }
                            if (PS == PlayerStates.W) {
                                transform.Rotate(0, 0, -90, Space.World);
                            }
                            transform.position = new Vector3(wHit1.point.x, wHit1.point.y + 1f, 0);
                            PS = PlayerStates.N;
                        }
                    }
                }
                PS2 = Playerstates2.Teleporting;
            }
        }
    }
}
