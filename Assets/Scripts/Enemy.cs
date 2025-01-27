using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Get Enemy parts
    public GameObject enemyWheel;
    public GameObject enemyGun;

    private float speed = 10.0f;
    private Rigidbody enemyWheelRb;
    public GameObject player;

    public float dist;

    private int xBoundary = 7;
    private int zBoundary = 5;

    public GameObject bulletPrefab;
    private float timePassed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyWheelRb = enemyWheel.GetComponent<Rigidbody>();
        player = GameObject.Find("Player Wheel");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 followDirection = (player.transform.position - enemyWheel.transform.position).normalized;
        Vector2 lookDirection = new Vector2(player.transform.position.x, player.transform.position.z) - new Vector2(enemyGun.transform.position.x, enemyGun.transform.position.z);
        lookDirection = lookDirection.normalized;

        dist = Vector3.Distance(enemyWheel.transform.position, player.transform.position);

        if(dist > 3.25f){
            enemyWheelRb.AddForce(followDirection * speed);
        }
        else if(dist < 2.75f){
            enemyWheelRb.AddForce(-followDirection * speed);
        }

        //Keep enemy in bounds
        if(enemyWheel.transform.position.x > xBoundary){
            enemyWheel.transform.position = new Vector3(xBoundary, enemyWheel.transform.position.y, enemyWheel.transform.position.z);
        }
        if(enemyWheel.transform.position.x < -xBoundary){
            enemyWheel.transform.position = new Vector3(-xBoundary, enemyWheel.transform.position.y, enemyWheel.transform.position.z);
        }
        if(enemyWheel.transform.position.z > zBoundary){
            enemyWheel.transform.position = new Vector3(enemyWheel.transform.position.x, enemyWheel.transform.position.y, zBoundary);
        }
        if(enemyWheel.transform.position.z < -zBoundary){
            enemyWheel.transform.position = new Vector3(enemyWheel.transform.position.x, enemyWheel.transform.position.y, -zBoundary);
        }

        //enemyWheelRb.AddForce(followDirection * speed);
        enemyGun.transform.position = enemyWheel.transform.position + new Vector3(0,0.5f,0); //Gun follows enemy

        float lookAngle = Vector2.SignedAngle(Vector2.right, lookDirection);
        enemyGun.transform.eulerAngles = new Vector3 (0, -lookAngle, 90);

        timePassed += Time.deltaTime;
        if(timePassed > 2f){
            Instantiate(bulletPrefab, enemyGun.transform.position, enemyGun.transform.rotation);
            timePassed = 0f;
        }
    }
}
