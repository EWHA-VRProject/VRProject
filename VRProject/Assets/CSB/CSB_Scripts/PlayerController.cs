using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 이동 관련 ========
    public float moveSpeed = 10f;   // 이동속력
    public float gravity = -10f;    // 중력가속도
    public float jumpPower = 1f;    // 점프 힘
    float yVelocity = 0;    // y방향 속력

    CharacterController cc;

    // 타겟 잡기 관련 ====================================
    public bool isGrab = false; // 잡고 있는지 여부
    public float range = 0.1f;  // 잡을 수 있는 범위
    public GameObject prison;
    // 만약에 tag가 Target이면 감옥으로 -> 감옥은 List로 
    public List<GameObject> targets = new List<GameObject>();


    GameObject target;


    void Start()
    {
        cc = GetComponent<CharacterController>();   
    }

    void Update()
    {
        // 상하좌우 이동 관련 ==================================
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

        // 타겟 잡기 관련 ============================================
        if (!isGrab)    // 잡은 것이 없는 경우
        {
            // 잡기
            Grab();
        }
        else
        {
            // 놓기
            UnGrab();
        }

        // 최종 선택 관련 =========
        MakeAChoice();

    }

    // 타겟 잡기 관련 함수 ========================================================
    void UnGrab()
    {
        //
        if(VRInput.GetUp(VRInput.Button.HandTrigger, VRInput.Controller.RController))
        {
            isGrab = false;
            target.GetComponent<Rigidbody>().isKinematic = false;
            target = null;
        }
    }

    // 타겟 잡기 - layer가 target일 경우 감옥으로, 만약 감옥이 다 찼으면 감옥이 다 찼다고 알려줌
    void Grab()
    {
        if (VRInput.GetDown(VRInput.Button.HandTrigger, VRInput.Controller.RController))
        {
            Ray ray = new Ray(VRInput.RHandPosition, VRInput.RHandDirection);    // 레이 쏘기
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo))   // 만약 레이어가 target이면
            {
                if(hitInfo.transform.CompareTag("Target") || hitInfo.transform.CompareTag("Answer"))
                {
                    // 감옥으로
                    isGrab = false;
                    target = hitInfo.transform.gameObject;
                    // 만약 감옥이 다 찼으면 "감옥이 다찼음" 알려줌
                    if (targets.Count >= 3)
                    {
                        print("감옥 다참");
                    }
                    else
                    {
                        targets.Add(target);    // 감옥 (타겟 리스트)에 추가
                        // 새로 추가한 아이의 인덱스 == 리스트의 마지막 인덱스
                        int index = targets.Count - 1;
                        // 자신의 인덱스에 해당하는 Prison 게임오브젝트의 자식 인덱스로 위치 이동
                        target.transform.position = prison.transform.GetChild(index).position;
                        //target.SetActive(false);    // 비활성화
                        print("잡았다 요놈");
                    }
                }
                else
                {
                    print("이거 아님");
                }

            }
        }
    }

    // 최종 선택 관련 함수 ================================
    void MakeAChoice()
    {
        if(VRInput.GetDown(VRInput.Button.IndexTrigger, VRInput.Controller.RController))
        {
            Ray ray = new Ray(VRInput.RHandPosition, VRInput.RHandDirection);    // 레이 쏘기
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.CompareTag("Answer")) // 태그가 "Answer"이면
                {
                    print("정답");
                }
                else if(hitInfo.transform.CompareTag("Target"))
                {
                    print("실패");
                }
            }
        }
    }
}
