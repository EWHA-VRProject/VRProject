using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckAnswer : MonoBehaviour
{
    // ���� üũ
    // ���� ������Ʈ�� ã�Ƽ�(tag��)
    // ���� ���� ������ ������Ʈ�� SetActive �� true��
    // Tag�� Answer�� �������ش�

    public GameObject answer;
    public Transform[] answerChildren;
    public Transform[] child;
    public GameObject childAns;
    GameObject humanMesh;
    GameObject humanMeshAns;
    public GameObject beard;
    public GameObject beardAns;
    public GameObject cloth;
    public GameObject clothAns;
    public GameObject hair;
    public GameObject hairAns;

    public List<bool> isAnsList = new List<bool>();
    public List<bool> isList = new List<bool>();

    public List<bool> isListChange = new List<bool>();

    void Start()
    {
        answer = GameObject.FindWithTag("Answer");  // ���� ���ӿ�����Ʈ
        answerChildren = answer.GetComponentsInChildren<Transform>(true);

        child = this.GetComponentsInChildren<Transform>(true);  // ���� �ڽ� ������Ʈ ���

        foreach (Transform child in answerChildren)
        {
            isAnsList.Add(child.gameObject.activeSelf);
        }

        foreach (Transform child in child)
        {
            isList.Add(child.gameObject.activeSelf);
        }
        /*        child = transform.GetChild(0).gameObject;
                childAns = answer.transform.GetChild(0).gameObject;

                humanMesh = child.transform.GetChild(0).gameObject;

                beard = humanMesh.transform.Find("beard").gameObject;

                cloth = humanMesh.transform.Find("cloth").gameObject;

                hair = humanMesh.transform.Find("hair").gameObject;*/

    }

    void Update()
    {
        GetTheAnswer();
        CheckTheAnswer();

        /*        if (cloth.transform.Find("cap").gameObject.activeSelf && cloth.transform.Find("jacket").gameObject.activeSelf && cloth.transform.Find("shortpants").gameObject.activeSelf)
                {
                    gameObject.tag = "Answer";
                }*/


    }

    void GetTheAnswer()
    {
        isAnsList.Clear();

        for (int i = 0; i < isList.Count; i++)
        {
            isAnsList.Add(child[i].gameObject.activeSelf);

        }
    }
    void CheckTheAnswer()
    {
        isListChange.Clear();

        for (int i = 0; i < isList.Count; i++)
        {
            isListChange.Add(child[i].gameObject.activeSelf);

        }


        if (isListChange.SequenceEqual(isAnsList))
        {
            print("����");
        }

    }
}
