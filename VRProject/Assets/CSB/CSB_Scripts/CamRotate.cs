using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// VR ��⸦ ������� ���� �� ī�޶� ȸ���� ����
// ���콺�� ��Ʈ��
public class CamRotate : MonoBehaviour
{
    Vector3 currAngle;  // ���� ����
    public float sensitivity = 200; // ���콺 ����

    void Start()
    {
        // ī�޶� ���� ���� �Է�
        currAngle.x = Camera.main.transform.eulerAngles.y;
        currAngle.y = -Camera.main.transform.eulerAngles.x;
        currAngle.z = Camera.main.transform.eulerAngles.z;
    }

    void Update()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        currAngle.x += x * sensitivity * Time.deltaTime;
        currAngle.y += y * sensitivity * Time.deltaTime;

        transform.eulerAngles = new Vector3(-currAngle.y, currAngle.x, transform.eulerAngles.z);
    }
}
