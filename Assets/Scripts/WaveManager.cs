using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    //Prefabs
    public GameObject portalPrefab;
    public GameObject[] enemyPrefabs;
    public GameObject powerUpPrefab;
    public GameObject healthPowerUpPrefab;
    public GameObject medkitPrefab;
    private GameObject player;

    //Portal spawn locations
    private Vector3[] portalSpawnPoints = {
        new Vector3(11,1,7),
        new Vector3(-11,1,7),
        new Vector3(11,1,-7),
        new Vector3(-11,1,-7),
        new Vector3(11,1,0),
        new Vector3(-11,1,0),
        new Vector3(0,1,7),
        new Vector3(0,1,-7),
    };

    private int waveCounter;
    public TextMeshProUGUI waveCounterText;
    public static float strongEnemyChance; //Probabilty for stronger enemies to spawn
    public static bool endless = false;

    //Score tracking variables
    public static int score;
    public TextMeshProUGUI scoreText;
    public static int highScore;
    public TextMeshProUGUI highScoreText;

    //private int itemCount;
    private bool waveBreak;
    private bool victory;
    public TextMeshProUGUI itemSelectionText;

    //UI variables
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public static bool isGameActive;
    public TextMeshProUGUI pauseText;
    public Button continueButton;
    public Button backToTitleButton;
    public TextMeshProUGUI youWinText;

    //Settings UI
    public Button settingsButton;
    public TextMeshProUGUI settingsText;
    public TextMeshProUGUI bgmText;
    public Slider bgmSlider;
    public TextMeshProUGUI sfxText;
    public Slider sfxSlider;
    public Button settingsBackButton;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        isGameActive = true;
        waveBreak = false;
        victory = false;
        Time.timeScale = 1;
        waveCounter = 0;
        waveCounterText.text = "Wave " + waveCounter;
        score = 0;
        scoreText.text = "Score: " + score;

        // highScoreText.gameObject.SetActive(endless);

        // if(PlayerPrefs.HasKey("highScore")){
        //     highScore = PlayerPrefs.GetInt("highScore");
        // }
        // else{
        //     highScore = 0;
        // }
        // highScoreText.text = "High Score: " + highScore;

        strongEnemyChance = -0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        //Variables to keep track of objects on the playing field.
        int enemyCount = FindObjectsOfType<Enemy>().Length;
        int portalCount = FindObjectsOfType<Portal>().Length;
        int itemCount = FindObjectsOfType<Item>().Length;

        //Portal Spawns
        Vector3 firstPortalSpawn = new Vector3(0,0,0);
        Vector3 secondPortalSpawn = new Vector3(0,0,0);
        Vector3 thirdPortalSpawn = new Vector3(0,0,0);
        Vector3 fourthPortalSpawn = new Vector3(0,0,0);

        //Enemy Spawns
        Vector3 firstEnemySpawn;
        Vector3 secondEnemySpawn;

        //Update score
        scoreText.text = "Score: " + score;

        //Debug.Log(Mathf.Abs(player.transform.position.x) <= 7 && Mathf.Abs(player.transform.position.z) <= 3.5);

        //When all of the portals are destroyed and no enemies are on the field
        if(enemyCount == 0 && portalCount == 0){

            if(waveCounter == 5 && !endless){
                YouWin();
                //UpdateHighScore();
                victory = true;
            }

            //Generate items before the next wave starts
            if(waveBreak && !victory){

                if(Mathf.Abs(player.transform.position.x) <= 7 && Mathf.Abs(player.transform.position.z) <= 3.5f){
                    Instantiate(powerUpPrefab, new Vector3(3.5f,1,6), powerUpPrefab.transform.rotation);
                    Instantiate(medkitPrefab, new Vector3(-3.5f,1,6), medkitPrefab.transform.rotation);
                    Instantiate(healthPowerUpPrefab, new Vector3(0,1,6), healthPowerUpPrefab.transform.rotation);
                }
                else{
                    Instantiate(powerUpPrefab, new Vector3(3.5f,1,0), powerUpPrefab.transform.rotation);
                    Instantiate(medkitPrefab, new Vector3(-3.5f,1,0), medkitPrefab.transform.rotation);
                    Instantiate(healthPowerUpPrefab, new Vector3(0,1,0), healthPowerUpPrefab.transform.rotation);
                }
                
                itemCount = FindObjectsOfType<Item>().Length; //This line is necessary so that the enemies don't spawn immediately.
                waveBreak = false;
                itemSelectionText.gameObject.SetActive(true);
            }

            //Start next wave after items are picked. I'm allowing the player to ignore the medkit for 2 power ups.
            if(itemCount <= 1 && !victory){
                waveCounter += 1;
                waveCounterText.text = "Wave " + waveCounter;
                itemSelectionText.gameObject.SetActive(false);

                //Spawn first portal
                firstPortalSpawn = GeneratePortalSpawnPosition();
                Instantiate(portalPrefab, firstPortalSpawn, portalPrefab.transform.rotation);

                //Spawn second portal
                if(waveCounter >= 3){
                    secondPortalSpawn = GeneratePortalSpawnPosition();
                    while(secondPortalSpawn == firstPortalSpawn){
                        secondPortalSpawn = GeneratePortalSpawnPosition();
                    }
                    Instantiate(portalPrefab, secondPortalSpawn, portalPrefab.transform.rotation);
                }

                //Spawn third portal
                if(waveCounter >= 5){
                    thirdPortalSpawn = GeneratePortalSpawnPosition();
                    while(thirdPortalSpawn == firstPortalSpawn || thirdPortalSpawn == secondPortalSpawn){
                        thirdPortalSpawn = GeneratePortalSpawnPosition();
                    }
                    Instantiate(portalPrefab, thirdPortalSpawn, portalPrefab.transform.rotation);
                }

                if(waveCounter >= 7){
                    fourthPortalSpawn = GeneratePortalSpawnPosition();
                    while(thirdPortalSpawn == firstPortalSpawn || thirdPortalSpawn == secondPortalSpawn || fourthPortalSpawn == thirdPortalSpawn){
                        fourthPortalSpawn = GeneratePortalSpawnPosition();
                    }
                    Instantiate(portalPrefab, fourthPortalSpawn, portalPrefab.transform.rotation);
                }
                
                //Spawn enemies
                strongEnemyChance += 0.15f;

                //Spawn First Enemy
                int enemyIndex = DiceRoller(strongEnemyChance);
                firstEnemySpawn = GenerateEnemySpawnPosition();
                while(Vector3.Distance(firstEnemySpawn, player.transform.position) < 7){
                    firstEnemySpawn = GenerateEnemySpawnPosition();
                }
                Instantiate(enemyPrefabs[enemyIndex], GenerateEnemySpawnPosition(), enemyPrefabs[enemyIndex].transform.rotation);

                //Spawn Second Enemy
                enemyIndex = DiceRoller(strongEnemyChance);
                secondEnemySpawn = GenerateEnemySpawnPosition();
                while(Vector3.Distance(secondEnemySpawn, player.transform.position) < 7){
                    secondEnemySpawn = GenerateEnemySpawnPosition();
                }
                Instantiate(enemyPrefabs[enemyIndex], GenerateEnemySpawnPosition(), enemyPrefabs[enemyIndex].transform.rotation);


                waveBreak = true;
                GameObject[] leftoverItems = GameObject.FindGameObjectsWithTag("Item");
                foreach(GameObject leftover in leftoverItems)
                    Destroy(leftover);
                // waveCounter += 1;
                // waveCounterText.text = "Wave " + waveCounter;
            }
        }

        if(PlayerController.healthPoints <= 0){
            GameOver();
            if(endless){
                UpdateHighScore();
            }
        }

        // if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0){
        //     PauseGame();
        // }

        if(Input.GetKeyDown(KeyCode.Escape) && isGameActive){
            PauseGame();
        }
    }

    private Vector3 GeneratePortalSpawnPosition(){
        int pointIndex = Random.Range(0,7);
        return portalSpawnPoints[pointIndex];
    }

    private Vector3 GenerateEnemySpawnPosition(){
        int enemyXPoint = Random.Range(-9,9);
        int enemyZPoint = Random.Range(-4,4);
        return new Vector3(enemyXPoint,2,enemyZPoint);
    }

    public void GameOver(){
        isGameActive = false;
        //Time.timeScale = 0;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        backToTitleButton.gameObject.SetActive(true);
    }

    public void RestartGame(){
        //Time.timeScale = 1;
        isGameActive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame(){
        pauseText.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);
        backToTitleButton.gameObject.SetActive(true);
        //Time.timeScale = 0;
        isGameActive = false;
        //PlayerController.playerRb.freezeRotation = true;
    }

    //Continue Game after pausing
    public void ContinueGame(){
        pauseText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        backToTitleButton.gameObject.SetActive(false);
        //Time.timeScale = 1;
        isGameActive = true;
        //PlayerController.playerRb.freezeRotation = false;
    }

    public void OpenSettings(){
        //Disable Pause UI
        pauseText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        backToTitleButton.gameObject.SetActive(false);

        //Enable Settings UI
        settingsText.gameObject.SetActive(true);
        bgmText.gameObject.SetActive(true);
        bgmSlider.gameObject.SetActive(true);
        sfxText.gameObject.SetActive(true);
        sfxSlider.gameObject.SetActive(true);
        settingsBackButton.gameObject.SetActive(true);
    }

    public void CloseSettings(){
        //Enable Pause UI
        pauseText.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);
        backToTitleButton.gameObject.SetActive(true);

        //Disable Settings UI
        settingsText.gameObject.SetActive(false);
        bgmText.gameObject.SetActive(false);
        bgmSlider.gameObject.SetActive(false);
        sfxText.gameObject.SetActive(false);
        sfxSlider.gameObject.SetActive(false);
        settingsBackButton.gameObject.SetActive(false);
    }

    public void YouWin(){
        //Time.timeScale = 0;
        isGameActive = false;
        youWinText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        backToTitleButton.gameObject.SetActive(true);
    }

    public void BackToTitle(){
        SceneManager.LoadScene("Title Menu");
    }

    private int DiceRoller(float strongChance){
        float temp = Random.Range(1,10);
        if(temp < strongChance * 10){
            return 1;
        }
        else{
            return 0;
        }
    }

    private void UpdateHighScore(){
        if(score > highScore){
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
            highScoreText.text = "High Score: " + highScore;
        }
    }
}
