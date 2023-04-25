using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Application.targetFrameRate = 80;
    }

    //here we have functions that are called upon certain actions, such as button click or hover
    public void Start()
    {
        Application.targetFrameRate = 200;
        
    }
    public void PlayGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void SettingsMenu()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HoverSFX()
    {
        
    }

    public void ClickSFX()
    {
        
    }
}

