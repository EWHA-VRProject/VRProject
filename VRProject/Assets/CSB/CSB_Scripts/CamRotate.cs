using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// VR 기기를 사용하지 않을 때 카메라 회전을 위함
// 마우스로 컨트롤
public class CamRotate : MonoBehaviour
{
    Vector3 currAngle;  // 현재 각도
    public float sensitivity = 200; // 마우스 감도

    void Start()
    {
        // 카메라 현재 각도 입력
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
