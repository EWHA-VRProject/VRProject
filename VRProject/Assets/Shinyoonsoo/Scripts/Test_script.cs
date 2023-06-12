using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test_script : MonoBehaviour
{
    


    NavMeshAgent agent;
    public Animator ani;
    public GameObject aim_point;
    [SerializeField] private Transform player = null;

    public bool execute_walking;
    public bool execute_sitting;

    public bool execute_running;

    public float walk_speed;
    public float run_speed;

    Vector3 sitting_position;
    Vector3 sitting_Rotation;



  
// 캐릭터의 운동상태 나타냄
    public bool walk;
    public bool run;
    public bool sit;
    public bool playerTouched;
//새로운 목표물 유무 판단
    public bool destermine_new_aim;


    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();

        sitting_position = new Vector3(0,0,0);
        sitting_Rotation = new Vector3(180,-90,-90);

        way_points.Clear();
        Sitting_points.Clear();

        GameObject[] waypointsFind = GameObject.FindGameObjectsWithTag("waypoint");
        GameObject[] SittingpointsFind = GameObject.FindGameObjectsWithTag("sittingpoint");


        foreach(GameObject g in waypointsFind)
        {
            way_points.Add(g);
        }
        foreach (GameObject g in SittingpointsFind)
        {
            Sitting_points.Add(g);
        }


    }

// 해당 상태에 있음을 알려주는 지표
    bool in_sitting; // 앉음 상태에 있음
    bool in_stop; // 멈춤 상태에 있음

// 코루틴 변수
    Coroutine sitting_start;
    Coroutine stop_start;

    public GameObject crowbar;

    IEnumerator sitting_down ()
    {
        yield return new WaitForSeconds(0);

        transform.parent = aim_point.transform;
       

      

        Destroy(agent);

        ani.SetInteger("legs", 3);
        ani.SetInteger("arms", 3);

        transform.localPosition = sitting_position;
        transform.localEulerAngles = sitting_Rotation;



        yield return new WaitForSeconds(5);

        agent =  gameObject.AddComponent<NavMeshAgent>();


        in_sitting = false;
        destermine_new_aim = false;
        transform.parent = null;

        StopCoroutine(sitting_start);
    }


    public IEnumerator stop_execute()
    {
        print(11);
        yield return new WaitForSeconds(0);

        print("여기");
        agent.speed = 0;
        ani.SetInteger("legs", 0);
        ani.SetInteger("arms", 0);
        yield return new WaitForSeconds(10); //10초 동안 멈춰놓음

        playerTouched = false;        
        destermine_new_aim = false;
        
    }

    public bool ready;

    void Update()
    {

        if(!ready || player ==null )
        {
            return;
        }

     
        if (Vector3.Distance(transform.position,player.position) < 0.5f) // 플레이어와 닿는 경우
        {
            Debug.Log("Touched"); // 플레이어와 터치되었다고 로그에 띄워줌
            playerTouched = true; // 플레이어와 터치되었다고 표지 켜줌
        }

        if(playerTouched) // 만약 플레이어가 터치했다면
        {
            agent.speed = 0;
            stop_start = StartCoroutine(stop_execute()); // 멈춤 작업 실행
        }
        
        if(!destermine_new_aim)
        {
            int what_to_choose = UnityEngine.Random.Range(0, 2);

           

            walk = false;
            run = false;
            sit = false;



            if(what_to_choose == 0)
            {
                walk = true;

                int Which_point = UnityEngine.Random.Range(0, way_points.Count);
                aim_point = way_points[Which_point].gameObject;
                destermine_new_aim = true;
            }
            if (what_to_choose == 1)
            {
                run = true;

                int Which_point = UnityEngine.Random.Range(0, way_points.Count );
                aim_point = way_points[Which_point].gameObject;
                destermine_new_aim = true;
            }
            if (what_to_choose == 2)
            {
                sit = true;

                int Which_point = UnityEngine.Random.Range(0, Sitting_points.Count );
                aim_point = Sitting_points[Which_point].gameObject;
                destermine_new_aim = true;
            }

        }
        if (destermine_new_aim)
        {
            if (walk) // 걷기 표지에 해당한다면
            {

                if (Vector3.Distance(transform.position,aim_point.transform.position) > 0.25f) // aimpoint와 거리가 0.25 초과일 때 = 포인트에 다다기 전일 때
                {
                   
                    agent.speed = walk_speed; // 해당 human의 speed 는 걷기 속도
                    agent.SetDestination(aim_point.transform.position); // aimpoint로 human을 움직임
                    ani.SetInteger("arms", 1); //애니메이션 설정
                    ani.SetInteger("legs", 1);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f) // aimpoint와 거리가 0.25 미만일 때 = 포인트에 다다를때
                {
                    agent.speed = 0; // 멈춤

                    ani.SetInteger("arms", 5);
                    ani.SetInteger("legs", 5);

                    destermine_new_aim = false; // 다시 aim 정함
                }

            }
            if(run)
            {

                if (Vector3.Distance(transform.position, aim_point.transform.position) > 0.25f) 
                {
                    Debug.Log("going to run");
                    agent.speed = run_speed;
                    agent.SetDestination(aim_point.transform.position);
                    ani.SetInteger("arms", 2);
                    ani.SetInteger("legs", 2);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f)
                {
                    agent.speed = 0;

                    ani.SetInteger("arms", 5);
                    ani.SetInteger("legs", 5);

                    destermine_new_aim = false;
                }

            }
            if(sit && !in_sitting)
            {

                if (Vector3.Distance(transform.position, aim_point.transform.position) > 0.25f) // aimpoint와 거리가 0.25 초과일 때 = 포인트에 다다기 전일 때
                {
                    
                    agent.speed = walk_speed; 
                    agent.SetDestination(aim_point.transform.position); // 해당 포인트 쪽으로 걸어오게 함
                    ani.SetInteger("arms", 1);
                    ani.SetInteger("legs", 1);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f)
                {
                    agent.speed = 0; //멈춤


                    if(!in_sitting)
                    {
                        in_sitting = true;

                        sitting_start = StartCoroutine(sitting_down()); // 앉는 동작 실행
                    }

                }

            }
            

        
        }


        






    }




    public List<GameObject> way_points = new List<GameObject>();
    public List<GameObject> Sitting_points = new List<GameObject>();

  


}
