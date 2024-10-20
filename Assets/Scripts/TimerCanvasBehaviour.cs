using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class TimerCanvasBehaviour : MonoBehaviour
{
    TextMeshProUGUI timerText;
    TextMeshProUGUI timerTextWin;
    Button restartButton;
    Button menuButton;
    Image backGround;
    public AudioClip timerMusic;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        Transform tTimerText = transform.Find("TimerText");
        timerText = tTimerText.GetComponent<TextMeshProUGUI>();

        Transform wTimerText = transform.Find("TimerTextWIn");
        timerTextWin = wTimerText.GetComponent<TextMeshProUGUI>();

        Transform rRestartButton = transform.Find("RestartButton");
        restartButton = rRestartButton.GetComponent<Button>();
        restartButton.onClick.AddListener(RestartOnClick);



        Transform mMenuButton = transform.Find("MenuButton");
        menuButton = mMenuButton.GetComponent<Button>();
        menuButton.onClick.AddListener(MenuOnClick);



        Transform bBackground = transform.Find("Background");

        backGround = bBackground.GetComponent<Image>();


        timerText.text = string.Empty;
        timerTextWin.text = string.Empty;

        timerText.enabled = true;
        timerTextWin.enabled = false;

        //restartButton.enabled = false;
        //restartButtonText.enabled = false;
        restartButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);

        backGround.enabled = false;



    }
    void RestartOnClick()
    {
        SceneFunctions.RestartScene();
    }
    void MenuOnClick()
    {
        //GameManager.
        SceneFunctions.StartMenuScene();
    }


    void Update()
    {
        switch (GameManager.GetInstance().GetGameState())
        {
            case GameManager.GameState.TIMERSTART:
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(timerMusic, 0.4F);
                }
                break;
            case GameManager.GameState.TIMERONGOING:
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(timerMusic, 0.4F);
                }
                Cursor.visible = false;
                timerText.text = "Your Time is " +
                    GameManager.GetInstance().GetTimerTime().ToString("F2");
                break;
            case GameManager.GameState.TIMEREND:
                timerTextWin.enabled = true;
                timerText.enabled = false;
                restartButton.gameObject.SetActive(true);
                menuButton.gameObject.SetActive(true);
                backGround.enabled = true;
                Cursor.visible = true;


                timerTextWin.text = "Your Time is " +
                    GameManager.GetInstance().GetTimerTime().ToString("F2");
                break;
            case GameManager.GameState.TIMERNOTSTART:
                break;
            case GameManager.GameState.RESTART:
                timerText.enabled = true;
                timerText.text = "Fail. Restarting";
                break;
        }
        
    }
}
