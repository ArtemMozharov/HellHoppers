using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    float timerTime = 0;
    GameState gameState;
    static GameManager instance;
    public float currentLevelTime = 0;
    //public AudioClip swing;
    public AudioClip jump;
    public AudioClip shoot;
    public AudioClip reload;
    //public AudioClip swing;
    //public AudioClip walk;
    AudioSource audioSource;


    //public GameObject winCanvas;
    public enum GameState
    {   
        TIMERNOTSTART,
        TIMERSTART,
        TIMEREND,
        TIMERONGOING,
        RESTART
    }
    public void ChangeTimerState(GameState state)
    {
        gameState = state;
    }
    public static GameManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        //winCanvas.SetActive(false);
        gameState = GameState.TIMERNOTSTART;
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown("escape")) SceneFunctions.StartMenuScene();
        switch (gameState)
        {
            case GameState.TIMERSTART:
                startTimer();
                break;
            case GameState.TIMERONGOING:
                timerTime += Time.deltaTime;
                break;
            case GameState.TIMEREND:
                
                break;
            case GameState.TIMERNOTSTART: 
                break;
            case GameState.RESTART:
                ChangeTimerState(GameState.TIMERNOTSTART);
                RestartLevel();
                break;
        }
    }
    void RestartLevel()
    {
        SceneFunctions.RestartScene();
    }
    void startTimer()
    {
        timerTime = 0;
        gameState = GameState.TIMERONGOING;
    }

    public float GetTimerTime()
    {
        return timerTime;
    }
    
    public GameState GetGameState()
    {
        return gameState;
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void SwingingSoundPlay()
    {
        if (!audioSource.isPlaying)
        {
            //audioSource.PlayOneShot(swing, 0.6F);
        }
    }
    public void JumpingSoundPlay()
    {
        audioSource.PlayOneShot(jump, 0.6F);
    }
    public void WalkingSoundPlay()
    {
        if(!audioSource.isPlaying) {
            //audioSource.PlayOneShot(walk, 0.6F);
        }
        
    }
    public void ShootingSoundPlay()
    {
        audioSource.PlayOneShot(shoot, 0.8F);
    }
    public void ReloadingSoundPlay()
    {
        audioSource.PlayOneShot(reload, 0.6F) ;
    }

}
