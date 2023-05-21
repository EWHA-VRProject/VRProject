using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;   // �̵��ӷ�
    public float gravity = -10f;    // �߷°��ӵ�
    public float jumpPower = 1f;    // ���� ��
    float yVelocity = 0;    // y���� �ӷ�


    CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();   
    }

    void Update()
    {
        // �����¿� �̵�
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
    }
}
