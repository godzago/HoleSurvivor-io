using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoadPanel : MonoBehaviour
{
    private LevelManager levelManager;
    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void NextLevel()
    {
        levelManager.ShowLoadPanel();
    }

    public void DeActive()
    {
        gameObject.SetActive(false);
    }
}
