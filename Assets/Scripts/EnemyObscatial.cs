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

    int money = 10;
    [SerializeField] TextMeshProUGUI ScoreText;

    [SerializeField] GameObject MoneyUIPrefeb;
    [SerializeField] GameObject GoldPanel;

    [SerializeField] GameObject MoneyPrefeb;

    [SerializeField] ParticleSystem ParticleSystem;

    [SerializeField] Rigidbody rgb;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("coins") == false)
        {
            PlayerPrefs.SetInt("coins", 0);
        }
    }
    private void Start()
    {
        money = PlayerPrefs.GetInt("coins");
    }
    private void LateUpdate()
    {
        if (money < 0)
        {
            uIController.LosePanel.SetActive(true);            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Money"))
        {
            AddCountCoins(+10);
            other.gameObject.SetActive(false);
            StartCoroutine(ActivateAfterDelay(3));
            Instantiate(MoneyUIPrefeb, Camera.main.WorldToScreenPoint(transform.position), GoldPanel.transform.rotation, GoldPanel.transform);
            AudioManager.Instance.PlaySFX("Coin");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyObscatial"))
        {
            AddCountCoins(-15);
            Camera.main.GetComponent<Shake>().StartShake();           
            ParticleSystem.Play();
            Invoke("StopParticleSystem", 0.5f);
        }

        if (collision.gameObject.CompareTag("EnemyAI"))
        {
            Vector3 valueDistance = collision.transform.position - gameObject.transform.position;
            float valueZ = Random.Range(-1f, 1f);

            gameObject.transform.DOMove(transform.position + new Vector3(valueZ, 0 , valueZ), 0.3f);            
        }
    }  

    void StopParticleSystem()
    {
        ParticleSystem.Stop();
    }

    public void AddCountCoins(int amount)
    {
        money += amount;
        ScoreText.text = money.ToString();
        PlayerPrefs.SetInt("coins", money);
    }

    IEnumerator ActivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(3);

        Vector3 spawnPosition = new Vector3(Random.Range(-4, 4), Random.Range(0, 0), Random.Range(-4, 4));

        Instantiate(MoneyPrefeb, spawnPosition, Quaternion.identity);
    }
}


