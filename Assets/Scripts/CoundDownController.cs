using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoundDownController : MonoBehaviour
{
    [Header("Timmer")]
    [SerializeField] int countDownTime;
    [SerializeField] GameObject countDownTimeObject;
    [SerializeField] TextMeshProUGUI countDownDisplay;

    [Header("Tutorial")]
    [SerializeField] GameObject bigArrow;
    [SerializeField] TMP_InputField display;

    [Header("Name")]
    [SerializeField] GameObject Menu;
    [HideInInspector] [SerializeField] bool nameEnable = false;
    [SerializeField] TextMeshProUGUI mame;

    //public GameObject bigArrowPortal

    private void Awake()
    {
        if (PlayerPrefs.HasKey("GameState") == false)
        {
            PlayerPrefs.SetInt("GameState", 0);
        }
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("GameState") == 0)
        {
            CreateName();
            Debug.Log("Menu Scren Open");
            //StartCoroutine(CoundDownStart(1f));
            //StartCoroutine(BigArrow(2.5f));
        }
        else
        {
            Debug.Log("Menu Scren Close");
            countDownTimeObject.SetActive(false);
            bigArrow.SetActive(false);
            Menu.SetActive(false);
            CloseMenu();
            //bigArrowPortal.SetActive(false);
        }
    }
   
    IEnumerator CoundDownStart(float t)
    {
        while (countDownTime > 0)
        {
            countDownDisplay.text = countDownTime.ToString();

            yield return new WaitForSeconds(t);

            countDownTime--;
        }
        countDownDisplay.text = "GO!";

        PlayerPrefs.SetInt("GameState", 1);
        PlayerPrefs.Save();

        countDownDisplay.gameObject.SetActive(false);
        yield return new WaitForSeconds(t);
    }

    IEnumerator BigArrow(float t)
    {
        yield return new WaitForSeconds(t);
        bigArrow.SetActive(false);
        //bigArrowPortal.SetActive(false);
    }

    public void CreateName()
    {
        Menu.SetActive(true); 
        PlayerPrefs.SetString("user_name", display.text);
        PlayerPrefs.Save();
        nameEnable = true;
    }

    public void CloseMenu()
    {
        Menu.SetActive(false);
        mame.text = PlayerPrefs.GetString("user_name");
        StartCoroutine(CoundDownStart(1f));
        StartCoroutine(BigArrow(2.5f));
    }
}
