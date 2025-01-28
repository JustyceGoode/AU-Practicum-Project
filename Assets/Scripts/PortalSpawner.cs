using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    public GameObject portalPrefab;
    public Vector3[] portalSpawnPoints = {
        new Vector3(5,1,3),
        new Vector3(-5,1,3),
        new Vector3(5,1,-3),
        new Vector3(-5,1,-3),
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int enemyCount = FindObjectsOfType<Enemy>().Length;
        int portalCount = FindObjectsOfType<EnemySpawner>().Length;

        if(enemyCount == 0 && portalCount == 0){
            Instantiate(portalPrefab, GenerateSpawnPosition(), portalPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition(){
        int pointIndex = Random.Range(0,3);
        return portalSpawnPoints[pointIndex];
    }
}
