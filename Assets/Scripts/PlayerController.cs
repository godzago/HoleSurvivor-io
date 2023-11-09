using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("Money Settings")]
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] GameObject MoneyUIPrefeb;
    [SerializeField] GameObject GoldPanel;
    [SerializeField] GameObject MoneyPrefeb;

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
    private bool isOpenTheGate = false;

    [SerializeField] [HideInInspector] private int maxHealth;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] public int[] levelGoals;

    int money;
    [HideInInspector] public int TotalMoney;
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

        TotalMoney = PlayerPrefs.GetInt("coins");
    }
    private void Start()
    {
        LevelCase(PlayerPrefs.GetInt("SceneNumber"));
        Debug.Log("" + maxHealth);
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
            //Debug.Log("Win");
            //holeManagar.StartCoroutine("WinPanel" , 1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Money"))
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(ActivateAfterDelay(3));
            AudioManager.Instance.PlaySFX("Coin");
            
            //other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("portal") && money >=100)
        {
            this.gameObject.transform.DOMoveY(15, 1f);
            holeManagar.StartCoroutine("WinPanel", 1f);
        }

        if (other.gameObject.CompareTag("MoneyHolder"))
        {
            AddCountCoins(moneyCollect.NumOfItemsHolding * 15);
            
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
            if (money >= maxHealth && !isOpenTheGate)
            {
                TotalMoney -= maxHealth;
                TextToStirng();
                Debug.Log("TOTAL GATE MONEY : " + TotalMoney);
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
                isOpenTheGate = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyObscatial"))
        {
            if (moneyCollect.NumOfItemsHolding > 0)
            {
                moneyCollect.NumOfItemsHolding -= 1;
                Transform item = moneyCollect.stackedMoney[moneyCollect.NumOfItemsHolding];
                item.parent = null;
                item.DOMove(collision.gameObject.transform.position, 0.5f).OnComplete(() => item.gameObject.SetActive(false));
                if (moneyCollect.NumOfItemsHolding == 0)
                {
                    moneyCollect.stackedMoney.Clear();
                }
            }

            TextToStirng();
            SliderSettings();   
            Camera.main.GetComponent<Shake>().StartShake();           
        }

        if (collision.gameObject.CompareTag("EnemyAI"))
        {
            Vector3 valueDistance = collision.transform.position - gameObject.transform.position;
            float valueZ = Random.Range(-1f, 1f);
            gameObject.transform.DOShakeScale(duraiton, strenght, vibrato, randomness);
            gameObject.transform.DOMove(transform.position + new Vector3(valueZ, 0 , valueZ), 0.3f);
        }
    }  

    void SliderSettings()
    {
        //float currnetScore = Mathf.SmoothDamp(processingBar.value, money, ref currnetVelecotiy, 50 * Time.deltaTime);
        processingBar.value = money;
    }

    public void AddCountCoins(int amount)
    {
        money += amount;
        TotalMoney += amount;
    }
    public void TextToStirng()
    {

        ScoreText.text = TotalMoney.ToString();

        text.text = money + "/" + maxHealth;
    }  
    public void SaveMoneyData()
    {
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
        maxHealth = levelGoals[levelCount];

        //switch (levelCount)
        //{
        //    case 0:
        //        maxHealth = 100;
        //        break;
        //    case 1:
        //        maxHealth = 125;
        //        break;
        //    case 2:
        //        maxHealth = 150;
        //        break;
        //    case 3:
        //        maxHealth = 175;
        //        break;
        //    case 4:
        //        maxHealth = 200;
        //        break;
        //    case 5:
        //        maxHealth = 225;
        //        break;
        //    case 6:
        //        maxHealth = 250;
        //        break;
        //    case 7:
        //        maxHealth = 300;
        //        break;
        //    case 8:
        //        maxHealth = 325;
        //        break;
        //    case 9:
        //        maxHealth = 350;
        //        break;
        //}
    }
}


