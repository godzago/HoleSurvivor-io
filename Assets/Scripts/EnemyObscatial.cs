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

    [SerializeField] HoleManager hole;


    [SerializeField] AudioClip _clip;

    [SerializeField] Rigidbody rgb;


    

    private void Awake()
    {
        hole = FindObjectOfType<HoleManager>();

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
            money += 5;
            ScoreUpdate();
            other.gameObject.SetActive(false);
            StartCoroutine(ActivateAfterDelay(3));
            Instantiate(MoneyUIPrefeb, Camera.main.WorldToScreenPoint(transform.position), GoldPanel.transform.rotation, GoldPanel.transform);
            SoundManager.Instance.PlaySound(_clip);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyObscatial"))
        {
            money -= 5;
            ScoreUpdate();
            Camera.main.GetComponent<Shake>().StartShake();
            ParticleSystem.Play();
            Invoke("StopParticleSystem", 0.5f);
        }

        if (collision.gameObject.CompareTag("EnemyAI"))
        {
            Vector3 valueDistance = collision.transform.position - gameObject.transform.position;
            float valueZ = Random.Range(-2f, 2f);
            //gameObject.GetComponent<Rigidbody>().AddForce(valueDistance * Random.Range(-100,100));

            gameObject.transform.DOMove(transform.position + new Vector3(valueZ, 0 , valueZ), 0.3f);            
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


