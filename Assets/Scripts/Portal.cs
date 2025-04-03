using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    //public GameObject enemyPrefab;
    public GameObject[] enemyPrefabs;
    private float timePassed = 0f;

    private int healthPoints = 120;
    //public int playerAttackDamage;

    public ParticleSystem explosionParticle;
    public AudioClip explosionSound;

    //private static float baseStrongEnemyChance = 0.0f;
    //public static float strongEnemyChance;

    // Start is called before the first frame update
    void Start()
    {
        //strongEnemyChance = baseStrongEnemyChance;
    }

    // Update is called once per frame
    void Update()
    {
        if(WaveManager.isGameActive){
            timePassed += Time.deltaTime;
            if(timePassed > 5f){
                //Debug.Log("Strong Enemy Chance: " + WaveManager.strongEnemyChance);
                int enemyIndex = DiceRoller(WaveManager.strongEnemyChance);
                Instantiate(enemyPrefabs[enemyIndex], transform.position + enemySpawnPosition(), enemyPrefabs[enemyIndex].transform.rotation);
                timePassed = 0f;
            }
        }

        //playerAttackDamage = PlayerController.attackDamage;

        //Whent the portal is destroyed.
        if(healthPoints <= 0){
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, SoundManager.sfxVolume);
            GameObject explosion = Instantiate(explosionParticle.gameObject, transform.position, transform.rotation);
            Destroy(explosion, 2.0f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            //healthPoints -= playerAttackDamage;
            healthPoints -= PlayerController.attackDamage;
            //Debug.Log("Portal Health: " + healthPoints);
            Destroy(other.gameObject);
        }
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

    private Vector3 enemySpawnPosition(){
        Vector3[] enemySpawnPoints = {
            new Vector3(3.5f, 3f, 0),
            new Vector3(-3.5f, 3f, 0),
            new Vector3(0, 3f, 3.5f),
            new Vector3(0, 3f, -3.5f)
        };

        int pointIndex = Random.Range(0,4);
        //Debug.Log(pointIndex);
        return enemySpawnPoints[pointIndex];
    }
}
