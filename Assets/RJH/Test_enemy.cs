using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_enemy : MonoBehaviour
{ public float speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dirV = transform.up * speed * Time.deltaTime;
        Vector3 dirH = transform.right * speed * Time.deltaTime;
        Vector3 dir = dirH + dirV;
        dir.Normalize();

        transform.position += dir * speed * Time.deltaTime;


    }
}
