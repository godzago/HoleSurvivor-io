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
    [SerializeField] GameObject Menu;
    [SerializeField] TMP_InputField display;
    [SerializeField] bool nameEnable = false;
    //public GameObject bigArrowPortal;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("GameStart") == 0)
        {
            CreateName();
            //StartCoroutine(CoundDownStart(1f));
            //StartCoroutine(BigArrow(2.5f));
        }
        else
        {
            countDownTimeObject.SetActive(false);
            bigArrow.SetActive(false);
            Menu.SetActive(false);
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

        countDownDisplay.text = "GO";

        PlayerPrefs.SetInt("GameStart", 1);

        yield return new WaitForSeconds(t);

        countDownDisplay.gameObject.SetActive(false);
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
        StartCoroutine(CoundDownStart(1f));
        StartCoroutine(BigArrow(2.5f));
    }
}
