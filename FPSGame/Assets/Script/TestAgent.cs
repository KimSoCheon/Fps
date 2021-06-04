using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class TestAgent : MonoBehaviour
{
    public Transform targetPosition;
    public NavMeshAgent meshAgent;
    void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        meshAgent.destination = targetPosition.position;
    }
}
