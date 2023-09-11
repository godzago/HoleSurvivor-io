using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoundDownController : MonoBehaviour
{
    [Header("Timmer")]
    public int countDownTime;
    public GameObject countDownTimeObject;
    public TextMeshProUGUI countDownDisplay;

    [Header("Tutorial")]
    public GameObject bigArrow;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("GameStart") == 0)
        {
            StartCoroutine(CoundDownStart(1f));
            StartCoroutine(BigArrow(2.5f));
        }
        else
        {
            countDownTimeObject.SetActive(false);
            bigArrow.SetActive(false);
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
    }
}
