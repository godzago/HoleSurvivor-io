using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyObscatial : MonoBehaviour
{
    [SerializeField] UIController uIController;

    int money;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] GameObject MoneyPrefeb;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyObscatial"))
        {
            uIController.LosePanel.SetActive(true);
        }

        if (other.gameObject.CompareTag("Money"))
        {
            money += 5;
            ScoreUpdate();
            other.gameObject.SetActive(false);
            StartCoroutine(ActivateAfterDelay(2));
            Instantiate(MoneyPrefeb, Camera.main.WorldToScreenPoint(transform.position), ScoreText.transform.rotation, ScoreText.transform);

        }

    }
    public void ScoreUpdate()
    {

        ScoreText.text = money.ToString();
    }

    IEnumerator ActivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(2); // Belirtilen gecikme süresini bekleyin

        Vector3 spawnPosition = new Vector3(Random.Range(-4, 4), Random.Range(0, 0), Random.Range(-4, 4));

        Instantiate(MoneyPrefeb, spawnPosition, Quaternion.identity);
    }
}


