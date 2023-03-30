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

    [SerializeField] GameObject MoneyUIPrefeb;
    [SerializeField] GameObject GoldPanel;

    [SerializeField] GameObject MoneyPrefeb;

    [SerializeField] ParticleSystem ParticleSystem;

    [SerializeField] AudioClip _clip;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyObscatial"))
        {
            money -= 5;
            ScoreUpdate();
            Camera.main.GetComponent<Shake>().StartShake();
            ParticleSystem.Play();
            Invoke("StopParticleSystem", 0.5f);
        }

        if (other.gameObject.CompareTag("Money"))
        {
            money += 5;
            ScoreUpdate();
            other.gameObject.SetActive(false);
            StartCoroutine(ActivateAfterDelay(3));
            Instantiate(MoneyUIPrefeb, Camera.main.WorldToScreenPoint(transform.position), GoldPanel.transform.rotation, GoldPanel.transform);
            SoundManager.Instance.PlaySound(_clip);

        }

    }
    void StopParticleSystem()
    {
        ParticleSystem.Stop();
    }
    public void ScoreUpdate()
    {

        ScoreText.text = money.ToString();
    }

    IEnumerator ActivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(3); // Belirtilen gecikme süresini bekleyin

        Vector3 spawnPosition = new Vector3(Random.Range(-4, 4), Random.Range(0, 0), Random.Range(-4, 4));

        Instantiate(MoneyPrefeb, spawnPosition, Quaternion.identity);
    }
}


