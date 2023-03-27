using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;


public class UIController : MonoBehaviour
{
    [SerializeField] public GameObject WinPanel, LosePanel, InGamePanel, TutorialPanel;
    [SerializeField] private TextMeshProUGUI moneyText, incMoney;
    [SerializeField] private List<string> moneyMulti = new();
    [SerializeField] private GameObject coin;

    private Canvas UICanvas;

    private Button Next, Restart;

    private LevelManager levelManager;

    private Settings settings;

    private bool coinanim;

    [SerializeField] Slider bar;
    [SerializeField] TextMeshProUGUI TimmerText;
    [SerializeField] float totalTime;

    private float timeLeft;
    private void Awake()
    {
        ScriptInitialize();
        ButtonInitialize();
        
    }

    private void Start()
    {
        GameManager.Instance.OnMoneyChange.Invoke();
        timeLeft = totalTime;
        bar.minValue = 0f;
        bar.maxValue = totalTime;
        bar.value = timeLeft; 
    }

    private void Update()
    {
        Timmer();
    }

    void ScriptInitialize()
    {
        levelManager = FindObjectOfType<LevelManager>();
        settings = FindObjectOfType<Settings>();
        UICanvas = GetComponentInParent<Canvas>();
    }

    void ButtonInitialize()
    {
        Next = WinPanel.GetComponentInChildren<Button>();
        Restart = LosePanel.GetComponentInChildren<Button>();

        Next.onClick.AddListener(() => levelManager.LoadLevel(1));
        Restart.onClick.AddListener(() => levelManager.LoadLevel(0));
    }

    void ShowPanel(GameObject panel, bool canvasMode = false)
    {
        panel.SetActive(true);
        GameObject panelChild = panel.transform.GetChild(0).gameObject;
        panelChild.transform.localScale = Vector3.zero;
        panelChild.SetActive(true);
        panelChild.transform.DOScale(Vector3.one, 0.5f);

        UICanvas.worldCamera = Camera.main;
        UICanvas.renderMode = canvasMode ? RenderMode.ScreenSpaceCamera : RenderMode.ScreenSpaceOverlay;
    }

    void GameReady()
    {
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
        InGamePanel.SetActive(true);
        ShowTutorial();
    }

    void SetMoneyText()
    {
        coin.transform.DORewind();
        coin.transform.DOShakeScale(1);

        incMoney.gameObject.SetActive(true);

        incMoney.text = GameManager.Instance.incMoney > 0 ? "+" + GameManager.Instance.incMoney : GameManager.Instance.incMoney.ToString();

        CancelInvoke(nameof(incMoneyFalse));
        Invoke(nameof(incMoneyFalse), 1.5f);

        var moneyDigit = Math.Floor(Math.Log10(GameManager.Instance.PlayerMoney) + 1);

        if (moneyDigit < 4)
        {
            moneyText.text = GameManager.Instance.PlayerMoney.ToString();
        }
        else
        {
            var temp = GameManager.Instance.PlayerMoney / Mathf.Pow(10, (int)moneyDigit - (((int)(moneyDigit - 1) % 3) + 1));
            moneyText.text = temp.ToString("F2") + moneyMulti[(int)((moneyDigit - 1) / 3)];
        }
    }

    void incMoneyFalse()
    {
        incMoney.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.Instance.LevelFail.AddListener(() => ShowPanel(LosePanel, true));
        GameManager.Instance.LevelSuccess.AddListener(() => ShowPanel(WinPanel, true));
        GameManager.Instance.GameReady.AddListener(GameReady);
        GameManager.Instance.OnMoneyChange.AddListener(SetMoneyText);
    }

    private void OnDisable()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.LevelFail.RemoveListener(() => ShowPanel(LosePanel, true));
            GameManager.Instance.LevelSuccess.RemoveListener(() => ShowPanel(WinPanel, true));
            GameManager.Instance.GameReady.RemoveListener(GameReady);
        }
    }

    void ShowTutorial()
    {
        TutorialPanel.transform.GetChild(settings.GetTutorialIndex()).gameObject.SetActive(true);
    }


    void Timmer()
    {

        if (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            bar.value = timeLeft;
            string timeString = FormatTime(timeLeft);
            TimmerText.text = timeString;
        }
        else
        {
            LosePanel.SetActive(true);
        }

    }
    private string FormatTime(float time)
    {
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}", seconds);
    }

}
