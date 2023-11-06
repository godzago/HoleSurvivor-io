using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopSettings : MonoBehaviour
{
    [Header("objects")]
    [SerializeField] private GameObject slot;
    [SerializeField] private List<Items> items;
    [SerializeField] private GameObject[] caps;
    [SerializeField] private Image mainSlot;
    [SerializeField] private Text PriceText;
    private HoleManager holeManager;
    private bool isSave = false;

    [Header("Money")]
    private int TotalMoney;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Item") == false)
        {
            PlayerPrefs.SetInt("Item", 0);
        }
    }
    private void Start()
    {
        holeManager = GameObject.Find("HoleDestroyed").GetComponent<HoleManager>();

        TotalMoney = PlayerPrefs.GetInt("coins");
        Debug.Log("PARA : " + TotalMoney);

        mainSlot.sprite = items[0].capObject;
        if (PlayerPrefs.HasKey("Cap"))
        {
            caps[PlayerPrefs.GetInt("Cap")].SetActive(true);
        }
        for (int i = 0; i < items.Count; i++)
        {
            if (PlayerPrefs.GetInt("Item" + i) == 1)
            {
                items[i].buy = true;
                Debug.Log(items[i].buy + " " + i);
            }
        }
        CallTheMarket();
        items[PlayerPrefs.GetInt("Cap")].gameObject.transform.Find("click").transform.Find("text2").GetComponent<Text>().text = "Geydi";

        UIController.Instance.ShopArea.SetActive(false);
    }
    private void LateUpdate()
    {
        if (holeManager.GameOver == true && !isSave)
        {
            TotalMoney = PlayerPrefs.GetInt("coins");
            isSave = true;
        }
    }
    void CallTheMarket()
    {
        for (int i = 0; i < items.Count; i++)
        {
            GameObject growingSlot = Instantiate(slot, transform);
            growingSlot.transform.Find("click").transform.Find("text").transform.Find("PriceText").GetComponent<Text>().text = items[i].price.ToString();
            GameObject rasim = growingSlot.transform.Find("Rasim").gameObject;
            rasim.GetComponentInChildren<Image>().sprite = items[i].capObject;          
            items[i].gameObject = growingSlot;

            if (!items[i].buy)
            {
                rasim.GetComponentInChildren<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.25f);
                growingSlot.transform.Find("click").transform.Find("text2").gameObject.SetActive(false);

            }
            else
            {
               // growingSlot.transform.Find("click").transform.Find("text").GetComponent<Text>().text = "PURCHASED";
                growingSlot.transform.Find("click").transform.Find("text").gameObject.SetActive(false);
                growingSlot.transform.Find("click").transform.Find("text2").gameObject.SetActive(true);

            }
            int temporary = i;
            growingSlot.transform.Find("click").transform.GetComponent<Button>().onClick.AddListener(() => Buying(temporary, growingSlot));
        }
    }
    public void Buying(int id, GameObject obje)
    {
        if (TotalMoney >= items[id].price && !items[id].buy)
        {
            mainSlot.sprite = items[id].capObject;
            GameObject objectss = GameObject.Find("Cap");
            if (objectss != null)
            {
                objectss.SetActive(false);
            }
            caps[id].SetActive(true);
            PlayerPrefs.SetInt("Cap", id);
            obje.transform.Find("click").transform.Find("text").gameObject.SetActive(false);
            obje.transform.Find("click").transform.Find("text2").gameObject.SetActive(true);
            obje.transform.Find("Rasim").GetComponentInChildren<Image>().color = Color.white;
            obje.transform.Find("Rasim").transform.DOShakeScale(1f,0.5f);
            TotalMoney -= items[id].price;
            PlayerPrefs.SetInt("coins", TotalMoney);
            Debug.Log("GÜNCEL PARA  : " + TotalMoney);
            items[id].buy = true;
            PlayerPrefs.SetInt("Item"+id,1);
        }
        else
        {
            GameObject objectss = GameObject.Find("Cap");
            objectss.SetActive(false);
            caps[id].SetActive(true);
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].buy)
                {
                    items[i].gameObject.transform.Find("click").transform.Find("text2").GetComponent<Text>().text = "PUT ON";
                }
            }
            obje.transform.Find("click").transform.Find("text2").GetComponent<Text>().text = "GEYDİ";

        }
        if (items[id].buy)
        {
            mainSlot.sprite = items[id].capObject;
        }
    }
}

[System.Serializable]
public class Items
{
    public string name;
    public Sprite capObject;
    public int price;
    public bool buy;
    public GameObject gameObject;
    public Items(string Name, Sprite CapObject, int Price, bool Buy, GameObject gameObject)
    {
        name = Name;
        capObject = CapObject;
        price = Price;
        buy = Buy;
        gameObject = gameObject;
    }
}
