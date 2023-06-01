using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckAnswer : MonoBehaviour
{
    // 정답 체크
    // 정답 오브젝트를 찾아서(tag로)
    // 만약 정답 조건의 오브젝트가 SetActive 가 true면
    // Tag를 Answer로 변경해준다

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
        answer = GameObject.FindWithTag("Answer");  // 정답 게임오브젝트
        answerChildren = answer.GetComponentsInChildren<Transform>(true);

        child = this.GetComponentsInChildren<Transform>(true);  // 나의 자식 오브젝트 모두

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
            print("정답");
        }

    }
}
