#define PC
#define Oculus

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PC, Oculus 환경 모두 적용하기위함
public static class VRInput
{
    // 하나의 명령어로 PC, Oculus 환경 모두 사용하기 위해 통일
#if PC
    public enum ButtonController
    {
        Fire1,
        Fire2,
        Fire3, 
        Jump,
    }
#endif
    public enum Button
    {
#if PC
        One = ButtonController.Fire1,
        Two = ButtonController.Jump,
        Thumbstick = ButtonController.Fire1,
        IndexTrigger = ButtonController.Fire3,
        HandTrigger = ButtonController.Fire2,

#elif Oculus
        One = OVRInput.Button.One,
        Two = OVRInput.Button.Two,
        Thumbstick = OVRInput.Button.PrimaryThumbstick,
        IndexTrigger = OVRInput.Button.PrimaryIndexTrigger,
        HandTrigger = OVRInput.Button.PrimaryHandTrigger,
#endif
    }

    // 컨트롤러 종류 (왼쪽, 오른쪽 구별)
    public enum Controller
    {
#if PC
        RController,
        LController,

#elif Oculus
        RController = OVRInput.Controller.RTouch,
        LController = OVRInput.Controller.LTouch,
#endif
    }

    // Oculus 일 때
#if Oculus
    static Transform transform;
#endif

#if Oculus
    static Transform GetTransform()
    {
        if(transform == null)
        {
            transform = GameObject.Find("TrackingSpace").transform;
        }
        return transform;
    }
#endif

    // 오른쪽 컨트롤러 위치 얻어오기
    public static Vector3 RHandPosition
    {
        get
        {
#if PC
            // 마우스의 스크린 좌표
            Vector3 pos = Input.mousePosition;
            pos.z = 0.7f;
            pos = Camera.main.ScreenToWorldPoint(pos); // 스크린 좌표를 월드 좌표로 변환
            RHand.position = pos;
            return pos;
#elif Oculus
            Vector3 pos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            pos = GetTransform().TransformPoint(pos);
            return pos;
#endif
        }
    }

    // 오른쪽 컨트롤러 방향 얻어오기
    public static Vector3 RHandDirection
    {
        get
        {
#if PC
            Vector3 dir = RHandPosition - Camera.main.transform.position;   // 카메라에서 RHand 향하는 방향
            RHand.forward = dir;
            return dir;

#elif Oculus
            Vector3 dir = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward;
            dir = GetTransform().TransformDirection(dir);
            return dir;

#endif
        }
    }

    // 왼쪽 컨트롤러 위치 얻어오기
    public static Vector3 LHandPosition
    {
        get
        {
#if PC
            // 마우스의 스크린 좌표
            Vector3 pos = Input.mousePosition;
            pos.z = 0.7f;
            pos = Camera.main.ScreenToWorldPoint(pos); // 스크린 좌표를 월드 좌표로 변환
            LHand.position = pos;
            return pos;

#elif Oculus
            Vector3 pos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            pos = GetTransform().TransformPoint(pos);
            return pos;
#endif
        }
    }

    // 왼쪽 컨트롤러 방향 얻어오기
    public static Vector3 LHandDirection
    {
        get
        {
#if PC
            Vector3 dir = LHandPosition - Camera.main.transform.position;   // 카메라에서 RHand 향하는 방향
            LHand.forward = dir;
            return dir;

#elif Oculus
            Vector3 dir = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch) * Vector3.forward;
            dir = GetTransform().TransformDirection(dir);
            return dir;
#endif
        }
    }

    // 왼쪽 컨트롤러
    static Transform lHand;
    // 씬에 등록된 왼쪽 컨트롤러 찾아 반환
    public static Transform LHand
    {
        get
        {
            if (lHand == null)
            {
#if PC
                // LHand라는 이름으로 게임 오브젝트 생성
                GameObject handObj = new GameObject("LHand");
                // 만들어진 객체의 트랜스폼을 lHand에 할당
                lHand = handObj.transform;
                // 컨트롤러를 카메라의 자식 객체로 등록
                lHand.parent = Camera.main.transform;

#elif Oculus
                lHand = GameObject.Find("LeftControllerAnchor").transform;
#endif
            }
            return lHand;
        }
    }

    // 오른쪽 컨트롤러
    static Transform rHand;
    // 씬에 등록된 오른쪽 컨트롤러 찾아 반환
    public static Transform RHand
    {
        get
        {
            if (rHand == null)
            {
#if PC
                // RHand라는 이름으로 게임 오브젝트 생성
                GameObject handObj = new GameObject("RHand");
                // 만들어진 객체의 트랜스폼을 lHand에 할당
                rHand = handObj.transform;
                // 컨트롤러를 카메라의 자식 객체로 등록
                rHand.parent = Camera.main.transform;
#elif Oculus
                rHand = GameObject.Find("RightControllerAnchor").transform;
#endif
            }
            return rHand;
        }
    }

    // ======= 버튼 입력 관련 ==============
    // 버튼 누르는 중
    public static bool Get(Button input, Controller hand = Controller.RController)
    {
#if PC
        return Input.GetButton(((ButtonController)input).ToString());

#elif Oculus
        return OVRInput.Get((OVRInput.Button)input, (OVRInput.Controller)hand);
#endif
    }

    // 버튼 누를 떄
    public static bool GetDown(Button input, Controller hand = Controller.RController)
    {
#if PC
        return Input.GetButtonDown(((ButtonController)input).ToString());
#elif Oculus
        return OVRInput.GetDown((OVRInput.Button)input, (OVRInput.Controller)hand);
#endif
    }

    // 버튼 뗄 때
    public static bool GetUp(Button input, Controller hand = Controller.RController)
    {
#if PC
        return Input.GetButtonUp(((ButtonController)input).ToString());
#elif Oculus
        return OVRInput.GetUp((OVRInput.Button)input, (OVRInput.Controller)hand);
#endif
    }

    // Thumbstick 관련
    // 컨트롤러의 axis 입력
    public static float GetAxis(string axis, Controller hand = Controller.LController)
    {
#if PC
        return Input.GetAxis(axis);

#elif Oculus
        if(axis == "Horizontal")
        {
            return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, (OVRInput.Controller)hand).x;
        }
        else
        {
            return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, (OVRInput.Controller)hand).y;
        }
#endif
    }

    // 초기 월드에서 사용자의 방향, 카메라 기준으로 설정
    public static void SetCenter()
    {
#if Oculus
        OVRManager.display.RecenterPose();
#endif
    }

    // 레이저 관련. 컨트롤러 또는 마우스가 가리키는 방향
    // ray를 쏘다가 물체에 맞으면 라인렌더러(crosshair) 그려주기
#if PC
    static Vector3 defaultScale = Vector3.one * 0.02f;
#else
    static Vector3 defaultScale = Vector3.one * 0.005f;
#endif
    public static void LaserPoint(Transform crosshair, bool isHand = true, Controller hand = Controller.RController)
    {
        Ray ray;
        if(isHand)
        {
#if PC
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#elif Oculus

            if(hand == Controller.RController)
            {
                ray = new Ray(RHandPosition, RHandDirection);
            }
            else
            {
                ray = new Ray(LHandPosition, LHandDirection);
            }
#endif
            Plane plane = new Plane(Vector3.up, 0);
            float distance = 0;

            if (plane.Raycast(ray, out distance))
            {
                // linerenderer 그리기
                // crosshair
                crosshair.position = ray.GetPoint(distance);
                crosshair.forward = -Camera.main.transform.forward;
                crosshair.localScale = defaultScale * Mathf.Max(1, distance);

            }
        }
    }

    // 진동 호출
    public static void PlayVibration(float duration, float frequency, float amplitude, Controller hand)
    {
#if Oculus
        if (CoroutineInstance.coroutineInstance == null)
        {
            GameObject coroutineObj = new GameObject("CoroutineInstance");
            coroutineObj.AddComponent<CoroutineInstance>();
        }
        CoroutineInstance.coroutineInstance.StopAllCoroutines();
        CoroutineInstance.coroutineInstance.StartCoroutine(VibrationCoroutine(duration, frequency, amplitude, hand));
#endif
    }

    public static void PlayVibration(Controller hand)
    {
#if Ouclus
        PlayVibration(0.06f, 1, 1, hand);
#endif
    }

#if Oculus
    static IEnumerator VibrationCoroutine(float duration, float frequency, float amplitude, Controller hand)
    {
        float currTime = 0;
        while (currTime < duration)
        {
            currTime += Time.deltaTime;
            OVRInput.SetControllerVibration(frequency, amplitude, (OVRInput.Controller)hand);
            yield return null;
        }
        OVRInput.SetControllerVibration(0, 0, (OVRInput.Controller)hand);
    }
#endif
}

// 진동을 위한 코루틴 객체
class CoroutineInstance : MonoBehaviour
{
    public static CoroutineInstance coroutineInstance = null;
    private void Awake()
    {
        if(coroutineInstance == null)
        {
            coroutineInstance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
}


