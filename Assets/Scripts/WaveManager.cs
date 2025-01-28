using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject portalPrefab;
    public GameObject powerUpPrefab;
    public GameObject medkitPrefab;
    public Vector3[] portalSpawnPoints = {
        new Vector3(5,1,3),
        new Vector3(-5,1,3),
        new Vector3(5,1,-3),
        new Vector3(-5,1,-3),
    };

    private bool waveBreak = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int enemyCount = FindObjectsOfType<Enemy>().Length;
        int portalCount = FindObjectsOfType<EnemySpawner>().Length;
        int itemCount = FindObjectsOfType<TestScript>().Length + FindObjectsOfType<TestScript>().Length;

        if(enemyCount == 0 && portalCount == 0){
            if(waveBreak){
                Instantiate(powerUpPrefab, new Vector3(1,1,0), powerUpPrefab.transform.rotation);
                Instantiate(medkitPrefab, new Vector3(-1,1,0), medkitPrefab.transform.rotation);
                waveBreak = false;
                itemCount = FindObjectsOfType<TestScript>().Length + FindObjectsOfType<TestScript>().Length;
            }
            if(itemCount <= 0){
                Instantiate(portalPrefab, GeneratePortalSpawnPosition(), portalPrefab.transform.rotation);
                waveBreak = true;
            }
        }

    }

    private Vector3 GeneratePortalSpawnPosition(){
        int pointIndex = Random.Range(0,3);
        return portalSpawnPoints[pointIndex];
    }
}
