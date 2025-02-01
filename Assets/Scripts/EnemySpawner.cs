using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float timePassed = 0f;

    private int healthPoints = 50;
    public int playerAttackDamage = PlayerController.attackDamage;

    public ParticleSystem explosionParticle;
    public AudioClip explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.isGameActive){
            timePassed += Time.deltaTime;
            if(timePassed > 5f){
                Instantiate(enemyPrefab, transform.position + new Vector3(0,1f,0), enemyPrefab.transform.rotation);
                timePassed = 0f;
            }
        }
        // timePassed += Time.deltaTime;
        // if(timePassed > 5f){
        //     Instantiate(enemyPrefab, transform.position + new Vector3(0,1f,0), enemyPrefab.transform.rotation);
        //     timePassed = 0f;
        // }

        if(healthPoints < 0){
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 0.4f);
            GameObject explosion = Instantiate(explosionParticle.gameObject, transform.position, transform.rotation);
            Destroy(explosion, 2.0f);
            Destroy(gameObject);
        }
    }

        private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            healthPoints -= playerAttackDamage;
            Destroy(other.gameObject);
        }
    }
}
