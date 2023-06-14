#define PC
#define Oculus

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PC, Oculus ȯ�� ��� �����ϱ�����
public static class VRInput
{
    // �ϳ��� ��ɾ�� PC, Oculus ȯ�� ��� ����ϱ� ���� ����
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
        Thumbstick = OVRInput.Button.SecondaryThumbstick,
        IndexTrigger = OVRInput.Button.SecondaryIndexTrigger,
        HandTrigger = OVRInput.Button.SecondaryHandTrigger,
#endif
    }

    // ��Ʈ�ѷ� ���� (����, ������ ����)
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

    // Oculus �� ��
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

    // ������ ��Ʈ�ѷ� ��ġ ������
    public static Vector3 RHandPosition
    {
        get
        {
#if PC
            // ���콺�� ��ũ�� ��ǥ
            Vector3 pos = Input.mousePosition;
            pos.z = 0.7f;
            pos = Camera.main.ScreenToWorldPoint(pos); // ��ũ�� ��ǥ�� ���� ��ǥ�� ��ȯ
            RHand.position = pos;
            return pos;
#elif Oculus
            Vector3 pos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            pos = GetTransform().TransformPoint(pos);
            return pos;
#endif
        }
    }

    // ������ ��Ʈ�ѷ� ���� ������
    public static Vector3 RHandDirection
    {
        get
        {
#if PC
            Vector3 dir = RHandPosition - Camera.main.transform.position;   // ī�޶󿡼� RHand ���ϴ� ����
            RHand.forward = dir;
            return dir;

#elif Oculus
            Vector3 dir = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward;
            dir = GetTransform().TransformDirection(dir);
            return dir;

#endif
        }
    }

    // ���� ��Ʈ�ѷ� ��ġ ������
    public static Vector3 LHandPosition
    {
        get
        {
#if PC
            // ���콺�� ��ũ�� ��ǥ
            Vector3 pos = Input.mousePosition;
            pos.z = 0.7f;
            pos = Camera.main.ScreenToWorldPoint(pos); // ��ũ�� ��ǥ�� ���� ��ǥ�� ��ȯ
            LHand.position = pos;
            return pos;

#elif Oculus
            Vector3 pos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            pos = GetTransform().TransformPoint(pos);
            return pos;
#endif
        }
    }

    // ���� ��Ʈ�ѷ� ���� ������
    public static Vector3 LHandDirection
    {
        get
        {
#if PC
            Vector3 dir = LHandPosition - Camera.main.transform.position;   // ī�޶󿡼� RHand ���ϴ� ����
            LHand.forward = dir;
            return dir;

#elif Oculus
            Vector3 dir = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch) * Vector3.forward;
            dir = GetTransform().TransformDirection(dir);
            return dir;
#endif
        }
    }

    // ���� ��Ʈ�ѷ�
    static Transform lHand;
    // ���� ��ϵ� ���� ��Ʈ�ѷ� ã�� ��ȯ
    public static Transform LHand
    {
        get
        {
            if (lHand == null)
            {
#if PC
                // LHand��� �̸����� ���� ������Ʈ ����
                GameObject handObj = new GameObject("LHand");
                // ������� ��ü�� Ʈ�������� lHand�� �Ҵ�
                lHand = handObj.transform;
                // ��Ʈ�ѷ��� ī�޶��� �ڽ� ��ü�� ���
                lHand.parent = Camera.main.transform;

#elif Oculus
                lHand = GameObject.Find("LeftControllerAnchor").transform;
#endif
            }
            return lHand;
        }
    }

    // ������ ��Ʈ�ѷ�
    static Transform rHand;
    // ���� ��ϵ� ������ ��Ʈ�ѷ� ã�� ��ȯ
    public static Transform RHand
    {
        get
        {
            if (rHand == null)
            {
#if PC
                // RHand��� �̸����� ���� ������Ʈ ����
                GameObject handObj = new GameObject("RHand");
                // ������� ��ü�� Ʈ�������� lHand�� �Ҵ�
                rHand = handObj.transform;
                // ��Ʈ�ѷ��� ī�޶��� �ڽ� ��ü�� ���
                rHand.parent = Camera.main.transform;
#elif Oculus
                rHand = GameObject.Find("RightControllerAnchor").transform;
#endif
            }
            return rHand;
        }
    }

    // ======= ��ư �Է� ���� ==============
    // ��ư ������ ��
    public static bool Get(Button input, Controller hand = Controller.RController)
    {
#if PC
        return Input.GetButton(((ButtonController)input).ToString());

#elif Oculus
        return OVRInput.Get((OVRInput.Button)input, (OVRInput.Controller)hand);
#endif
    }

    // ��ư ���� ��
    public static bool GetDown(Button input, Controller hand = Controller.RController)
    {
#if PC
        return Input.GetButtonDown(((ButtonController)input).ToString());
#elif Oculus
        return OVRInput.GetDown((OVRInput.Button)input, (OVRInput.Controller)hand);
#endif
    }

    // ��ư �� ��
    public static bool GetUp(Button input, Controller hand = Controller.RController)
    {
#if PC
        return Input.GetButtonUp(((ButtonController)input).ToString());
#elif Oculus
        return OVRInput.GetUp((OVRInput.Button)input, (OVRInput.Controller)hand);
#endif
    }

    // Thumbstick ����
    // ��Ʈ�ѷ��� axis �Է�
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

    // �ʱ� ���忡�� ������� ����, ī�޶� �������� ����
    public static void SetCenter()
    {
#if Oculus
        OVRManager.display.RecenterPose();
#endif
    }

    // ������ ����. ��Ʈ�ѷ� �Ǵ� ���콺�� ����Ű�� ����
    // ray�� ��ٰ� ��ü�� ������ ���η�����(crosshair) �׷��ֱ�
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
                // linerenderer �׸���
                // crosshair
                crosshair.position = ray.GetPoint(distance);
                crosshair.forward = -Camera.main.transform.forward;
                crosshair.localScale = defaultScale * Mathf.Max(1, distance);

            }
            else
            {
                crosshair.position = ray.origin + ray.direction * 100;
                crosshair.forward = -Camera.main.transform.forward;
                distance = (crosshair.position - ray.origin).magnitude;
                crosshair.localScale = defaultScale * Mathf.Max(1, distance);
            }
        }
        else
        {
            ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        }
    }

    // ���� ȣ��
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
        PlayVibration(0.1f, 1, 1, hand);
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

    public static void Recenter()
    {
#if Oculus
        OVRManager.display.RecenterPose();
#endif
    }
}

// ������ ���� �ڷ�ƾ ��ü
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


