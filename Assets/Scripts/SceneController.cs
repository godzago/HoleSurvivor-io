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

    public int sceneNumber;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SceneNumber") == false)
        {
            PlayerPrefs.SetInt("SceneNumber", 0);
        }
        else
        {
            sceneNumber = PlayerPrefs.GetInt("SceneNumber");
        }

    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != sceneNumber)
        {
            LoadScene();
        }
    }

    public void NextLevel()
    {
        sceneNumber += 1;

        PlayerPrefs.SetInt("SceneNumber", sceneNumber);

        Debug.Log("" + sceneNumber);

        if (sceneNumber <= 9)
        {
            LoadScene();
        }
        else
        {
            SceneManager.LoadScene(Random.Range(2,9));
        }
    }

    public void DownLevel()
    {
        sceneNumber -= 1;

        PlayerPrefs.SetInt("SceneNumber", sceneNumber);

        Debug.Log("" + sceneNumber);

        LoadScene();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
