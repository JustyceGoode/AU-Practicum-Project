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

    //Portal spawn locations
    public Vector3[] portalSpawnPoints = {
        new Vector3(5,1,3),
        new Vector3(-5,1,3),
        new Vector3(5,1,-3),
        new Vector3(-5,1,-3),
    };

    private int waveCounter;
    public TextMeshProUGUI waveCounterText;
    public static float strongEnemyChance; //Probabilty for stronger enemies to spawn

    //Score tracking variables
    public static int score;
    public TextMeshProUGUI scoreText;

    //private int itemCount;
    private bool waveBreak = true;

    //Game over variables
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public static bool isGameActive;

    //Pause button variables
    public TextMeshProUGUI pauseText;
    public Button continueButton;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        //Time.timeScale = 1;
        waveCounter = 1;
        waveCounterText.text = "Wave " + waveCounter;
        score = 0;
        scoreText.text = "Score: " + score;
        strongEnemyChance = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Variables to keep track of objects on the playing field.
        int enemyCount = FindObjectsOfType<Enemy>().Length;
        int portalCount = FindObjectsOfType<Portal>().Length;
        //int itemCount = FindObjectsOfType<TestScript>().Length + FindObjectsOfType<Item>().Length;
        int itemCount = FindObjectsOfType<Item>().Length;

        //Update score
        scoreText.text = "Score: " + score;

        //When all of the portals are destroyed and no enemies are on the field
        if(enemyCount == 0 && portalCount == 0){

            //Generate items before the next wave starts
            if(waveBreak){
                Instantiate(powerUpPrefab, new Vector3(2,1,0), powerUpPrefab.transform.rotation);
                Instantiate(medkitPrefab, new Vector3(-2,1,0), medkitPrefab.transform.rotation);
                Instantiate(healthPowerUpPrefab, new Vector3(0,1,0), healthPowerUpPrefab.transform.rotation);
                //itemCount = FindObjectsOfType<TestScript>().Length + FindObjectsOfType<Item>().Length;
                itemCount = FindObjectsOfType<Item>().Length; //This line is necessary so that the enemies don't spawn immediately.
                waveBreak = false;
            }

            //Start next wave after items are picked. I'm allowing the player to ignore the medkit for 2 power ups.
            if(itemCount <= 1){
                Instantiate(portalPrefab, GeneratePortalSpawnPosition(), portalPrefab.transform.rotation);
                strongEnemyChance += 0.2f;
                int enemyIndex = DiceRoller(strongEnemyChance);
                Instantiate(enemyPrefabs[enemyIndex], GenerateEnemySpawnPosition(), enemyPrefabs[enemyIndex].transform.rotation);
                enemyIndex = DiceRoller(strongEnemyChance);
                Instantiate(enemyPrefabs[enemyIndex], GenerateEnemySpawnPosition(), enemyPrefabs[enemyIndex].transform.rotation);
                waveBreak = true;
                GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
                foreach(GameObject item in items)
                    Destroy(item);
                waveCounter += 1;
                waveCounterText.text = "Wave " + waveCounter;
            }
        }

        if(PlayerController.healthPoints <= 0){
            GameOver();
            isGameActive = false;
            //Time.timeScale = 0;
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            PauseGame();
        }
    }

    private Vector3 GeneratePortalSpawnPosition(){
        int pointIndex = Random.Range(0,3);
        return portalSpawnPoints[pointIndex];
    }

    private Vector3 GenerateEnemySpawnPosition(){
        int enemyXPoint = Random.Range(-6,6);
        int enemyZPoint = Random.Range(-4,4);
        return new Vector3(enemyXPoint,1,enemyZPoint);
    }

    public void GameOver(){
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame(){
        //Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame(){
        pauseText.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueGame(){
        pauseText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        Time.timeScale = 1;
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
}
