using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float timePassed = 0f;

    private float healthPoints = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed > 5f){
            Instantiate(enemyPrefab, transform.position + new Vector3(0,1f,0), enemyPrefab.transform.rotation);
            timePassed = 0f;
        }

        if(healthPoints < 0){
            Destroy(gameObject);
        }
    }

        private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            healthPoints -= 10f;
            Destroy(other.gameObject);
        }
    }
}
