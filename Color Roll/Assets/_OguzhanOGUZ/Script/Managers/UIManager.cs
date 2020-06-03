using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private UIManager() { }

    [SerializeField] private RectTransform _canvas;
    [SerializeField] private GameObject joyStickPanel;
    [SerializeField] private RectTransform joyStickBase;
    [SerializeField] private RectTransform joyStickHandle;
    private RawImage joyStickBaseImage;
    private RawImage joyStickHandleImage;

    [SerializeField] private GameObject panelInGame;
    [SerializeField] private GameObject panelFail;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private Text collectedStarNoText;
    [SerializeField] private Text collectedDiamondNoText;
    [SerializeField] private GameObject panelQuitConfirm;
    private float joyStickRadius = 0;

    private void Start()
    {
        joyStickRadius = (float)_canvas.GetComponent<RectTransform>().sizeDelta.y / 8F;
        joyStickBaseImage = joyStickBase.GetComponent<RawImage>();
        joyStickHandleImage = joyStickHandle.GetComponent<RawImage>();
        joyStickBase.sizeDelta = Vector2.one * joyStickRadius * 2;
    }

    private void Update()
    {
        JoyStickUI();
    }
    void JoyStickUI()
    {
        Vector2 firstTouchPoint = GameManager.Instance.GetJoyStickFirstTouchPoint();
        Vector2 joyStickValue = GameManager.Instance.GetJoystick();
        bool isJoyStickActive = !(joyStickValue.x == 0 && joyStickValue.y == 0);
        UpdateJoyStick(firstTouchPoint, joyStickValue, joyStickRadius, isJoyStickActive);
        UpdateJoyStickColor();
    }
    void UpdateJoyStickColor()
    {
        Color32 currentBallColor = GameManager.Instance.GetBallCurrentColor();
        joyStickBaseImage.color = currentBallColor;
        joyStickHandleImage.color = currentBallColor;
    }
    public void UpdateJoyStick(Vector2 firstTouchPoint, Vector2 joyStickValue, float joyStickRadiusUI, bool isJoyStickActive = false)
    {
        if(joyStickValue.magnitude > 0.1F && isJoyStickActive)
        {
            joyStickPanel.SetActive(true);
            float x = ((float)firstTouchPoint.x / (float)Screen.width) * (float)_canvas.sizeDelta.x;
            float y = ((float)firstTouchPoint.y / (float)Screen.height) * (float)_canvas.sizeDelta.y;

            Vector2 basePosition = new Vector2(x, y);
            joyStickBase.anchoredPosition = basePosition;

            float joyStickAngle = Mathf.Atan2(joyStickValue.y, joyStickValue.x);
            var joyStickOffLimitPosition = new Vector2(Mathf.Cos(joyStickAngle), Mathf.Sin(joyStickAngle));

            Vector2 joyStickPosition = (joyStickOffLimitPosition.magnitude < joyStickValue.magnitude) ? joyStickOffLimitPosition : joyStickValue;

            Vector2 handlerPos = basePosition + (joyStickPosition * joyStickRadius);
            joyStickHandle.anchoredPosition = handlerPos;

        }
        else
        {
            joyStickPanel.SetActive(false);
        }
    }

    public void OnBallFell()
    {
        panelFail.SetActive(true);
    }

    public void OnLevelCleared()
    {
        collectedStarNoText.text = ": " + GameManager.Instance.GetGemStar();
        collectedDiamondNoText.text = ": " + GameManager.Instance.GetGemDiamond();
        Invoke("ShowWinPanel", 1);
    }

    void ShowWinPanel()
    {
        panelWin.SetActive(true);
    }



    //In Game
    public void UIButton_QuitInGame()
    {
        Time.timeScale = 0;
        panelQuitConfirm.SetActive(true);
    }
    //Quit Panel
    public void UIButton_ConfirmQuit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
    public void UIButton_CancelQuit()
    {
        Time.timeScale = 1;
        panelQuitConfirm.SetActive(false);
    }
    //Level Fail Panel
    public void UIButton_Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void UIButton_QuitFailPanel()
    {
        Time.timeScale = 0;
        panelQuitConfirm.SetActive(true);
    }
    //Win Panel
    public void UIButton_QuitWinPanel()
    {
        Time.timeScale = 0;
        panelQuitConfirm.SetActive(true);
    }
    public void UIButton_ReloadWinPanel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void UIButton_NextLevel()
    {
        int activeSceneNo = int.Parse(SceneManager.GetActiveScene().name);
        int nextLevelNo = activeSceneNo + 1;
        if(nextLevelNo<= MainMenu.maxLevelNo)
        {
            string nextLevelName = nextLevelNo.ToString();
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

}
