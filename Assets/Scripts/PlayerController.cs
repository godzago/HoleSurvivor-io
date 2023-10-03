using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] GameObject MoneyUIPrefeb;
    [SerializeField] GameObject GoldPanel;
    [SerializeField] GameObject MoneyPrefeb;
    [SerializeField] ParticleSystem ParticleSystem;

    [Header("Scripts")]
    [SerializeField] UIController uIController;
    private HoleManager holeManagar;
    private MoneyCollect moneyCollect;

    [Header("DOTWeen Settings")]
    [SerializeField] private float duraiton;
    [SerializeField] private float strenght;
    [SerializeField] private int vibrato;
    [SerializeField] private float randomness;

    [Header("Processin Area ")]
    [SerializeField] GameObject portalObject;
    [SerializeField] Slider processingBar;
    [SerializeField] GameObject portalArrow;

    [SerializeField] [HideInInspector] private int maxHealth;
    [SerializeField] private TextMeshProUGUI text;

    int money = 10;
    private Rigidbody rgb;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("coins") == false)
        {
            PlayerPrefs.SetInt("coins", 0);
        }
        if (PlayerPrefs.HasKey("PortalArrow") == false)
        {
            PlayerPrefs.SetInt("PortalArrow", 0);
        }

        rgb = GetComponent<Rigidbody>();

        holeManagar = GameObject.Find("HoleDestroyed").GetComponent<HoleManager>();

        moneyCollect = GetComponent<MoneyCollect>();
    }
    private void Start()
    {
        LevelCase(SceneController.sceneNumber);
        Debug.Log("" + maxHealth);
        //money = PlayerPrefs.GetInt("coins");
        processingBar.maxValue = maxHealth;
        TextToStirng();
    }

    private void FixedUpdate()
    {
        if (money < 0)
        {
            Debug.Log("Loss");
            uIController.LosePanel.SetActive(true);            
        }
        else if (money > maxHealth)
        {
            Debug.Log("Win");
            //holeManagar.StartCoroutine("WinPanel" , 1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Money"))
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            AddCountCoins(+15);
            StartCoroutine(ActivateAfterDelay(3));
            AudioManager.Instance.PlaySFX("Coin");
            
            //other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("portal") && money >=100)
        {
            this.gameObject.SetActive(false);
            holeManagar.StartCoroutine("WinPanel", 1f);
        }

        if (other.gameObject.CompareTag("MoneyHolder"))
        {
            foreach (var item in moneyCollect.stackedMoney)
            {
                item.parent = null;
                item.DOMove(other.transform.position, 0.5f).OnComplete(() => item.gameObject.SetActive(false));
                //Instantiate(MoneyUIPrefeb, Camera.main.WorldToScreenPoint(transform.position), GoldPanel.transform.rotation, GoldPanel.transform);
                TextToStirng();                   
            }

            SliderSettings();

            moneyCollect.stackedMoney.Clear();
            moneyCollect.NumOfItemsHolding = 0;

            // bu if win olup olmadığını kontrol ediyor ve  arrow animasyonunu kontorle edşyor
            if (money >= maxHealth)
            {              
                portalObject.SetActive(true);
                if (PlayerPrefs.GetInt("PortalArrow") == 0)
                {
                    PlayerPrefs.SetInt("PortalArrow", 2);
                    StartCoroutine(PortalArrow(25f));
                }
                else
                {
                    portalArrow.SetActive(false);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyObscatial"))
        {
            AddCountCoins(-15);
            TextToStirng();
            SliderSettings();   
            Camera.main.GetComponent<Shake>().StartShake();           
            ParticleSystem.Play();
            Invoke("StopParticleSystem", 0.5f);
        }

        if (collision.gameObject.CompareTag("EnemyAI"))
        {
            Vector3 valueDistance = collision.transform.position - gameObject.transform.position;
            float valueZ = Random.Range(-1f, 1f);
            gameObject.transform.DOShakeScale(duraiton, strenght, vibrato, randomness);
            gameObject.transform.DOMove(transform.position + new Vector3(valueZ, 0 , valueZ), 0.3f);
        }
    }  

    void StopParticleSystem()
    {
        ParticleSystem.Stop();
    }
    void SliderSettings()
    {
        //float currnetScore = Mathf.SmoothDamp(processingBar.value, money, ref currnetVelecotiy, 50 * Time.deltaTime);
        processingBar.value = money;
    }

    public void AddCountCoins(int amount)
    {
        money += amount;
        //ScoreText.text = money.ToString();
        //PlayerPrefs.SetInt("coins", money);
    }
    public void TextToStirng()
    {
        ScoreText.text = money.ToString();
        text.text = money + "/" + maxHealth;
    }  
    public void SaveMoneyData(int TotalMoney)
    {
        money += TotalMoney;
        PlayerPrefs.SetInt("coins", TotalMoney);
    }   
    IEnumerator PortalArrow(float t)
    {
        yield return new WaitForSeconds(t);
        portalArrow.SetActive(false);
    }

    IEnumerator ActivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(3);

        Vector3 spawnPosition = new Vector3(Random.Range(-4, 4), Random.Range(0, 0), Random.Range(-4, 4));

        Instantiate(MoneyPrefeb, spawnPosition, Quaternion.identity);
    }

    void LevelCase(int levelCount)
    {
        SceneController.sceneNumber = levelCount;

        switch (levelCount)
        {
            case 0:
                maxHealth = 100;
                break;
            case 1:
                maxHealth = 125;
                break;
            case 2:
                maxHealth = 150;
                break;
            case 3:
                maxHealth = 175;
                break;
            case 4:
                maxHealth = 200;
                break;
            case 5:
                maxHealth = 225;
                break;
            case 6:
                maxHealth = 250;
                break;
            case 7:
                maxHealth = 300;
                break;
        }
    }
}


