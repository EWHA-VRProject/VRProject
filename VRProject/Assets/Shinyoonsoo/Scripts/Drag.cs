using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    RaycastHit hit, hitFloor;
    GameObject dragAnchor;
    Vector3 mainCamPosition;

    void Start()
    {
        mainCamPosition = Camera.main.transform.position;
    }

    private void OnMouseUp()
    {
        transform.SetParent(null);
        Destroy(dragAnchor);
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            dragAnchor = new GameObject("DragAnchor");
            dragAnchor.transform.position = hit.point;
            transform.SetParent(dragAnchor.transform);
        }
    }

    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitFloor, Mathf.Infinity, LayerMask.GetMask("Floor")))
        {
            float h = dragAnchor.transform.position.y;

            Vector3 camToFloor = hitFloor.point - Camera.main.transform.position;
            Vector3 nextPosition = Vector3.zero;

            float lo = 0.0f, hi = 1.0f; // ratio
            for (int i = 0; i < 38; i++)
            {
                float diff = hi - lo;
                float p1 = lo + diff / 3;
                float p2 = hi - diff / 3;

                var v1 = mainCamPosition + camToFloor * p1;
                var v2 = mainCamPosition + camToFloor * p2;
                if (Mathf.Abs(v1.y - h) > Mathf.Abs(v2.y - h))
                {
                    nextPosition = v2;
                    lo = p1;
                }
                else
                {
                    nextPosition = v1;
                    hi = p2;
                }
            }

            dragAnchor.transform.position = nextPosition;
        }
    }
}