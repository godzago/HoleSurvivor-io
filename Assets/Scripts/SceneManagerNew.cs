using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerNew : MonoBehaviour
{
    private static SceneManagerNew instance;
    public static SceneManagerNew Instance { get { return instance; } }

    [SerializeField] private Button nextButton;
    [SerializeField] private Button downButton;

    public static int sceneNumber = 1;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(NextLevel);
        }
        if (downButton != null)
        {
            downButton.onClick.AddListener(DownLevel);
        }
    }

    public void NextLevel()
    {
        if (sceneNumber < 7)
        {
            Debug.Log("" + sceneNumber);
        }
        else
        {
            sceneNumber = Random.Range(1, 7);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneNumber);
    }

    public void DownLevel()
    {
        sceneNumber -= 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneNumber);
    }
}
