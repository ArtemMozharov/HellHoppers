using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFunctions : MonoBehaviour
{
    public static void StartMenuScene()
    {
        SceneManager.LoadScene(0);
    }
    public static void StartTrainingLevel()
    {
        //SceneManager.LoadSceneAsync(1);
        SceneManager.LoadScene(1);
    }

    public static void StartFirstLevel()
    {

        //SceneManager.LoadScene(2);
        SceneManager.LoadScene(2);
    }

    public static void RestartScene()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
