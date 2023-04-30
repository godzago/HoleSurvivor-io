using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class HoleManager : MonoBehaviour
{
    public bool GameOver;
    [SerializeField] UIController uIController;

    private void Awake()
    {
        GameOver = false;
        Time.timeScale = 1;
        AudioManager.Instance.PlayMusic("Theme");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }

        if (other.gameObject.CompareTag("EnemyAI"))
        {
            other.gameObject.GetComponent<NavMeshAgent>().transform.DOMove(new Vector3(transform.position.x, transform.position.y - 5, transform.position.z), 2);
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
}
