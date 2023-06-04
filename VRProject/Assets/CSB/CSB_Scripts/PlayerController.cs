using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    // 정답 확인 관련 =====================
    public GameObject answer;
    public Transform[] answerChildren;
    public Transform[] child;
    public List<bool> isAnsList = new List<bool>();
    public List<bool> isList = new List<bool>();

    public List<bool> isListChange = new List<bool>();

    GameObject target;

    // 초점 관련 ==========================
    public Transform crosshair;

    // 아이템 잡기 관련 =========
    public bool isItem = false; // 잡고 있는지 여부
    GameObject item;


    void Start()
    {
        cc = GetComponent<CharacterController>();

        answer = GameObject.FindWithTag("Answer");  // 정답 게임오브젝트
        answerChildren = answer.GetComponentsInChildren<Transform>(true);   // 정답의 자식 모두

    }

    void Update()
    {
        // 레이저 발사 관련 ======================================
        // 충돌한 물체가 있으면 레이저 발사 (레이저는 마우스 또는 컨트롤러가 가리키는 방향으로 생성)
        VRInput.LaserPoint(crosshair);


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

        // 점프, PC일 경우 스페이스 바, Oculus일 경우 오른쪽 Button.Two 점프 버튼 클릭
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

        GoTOThePrison();

        // 아이템 잡기 관련 ============================================
        if (!isItem)    // 잡은 것이 없는 경우
        {
            // 잡기
            DragAnItem();
        }
        else
        {
            // 놓기
            CatchAnItem();
        }

    }

    // 타겟 잡기 관련 함수 (오른쪽 Button.One) ========================================================
    void UnGrab()
    {
        //
        if(VRInput.GetUp(VRInput.Button.One, VRInput.Controller.RController))
        {
            isGrab = false;
            target.GetComponent<Rigidbody>().isKinematic = false;
            target = null;
        }
    }

    // 타겟 잡기 (오른쪽 Button.One)
    // - layer가 target일 경우 감옥으로, 만약 감옥이 다 찼으면 감옥이 다 찼다고 알려줌
    void Grab()
    {
        if (VRInput.GetDown(VRInput.Button.One, VRInput.Controller.RController))
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

    // 최종 선택 관련 함수 (오른쪽 IndexTrigger) ================================
    void MakeAChoice()
    {
        if(VRInput.GetDown(VRInput.Button.IndexTrigger, VRInput.Controller.RController))
        {
            child.Initialize();
            isAnsList.Clear();
            isList.Clear();

            Ray ray = new Ray(VRInput.RHandPosition, VRInput.RHandDirection);    // 레이 쏘기
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                foreach (Transform child in answerChildren)
                {
                    isAnsList.Add(child.gameObject.activeSelf); // 정답의 자식들 bool 값 (활성화 여부)
                }
                child = hitInfo.transform.GetComponentsInChildren<Transform>(true);  // 레이 자식 오브젝트 모두
                foreach (Transform child in child)
                {
                    isList.Add(child.gameObject.activeSelf);
                }

                // 

                if (hitInfo.transform.CompareTag("Answer")) // 태그가 "Answer"이면
                {
                    print("정답");
                }
                else if(hitInfo.transform.CompareTag("Target"))
                {
                    print("실패");
                }
                else if (isList.SequenceEqual(isAnsList))
                {
                    List<bool> isAllTrue = new List<bool>();
                    // 텍스쳐까지 같은지 확인
                    // SetActive 가 true 인 오브젝트들 찾아서 텍스쳐 동일한지 확인
                    for (int i = 0; i < isList.Count; i++)
                    {
                        if (child[i].gameObject.GetComponent<SkinnedMeshRenderer>() == null)
                        {
                            print("아니야");
                        }
                        else if (child[i].gameObject.GetComponent<SkinnedMeshRenderer>().material.mainTexture == answerChildren[i].gameObject.GetComponent<SkinnedMeshRenderer>().material.mainTexture)
                        {
                            isAllTrue.Add(true);
                        }
                        else
                        {
                            print("여긴?");
                            isAllTrue.Add(false);
                        }

                    }
                    if(isAllTrue.All(element => element == true))
                    {
                        print("찐정답");
                    }
                    

                }
            }
        }
    }

    // 감옥 이동 관련 (왼쪽 Button.One) ===================
    public GameObject prisonPoint;
    bool isPrison = false;
    void GoTOThePrison()
    {
        if(VRInput.GetDown(VRInput.Button.One, VRInput.Controller.LController))
        {
            if (!isPrison)
            {
                this.gameObject.transform.position = prisonPoint.transform.position;
                isPrison = true;
            }
            else
            {
                this.gameObject.transform.position = new Vector3(0, 1, 0);
                isPrison = false;
            }
        }
    }

    // 아이템 잡기 관련 (왼쪽 IndexTrigger) ==================
    void CatchAnItem()
    {
        // 한번 누르면 아이템 획득
        if(VRInput.GetDown(VRInput.Button.IndexTrigger, VRInput.Controller.LController))
        {
            Ray ray = new Ray(VRInput.LHandPosition, VRInput.LHandDirection);    // 레이 쏘기
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))   // 만약 레이어가 item이면
            {
                if (hitInfo.transform.CompareTag("Item"))
                {
                    // 인벤토리로
                    isItem = false;
                    item = hitInfo.transform.gameObject;


                    // ***** 아이템 인벤토리로 추가 ******
                }
            }
        }
        if (VRInput.GetUp(VRInput.Button.IndexTrigger, VRInput.Controller.LController))
        {
            isItem = false;
            item.GetComponent<Rigidbody>().isKinematic = false;
            item.transform.parent = null;
            item = null;

        }
    }

    void DragAnItem()
    {        
        // 꾹 누르면 아이템 드래그
        if (VRInput.Get(VRInput.Button.IndexTrigger, VRInput.Controller.LController))
        {
            Ray ray = new Ray(VRInput.LHandPosition, VRInput.LHandDirection);    // 레이 쏘기
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))   // 만약 레이어가 item이면
            {
                if (hitInfo.transform.CompareTag("Item"))
                {
                    isItem = true;

                    // 컨트롤러가 향하는 방향 따라 이동
                    item = hitInfo.transform.gameObject;
                    item.transform.parent = VRInput.LHand;
                    item.GetComponent<Rigidbody>().isKinematic = true;

                    // ***** 아이템 인벤토리로 추가 ******
                }
            }
        }
        //

    }

    // 인벤토리 열기 (왼쪽 Button.Two) =================
}
