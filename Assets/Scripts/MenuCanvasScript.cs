using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;


public class MenuCanvasScript : MonoBehaviour
{

    Button lCButton;
    Button firstLevel;
    Button quitGame;
    public AudioClip menuMusic;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        Transform rRestartButton = transform.Find("LCButton");
        lCButton = rRestartButton.GetComponent<Button>();
        lCButton.onClick.AddListener(LCOnClick);
        Transform mMenuButton = transform.Find("FirstLevelButton");
        firstLevel = mMenuButton.GetComponent<Button>();
        firstLevel.onClick.AddListener(FirstLevelOnClick);
        Cursor.visible = true;
        Transform qQuitButton = transform.Find("QuitButton");
        quitGame = qQuitButton.GetComponent<Button>();
        quitGame.onClick.AddListener(QuitGameOnClick);
        audioSource.PlayOneShot(menuMusic, 0.7F);
    }
    void LCOnClick()
    {
        SceneFunctions.StartTrainingLevel();
    }
    void FirstLevelOnClick()
    {
        SceneFunctions.StartFirstLevel();
    }
    void QuitGameOnClick()
    {
        GameManager.GetInstance().Quit();
    }



    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(menuMusic, 0.7F);
        }
        Cursor.visible = true;
    }
}
