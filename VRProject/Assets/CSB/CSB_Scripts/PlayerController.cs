using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    Vector3 pos;

    // �̵� ���� ========
    public float moveSpeed = 10f;   // �̵��ӷ�
    public float gravity = -10f;    // �߷°��ӵ�
    public float jumpPower = 1f;    // ���� ��
    float yVelocity = 0;    // y���� �ӷ�

    CharacterController cc;

    // Ÿ�� ��� ���� ====================================
    public bool isGrab = false; // ��� �ִ��� ����
    public float range = 0.1f;  // ���� �� �ִ� ����
    public GameObject prison;
    // ���࿡ tag�� Target�̸� �������� -> ������ List�� 
    public List<GameObject> targets = new List<GameObject>();

    // ���� Ȯ�� ���� =====================
    public GameObject answer;
    public Transform[] answerChildren;
    public Transform[] child;
    public List<bool> isAnsList = new List<bool>();
    public List<bool> isList = new List<bool>();

    public List<bool> isListChange = new List<bool>();

    GameObject target;

    // ���� ���� ==========================
    public Transform crosshair;

    // ������ ��� ���� =========
    public bool isItem = false; // ��� �ִ��� ����
    GameObject item;
    // public Inventory inventoryM=GameObject.Find("InventoryM").GetComponent<Inventory>;
    
    
    public Inventory inv ;

    // Sound ���� =====
    AudioSource audioSource;
    public AudioClip audioClip0;


    void Start()
    {
        inv=GameObject.FindGameObjectWithTag("invM").GetComponent<Inventory>();
        cc = GetComponent<CharacterController>();
        pos = transform.position;

        audioSource = GetComponent<AudioSource>();

        answer = GameObject.FindWithTag("Answer");  // ���� ���ӿ�����Ʈ
        answerChildren = answer.GetComponentsInChildren<Transform>(true);   // ������ �ڽ� ���

    }

    void Update()
    {
        pos = transform.position;

        // ������ �߻� ���� ======================================
        // �浹�� ��ü�� ������ ������ �߻� (�������� ���콺 �Ǵ� ��Ʈ�ѷ��� ����Ű�� �������� ����)
        VRInput.LaserPoint(crosshair);


        // �����¿� �̵� ���� ==================================
        float h = VRInput.GetAxis("Horizontal");
        float v = VRInput.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);    // ī�޶� �ٶ󺸴� ������ �������� �ϱ� ����

        yVelocity += gravity * Time.deltaTime;

        // �ٴڿ� ������ �����ӵ� 0����
        if (cc.isGrounded)
        {
            yVelocity = 0;
        }

        // ����, PC�� ��� �����̽� ��, Oculus�� ��� ������ Button.Two ���� ��ư Ŭ��
        if(VRInput.GetDown(VRInput.Button.Two, VRInput.Controller.RController))
        {
            yVelocity = jumpPower;
        }

        dir.y = yVelocity;

        cc.Move(dir * moveSpeed * Time.deltaTime);
        if(pos != transform.position)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClip0);
            }
        }
        else
        {
            audioSource.Pause();
        }


        // Ÿ�� ��� ���� ============================================
        if (!isGrab)    // ���� ���� ���� ���
        {
            // ���
            Grab();
        }
        else
        {
            // ����
            UnGrab();
        }

        // ���� ���� ���� =========
        MakeAChoice();

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            GoTOThePrison();
        }

        CatchAnItem();
/*        // ������ ��� ���� ============================================
        if (!isItem)    // ���� ���� ���� ���
        {
            // ���
            DragAnItem();
        }
        else
        {
            // ����
            CatchAnItem();
        }*/

    }

    // Ÿ�� ��� ���� �Լ� (������ Button.One) ========================================================
    void UnGrab()
    {
        //
        if(VRInput.GetUp(VRInput.Button.One, VRInput.Controller.RController))
        {
            VRInput.PlayVibration(VRInput.Controller.RController);
            isGrab = false;
            target.GetComponent<Rigidbody>().isKinematic = false;
            target = null;
        }
    }

    NavMeshAgent agent;
    // Ÿ�� ��� (������ Button.One)
    // - layer�� target�� ��� ��������, ���� ������ �� á���� ������ �� á�ٰ� �˷���
    void Grab()
    {
        if (VRInput.GetDown(VRInput.Button.One, VRInput.Controller.RController))
        {
            Ray ray = new Ray(VRInput.RHandPosition, VRInput.RHandDirection);    // ���� ���
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo))   // ���� ���̾ target�̸�
            {
                if(hitInfo.transform.CompareTag("Target") || hitInfo.transform.CompareTag("Answer"))
                {
                    isGrab = false;

                    target = hitInfo.transform.gameObject;
                    Test_script test = target.GetComponent<Test_script>();
                    print(1);
                    test.playerTouched = true;

/*                    // ��������
                    isGrab = false;
                    target = hitInfo.transform.gameObject;
                    // ���� ������ �� á���� "������ ��á��" �˷���
                    if (targets.Count >= 3)
                    {
                        print("���� ����");
                    }
                    else
                    {
                        targets.Add(target);    // ���� (Ÿ�� ����Ʈ)�� �߰�
                        // ���� �߰��� ������ �ε��� == ����Ʈ�� ������ �ε���
                        int index = targets.Count - 1;
                        // �ڽ��� �ε����� �ش��ϴ� Prison ���ӿ�����Ʈ�� �ڽ� �ε����� ��ġ �̵�
                        target.transform.position = prison.transform.GetChild(index).position;
                        //target.SetActive(false);    // ��Ȱ��ȭ
                        print("��Ҵ� ���");
                    }*/
                }
                else
                {
                    Debug.Log("grab hhh");
                }

            }
        }
    }

    // ���� ���� ���� �Լ� (������ IndexTrigger) ================================
    void MakeAChoice()
    {
        if(VRInput.GetDown(VRInput.Button.IndexTrigger, VRInput.Controller.RController))
        {
            VRInput.PlayVibration(VRInput.Controller.RController);
            child.Initialize();
            isAnsList.Clear();
            isList.Clear();

            Ray ray = new Ray(VRInput.RHandPosition, VRInput.RHandDirection);    // ���� ���
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                foreach (Transform child in answerChildren)
                {
                    isAnsList.Add(child.gameObject.activeSelf); // ������ �ڽĵ� bool �� (Ȱ��ȭ ����)
                }
                child = hitInfo.transform.GetComponentsInChildren<Transform>(true);  // ���� �ڽ� ������Ʈ ���
                foreach (Transform child in child)
                {
                    isList.Add(child.gameObject.activeSelf);
                }

                // 

                if (hitInfo.transform.CompareTag("Answer")) // �±װ� "Answer"�̸�
                {
                    Debug.Log("answer hhh");
                }
                else if(hitInfo.transform.CompareTag("Target"))
                {
                    Debug.Log("target hhh");
                }
                else if (isList.SequenceEqual(isAnsList))
                {
                    List<bool> isAllTrue = new List<bool>();
                    // �ؽ��ı��� ������ Ȯ��
                    // SetActive �� true �� ������Ʈ�� ã�Ƽ� �ؽ��� �������� Ȯ��
                    for (int i = 0; i < isList.Count; i++)
                    {
                        if (child[i].gameObject.GetComponent<SkinnedMeshRenderer>() == null)
                        {
                            print("�ƴϾ�");
                        }
                        else if (child[i].gameObject.GetComponent<SkinnedMeshRenderer>().material.mainTexture == answerChildren[i].gameObject.GetComponent<SkinnedMeshRenderer>().material.mainTexture)
                        {
                            isAllTrue.Add(true);
                        }
                        else
                        {
                            print("����?");
                            isAllTrue.Add(false);
                        }

                    }
                    if(isAllTrue.All(element => element == true))
                    {
                        print("������");
                    }
                    

                }
            }
        }
    }

    // ���� �̵� ���� (���� Button.One) ===================
    public GameObject prisonPoint;
    bool isPrison = false;
    public GameObject returnPos;

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
                this.gameObject.transform.position = returnPos.transform.position;
                isPrison = false;
            }
        }
    }

    // ������ ��� ���� (���� IndexTrigger) ==================
    void CatchAnItem()
    {
        // �ѹ� ������ ������ ȹ��
        if(VRInput.GetDown(VRInput.Button.IndexTrigger, VRInput.Controller.LController))
        {
            VRInput.PlayVibration(VRInput.Controller.LController);
            Ray ray = new Ray(VRInput.LHandPosition, VRInput.LHandDirection);    // ���� ���
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))   // ���� ���̾ item�̸�
            {
                
                Item itemCatched = hitInfo.transform.GetComponent<ItemPickUp>().item;
                GameObject itemC = hitInfo.transform.gameObject;
                if (itemCatched)
                {

                    Renderer itemRenderer = itemC.GetComponent<Renderer>();
                    if(itemRenderer!=null){
                        itemRenderer.enabled=false;
                    }
                    //hitInfo.transform.CompareTag("Item")
                    // �κ��丮��
                    print(itemCatched.itemName);
                    inv.AddItem(itemCatched);
                    // ***** ������ �κ��丮�� �߰� ******
                }
            }
        }
        if (VRInput.GetUp(VRInput.Button.IndexTrigger, VRInput.Controller.LController))
        {
                    // isItem = false;
                    // item.GetComponent<Rigidbody>().isKinematic = false;
                    // item.transform.parent = null;
                    // item = null;

        }
    }

/*    void DragAnItem()
    {        
        // �� ������ ������ �巡��
        if (VRInput.Get(VRInput.Button.IndexTrigger, VRInput.Controller.LController))
        {
            Ray ray = new Ray(VRInput.LHandPosition, VRInput.LHandDirection);    // ���� ���
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))   // ���� ���̾ item�̸�
            {
                if (hitInfo.transform.CompareTag("Item"))
                {
                    isItem = true;

                    // ��Ʈ�ѷ��� ���ϴ� ���� ���� �̵�
                    item = hitInfo.transform.gameObject;
                    item.transform.parent = VRInput.LHand;
                    item.GetComponent<Rigidbody>().isKinematic = true;

                    // ***** ������ �κ��丮�� �߰� ******
                }
            }
        }
        //

    }*/

    // �κ��丮 ���� (���� Button.Two) =================
}
