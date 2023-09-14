using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]

public class Hole_AI : MonoBehaviour
{
    [SerializeField] float minX = -6f;
    [SerializeField] float maxX = 6f;
    [SerializeField] float minZ = -6f;
    [SerializeField] float maxZ = 6f;

    NavMeshAgent navMeshAgent;
    HoleManager holeManager;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        holeManager = GameObject.Find("HoleDestroyed").GetComponent<HoleManager>();
    }

    void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f && PlayerPrefs.GetInt("GameStart") == 1)
        {
            SetRandomDestination();
        }
    }

    void SetRandomDestination()
    {
        if (holeManager.GameOver == false)
        {
            Vector3 randomDestination = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));
            navMeshAgent.SetDestination(randomDestination);
        }
        else
        {
            navMeshAgent.speed = 0;
        }
    }
}
