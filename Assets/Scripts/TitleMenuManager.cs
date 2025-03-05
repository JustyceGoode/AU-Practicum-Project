using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class TitleMenuManager : MonoBehaviour
{
    //Title Menu Elements
    public TextMeshProUGUI titleText;
    public Button startGameButton;
    public Button controlMenuButton;
    public Button exitGameButton;

    //Control Menu Elements
    public TextMeshProUGUI controlMenuHeaderText;
    public TextMeshProUGUI controlMenuText;
    public Button controlsBackButton;
    public GameObject medkit;
    public GameObject powerUp;
    public GameObject healthUp;

    //Credits Menu Elements
    public Button creditsButton;
    public TextMeshProUGUI creditsMenuHeaderText;
    public TextMeshProUGUI creditsMenuText;
    public Button creditsBackButton;

    public void StartGame(){
        SceneManager.LoadScene("My Game");
        Time.timeScale = 1;
    }

    public void OpenControlsMenu(){
        //Disable Title UI
        titleText.gameObject.SetActive(false);
        startGameButton.gameObject.SetActive(false);
        controlMenuButton.gameObject.SetActive(false);
        exitGameButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);

        //Enable Control UI
        controlMenuHeaderText.gameObject.SetActive(true);
        controlMenuText.gameObject.SetActive(true);
        controlsBackButton.gameObject.SetActive(true);
        medkit.gameObject.SetActive(true);
        powerUp.gameObject.SetActive(true);
        healthUp.gameObject.SetActive(true);
    }

    public void CloseControlsMenu(){
        //Enable Title UI
        titleText.gameObject.SetActive(true);
        startGameButton.gameObject.SetActive(true);
        controlMenuButton.gameObject.SetActive(true);
        exitGameButton.gameObject.SetActive(true);
        creditsButton.gameObject.SetActive(true);

        //Disable Contrls UI
        controlMenuHeaderText.gameObject.SetActive(false);
        controlMenuText.gameObject.SetActive(false);
        controlsBackButton.gameObject.SetActive(false);
        medkit.gameObject.SetActive(false);
        powerUp.gameObject.SetActive(false);
        healthUp.gameObject.SetActive(false);
    }

    public void OpenCreditsMenu(){
        //Disable Title UI
        titleText.gameObject.SetActive(false);
        startGameButton.gameObject.SetActive(false);
        controlMenuButton.gameObject.SetActive(false);
        exitGameButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);

        //Enable Credits UI
        creditsMenuHeaderText.gameObject.SetActive(true);
        creditsMenuText.gameObject.SetActive(true);
        creditsBackButton.gameObject.SetActive(true);
    }

    public void CloseCreditsMenu(){
        //Enable Title UI
        titleText.gameObject.SetActive(true);
        startGameButton.gameObject.SetActive(true);
        controlMenuButton.gameObject.SetActive(true);
        exitGameButton.gameObject.SetActive(true);
        creditsButton.gameObject.SetActive(true);

        //Disable Credits UI
        creditsMenuHeaderText.gameObject.SetActive(false);
        creditsMenuText.gameObject.SetActive(false);
        creditsBackButton.gameObject.SetActive(false);
    }

    public void ExitGame(){
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
