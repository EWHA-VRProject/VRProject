using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

    void Start()
    {
        cc = GetComponent<CharacterController>();

        answer = GameObject.FindWithTag("Answer");  // ���� ���ӿ�����Ʈ
        answerChildren = answer.GetComponentsInChildren<Transform>(true);   // ������ �ڽ� ���

    }

    void Update()
    {
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

        // ����, PC�� ��� �����̽� ��, Oculus�� ��� ������ ��Ʈ�ѷ� ���� ��ư Ŭ��
        if(VRInput.GetDown(VRInput.Button.Two, VRInput.Controller.RController))
        {
            yVelocity = jumpPower;
        }

        dir.y = yVelocity;

        cc.Move(dir * moveSpeed * Time.deltaTime);

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

        GoTOThePrison();

    }

    // Ÿ�� ��� ���� �Լ� ========================================================
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

    // Ÿ�� ��� - layer�� target�� ��� ��������, ���� ������ �� á���� ������ �� á�ٰ� �˷���
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
                    // ��������
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
                    }
                }
                else
                {
                    print("�̰� �ƴ�");
                }

            }
        }
    }

    // ���� ���� ���� �Լ� ================================
    void MakeAChoice()
    {
        if(VRInput.GetDown(VRInput.Button.IndexTrigger, VRInput.Controller.RController))
        {
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
                    print("����");
                }
                else if(hitInfo.transform.CompareTag("Target"))
                {
                    print("����");
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

    // ���� �̵� ���� ===================
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
}
