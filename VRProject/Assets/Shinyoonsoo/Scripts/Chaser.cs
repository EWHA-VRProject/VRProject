using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chaser : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private Transform target = null;
    
    // Start is called before the first frame update
    void Start()
    {
        if(agent == null)
            if(!TryGetComponent(out agent))
                Debug.LogWarning(name + " needs a navmesh agent");

    }

    // Update is called once per frame
    void Update()
    {
        if(target)
            MoveToTarget();
    }

    private void MoveToTarget()
    {
        agent.SetDestination(target.position);
        agent.isStopped = false;
    }
}
