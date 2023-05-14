using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]

public class Enemy_AI : MonoBehaviour
{
    public float minX = -6f; // AI'nýn sol sýnýrý
    public float maxX = 6f; // AI'nýn sað sýnýrý
    public float minZ = -6f; // AI'nýn alt sýnýrý
    public float maxZ = 6f; // AI'nýn üst sýnýrý

    private NavMeshAgent navMeshAgent; // AI'nýn NavMeshAgent bileþeni
    private Animator animator;
    [SerializeField] GameObject tutorialObject;
    bool IamIdleNow = true;

    void Start()
    {
        // NavMeshAgent bileþenini al
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            IamIdleNow = false;
        }

        // Eðer hedefe ulaþýlmýþsa yeni hedef belirle
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            SetRandomDestination();
        }
    }

    void SetRandomDestination()
    {
        if (IamIdleNow == false)
        {
            AnimationClose();
            tutorialObject.SetActive(false);
            // Rastgele bir hedef belirle
            Vector3 randomDestination = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));

            // Hedefi ayarla
            navMeshAgent.SetDestination(randomDestination);
        }
        else
        {
            AnimationOpen();
            tutorialObject.SetActive(true);
        }
        
    }

    void AnimationOpen()
    {
        animator.SetBool("IdleOn", true);
    }

    void AnimationClose()
    {
        animator.SetBool("IdleOn", false);
    }

}
