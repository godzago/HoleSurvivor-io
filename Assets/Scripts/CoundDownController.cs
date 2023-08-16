using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoundDownController : MonoBehaviour
{

    public int countDownTime;
    public TextMeshProUGUI countDownDisplay;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("GameStart") == 0)
        {
            StartCoroutine(CoundDownStart());
        }
    }

    IEnumerator CoundDownStart()
    {
        while (countDownTime > 0)
        {
            countDownDisplay.text = countDownTime.ToString();

            yield return new WaitForSeconds(1f);

            countDownTime--;
        }

        countDownDisplay.text = "GO";

        PlayerPrefs.SetInt("GameStart", 1);

        yield return new WaitForSeconds(1f);

        countDownDisplay.gameObject.SetActive(false);
    }

}
