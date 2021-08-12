using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float walkSpeed = 1.0f;
    public float turnspeed = 1.0f;

    public float CameraRo = 45f;

    private Rigidbody myRigid;
    public GameObject cam;

    void Start()
    {
        myRigid = GetComponent<Rigidbody>();  // private
    }

    void Update()  
    {
        //캐릭터 W,AS,D
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dirH = transform.right * h;
        Vector3 dirV = transform.forward * v;
        var dir = new Vector3(h, 0, v);

        Vector3 dirHV = (dirH + dirV).normalized * walkSpeed;

        myRigid.MovePosition(transform.position + dir * walkSpeed * SH_TimeScaler.deltaTime);

        var rot = cam.transform.rotation.eulerAngles;
        rot.y += Input.GetAxis("Mouse X") * turnspeed;
        rot.x += Input.GetAxis("Mouse Y") * turnspeed;
        var q = Quaternion.Euler(rot);
        q.z = 0;
        cam.transform.rotation = Quaternion.Slerp(transform.rotation, q, turnspeed * SH_TimeScaler.deltaTime);
    }

   
}