using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using TMPro;

public class Enemy_AI : MonoBehaviour
{
    [Header("Area")]
    [SerializeField] float minX = -6f;
    [SerializeField] float maxX = 6f;
    [SerializeField] float minZ = -6f;
    [SerializeField] float maxZ = 6f;

    [Header("Enemy Name Take")]
    public string[] enemyNames;
    [SerializeField] TextMeshProUGUI enemynamesText;

    private NavMeshAgent navMeshAgent;
    private HoleManager holeManager;
    bool IamIdleNow = true;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        holeManager = GameObject.Find("HoleDestroyed").GetComponent<HoleManager>();
    }
    private void Start()
    {
        SetEnemyNameText();
    }
    void Update()
    {
        if (PlayerPrefs.GetInt("GameState") == 1)
        {
            IamIdleNow = false;
        }
        // eğer hedefe ulaşmısa yeni hedef belirle
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f && PlayerPrefs.GetInt("GameState") == 1)
        {
            SetRandomDestination();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collison Enter");
        this.gameObject.transform.DORotate(new Vector3(0, 180, 0), 0.30f, RotateMode.WorldAxisAdd);
    }

    void SetRandomDestination()
    {
        if (IamIdleNow == false && holeManager.GameOver == false)
        {
            // Rastgele bir hedef belirle
            Vector3 randomDestination = new Vector3(Random.Range(minX, maxX), 0.25f, Random.Range(minZ, maxZ));

            // Hedefi ayarla
            navMeshAgent.SetDestination(randomDestination);
        }   
    }

    public void SetEnemyNameText()
    {
        enemynamesText.text = enemyNames[Random.Range(0, enemyNames.Length)];
    }
}
