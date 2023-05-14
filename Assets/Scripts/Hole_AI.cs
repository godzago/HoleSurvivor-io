using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hole_AI : MonoBehaviour
{
    public float minX = -6f; // AI'nýn sol sýnýrý
    public float maxX = 6f; // AI'nýn sað sýnýrý
    public float minZ = -6f; // AI'nýn alt sýnýrý
    public float maxZ = 6f; // AI'nýn üst sýnýrý

    private NavMeshAgent navMeshAgent; // AI'nýn NavMeshAgent bileþeni

    void Start()
    {
        // NavMeshAgent bileþenini al
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Eðer hedefe ulaþýlmýþsa yeni hedef belirle
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            SetRandomDestination();
        }
    }

    void SetRandomDestination()
    {
        // Rastgele bir hedef belirle
        Vector3 randomDestination = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));

        // Hedefi ayarla
        navMeshAgent.SetDestination(randomDestination);
    }
}
