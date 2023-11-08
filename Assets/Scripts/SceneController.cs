using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private Button downButton;
    [SerializeField] private Button restartButton;

    public static int sceneNumber;

    private void Awake()
    {
        //if (PlayerPrefs.HasKey("sceneNumber") == false)
        //{
        //    PlayerPrefs.SetInt("sceneNumber", 0);
        //}
    }
    //private void Start()
    //{
    //    Debug.Log(PlayerPrefs.GetInt("sceneNumber"));
    //    LoadScene();
    //}

    public void NextLevel()
    {
        sceneNumber += 1;

        PlayerPrefs.SetInt("sceneNumber", sceneNumber);

        Debug.Log("" + sceneNumber);

        if (sceneNumber <= 7)
        {
            SceneManager.LoadScene(sceneNumber);
        }
        else
        {
            SceneManager.LoadScene(Random.Range(1, 7));
        }
    }

    public void DownLevel()
    {
        sceneNumber -= 1;

        Debug.Log("" + sceneNumber);

        SceneManager.LoadScene(sceneNumber);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("sceneNumber"));
    }
}
