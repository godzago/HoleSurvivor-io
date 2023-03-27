using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : MonoBehaviour
{
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.GameStart.Invoke();
            gameObject.SetActive(false);
        }
    }
}
