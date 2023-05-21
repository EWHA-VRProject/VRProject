using System.Collections;
using System.Collections.Generic;
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


    GameObject target;


    void Start()
    {
        cc = GetComponent<CharacterController>();   
    }

    void Update()
    {
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

    }

    // Ÿ�� ��� ���� �Լ� ========================================================
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

    // Ÿ�� ��� - layer�� target�� ��� ��������, ���� ������ �� á���� ������ �� á�ٰ� �˷���
    void Grab()
    {
        if (VRInput.GetDown(VRInput.Button.HandTrigger, VRInput.Controller.RController))
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
            Ray ray = new Ray(VRInput.RHandPosition, VRInput.RHandDirection);    // ���� ���
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.CompareTag("Answer")) // �±װ� "Answer"�̸�
                {
                    print("����");
                }
                else if(hitInfo.transform.CompareTag("Target"))
                {
                    print("����");
                }
            }
        }
    }
}
