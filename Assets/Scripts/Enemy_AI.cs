﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
    

public class Enemy_AI : MonoBehaviour
{
    public float minX = -6f;
    public float maxX = 6f;
    public float minZ = -6f; 
    public float maxZ = 6f;

    private NavMeshAgent navMeshAgent;
    [Header("Scripts")]
    [SerializeField] HoleManager holeManager;
    bool IamIdleNow = true;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        holeManager = GameObject.Find("HoleDestroyed").GetComponent<HoleManager>();
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("GameStart") == 1)
        {
            IamIdleNow = false;
        }
        // eğer hedefe ulaşmısa yeni hedef belirle
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f && PlayerPrefs.GetInt("GameStart") == 1)
        {
            SetRandomDestination();
        }
    }

    void SetRandomDestination()
    {
        if (IamIdleNow == false && holeManager.GameOver == false)
        {
            // Rastgele bir hedef belirle
            Vector3 randomDestination = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));

            // Hedefi ayarla
            navMeshAgent.SetDestination(randomDestination);
        }   
    }
}
