using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;   // 이동속력
    public float gravity = -10f;    // 중력가속도
    public float jumpPower = 1f;    // 점프 힘
    float yVelocity = 0;    // y방향 속력


    CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();   
    }

    void Update()
    {
        // 상하좌우 이동
        float h = VRInput.GetAxis("Horizontal");
        float v = VRInput.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);    // 카메라가 바라보는 방향을 기준으로 하기 위함

        yVelocity += gravity * Time.deltaTime;

        // 바닥에 있으면 수직속도 0으로
        if (cc.isGrounded)
        {
            yVelocity = 0;
        }

        // 점프, PC일 경우 스페이스 바, Oculus일 경우 오른쪽 컨트롤러 점프 버튼 클릭
        if(VRInput.GetDown(VRInput.Button.Two, VRInput.Controller.RController))
        {
            yVelocity = jumpPower;
        }

        dir.y = yVelocity;

        cc.Move(dir * moveSpeed * Time.deltaTime);
    }
}
