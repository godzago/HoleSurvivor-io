using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class HoleManager : MonoBehaviour
{
    public bool GameOver = false;

    [Header("Scripts")]
    [SerializeField] UIController uIController;

    public GameObject[] EnemyNumber;

    int enemyCount;
    int GameStart;

    private void Awake()
    {
        AudioManager.Instance.PlayMusic("Theme");

        Time.timeScale = 1;

        if (PlayerPrefs.HasKey(nameof(GameStart)) == false)
        {
            PlayerPrefs.SetInt(nameof(GameStart), 0);
        }
    }
    private void Start()
    {
        EnemyNumber = GameObject.FindGameObjectsWithTag("EnemyAI");

        enemyCount = EnemyNumber.Length;
    }

    private void LateUpdate()
    {
         if (enemyCount <= 0)
         {
            StartCoroutine(WinPanel(1.5f));
         }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyAI"))
        {
            other.gameObject.GetComponent<NavMeshAgent>().transform.DOMove(new Vector3(transform.position.x, transform.position.y - 5, transform.position.z), 1.5f).OnComplete(() => Destroy(other.gameObject));
            enemyCount += -1;
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.DOMove(new Vector3(transform.position.x, transform.position.y - 5, transform.position.z), 2);
            GameOver = true;
            StartCoroutine(ActivateAfterDelay(1));        
        }
    }
     
    IEnumerator ActivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(1);
        uIController.LosePanel.SetActive(true);
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlaySFX("Lose");
        Time.timeScale = 0;
    }

    IEnumerator WinPanel(float t)
    {
        yield return new WaitForSeconds(t);
        uIController.WinPanel.SetActive(true);
        GameOver = true;
        Debug.Log("WİN");
    }
}
