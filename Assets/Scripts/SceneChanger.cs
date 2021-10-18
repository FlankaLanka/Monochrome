using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().buildIndex != 0) //0 is main menu
        {
            RestartGame();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            ReturnToMain();
        }
    }



    /// <summary>
    /// below this are functions used in game
    /// </summary>
    /// 
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("Main Menu");
    }



    /// <summary>
    /// below this are functions used in main menu
    /// </summary>
    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //replace with game scene name
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
