using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class TitleMenuManager : MonoBehaviour
{
    //Title Menu Variables
    public TextMeshProUGUI titleText;
    public Button startGameButton;
    public Button controlMenuButton;

    //Control Menu Variables
    public TextMeshProUGUI controlMenuHeaderText;
    public TextMeshProUGUI controlMenuText;
    public Button controlsBackButton;
    public GameObject medkit;
    public GameObject powerUp;
    public GameObject healthUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        SceneManager.LoadScene("My Game");
        Time.timeScale = 1;
    }

    public void OpenControlsMenu(){
        titleText.gameObject.SetActive(false);
        startGameButton.gameObject.SetActive(false);
        controlMenuButton.gameObject.SetActive(false);
        controlMenuHeaderText.gameObject.SetActive(true);
        controlMenuText.gameObject.SetActive(true);
        controlsBackButton.gameObject.SetActive(true);
        medkit.gameObject.SetActive(true);
        powerUp.gameObject.SetActive(true);
        healthUp.gameObject.SetActive(true);
    }

    public void CloseControlsMenu(){
        titleText.gameObject.SetActive(true);
        startGameButton.gameObject.SetActive(true);
        controlMenuButton.gameObject.SetActive(true);
        controlMenuHeaderText.gameObject.SetActive(false);
        controlMenuText.gameObject.SetActive(false);
        controlsBackButton.gameObject.SetActive(false);
        medkit.gameObject.SetActive(false);
        powerUp.gameObject.SetActive(false);
        healthUp.gameObject.SetActive(false);
    }
}
