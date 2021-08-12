using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_PlayerMove : MonoBehaviour
{
    public float walkSpeed;
    public float lookSensitivity;
    public float camUpLimit;
    public float camDownLimit;
    public float currentCameraRotationX;
    public Animator animator;
    public AudioSource footStepAudioSource;

    public Camera cam;
    public Transform camPos;    //카메라 최초 위치
    SH_StageManager stageManager;

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        cam = Camera.main;
        stageManager = FindObjectOfType<SH_StageManager>();
        cam.transform.position = camPos.position;
        cam.transform.rotation = camPos.rotation;

    }
    bool MoveAnimation(float x, float z)
    {
        if (x != 0 || z != 0)
        {
            animator.SetBool("Run", true);

            if (z != 0)
            {
                if (z < 0)
                {
                    animator.SetBool("Run_Forward", false);
                    animator.SetBool("Run_Backward", true);
                }
                else if (z > 0)
                {
                    animator.SetBool("Run_Forward", true);
                    animator.SetBool("Run_Backward", false);
                }
            }
            else
            {
                animator.SetBool("Run_Forward", false);
                animator.SetBool("Run_Backward", false);
            }

            if (x != 0)
            {
                if (x < 0)
                {
                    animator.SetBool("Run_Left", true);
                    animator.SetBool("Run_Right", false);
                }
                else if (x > 0)
                {
                    animator.SetBool("Run_Left", false);
                    animator.SetBool("Run_Right", true);
                }
            }
            else
            {
                animator.SetBool("Run_Left", false);
                animator.SetBool("Run_Right", false);
            }
            return true;
        }
        else
        {
            animator.SetBool("Run_Forward", false);
            animator.SetBool("Run_Backward", false);
            animator.SetBool("Run_Left", false);
            animator.SetBool("Run_Right", false);
            animator.SetBool("Run", false);
            return false;
        }
    }
    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if (!MoveAnimation(x, z))
            return;

        Vector3 _moveHorizontal = transform.right * x;
        Vector3 _moveVertical = transform.forward * z;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized;
        transform.position += _velocity * walkSpeed * SH_TimeScaler.deltaTime;
    }

    private void CameraAndCharacterRotate()  // 좌우 캐릭터 회전
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        var rotateVectorY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        var rotateVectorX = _xRotation * lookSensitivity;

        currentCameraRotationX -= rotateVectorX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -camUpLimit, camDownLimit);

        gameObject.transform.eulerAngles += rotateVectorY;
        cam.transform.eulerAngles = gameObject.transform.eulerAngles + new Vector3(currentCameraRotationX, 0f, 0f);
        camPos.localEulerAngles = cam.transform.eulerAngles;
    }
    // Update is called once per frame
    void Update()
    {
        animator.speed = 1f * SH_TimeScaler.TimeScale;

        if (stageManager.inputWaiting)
            return;
        cam.transform.position = camPos.position;
        Move();
        CameraAndCharacterRotate();
    }

    void Foot()
    {
        footStepAudioSource.Play();
    }
    void FootL() { footStepAudioSource.Play(); }
    void FootR() { footStepAudioSource.Play(); }

}
