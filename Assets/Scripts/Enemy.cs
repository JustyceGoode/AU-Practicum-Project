using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Get Enemy parts
    public GameObject enemyWheel;
    public GameObject enemyGun;

    private float speed = 5.0f;
    private Rigidbody enemyWheelRb;
    public GameObject player;

    public float dist;

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

        if(dist > 3.0f){
            enemyWheelRb.AddForce(followDirection * speed);
        }
        else if(dist < 3.0f){
            enemyWheelRb.AddForce(-followDirection * speed);
        }

        //enemyWheelRb.AddForce(followDirection * speed);
        enemyGun.transform.position = enemyWheel.transform.position + new Vector3(0,0.5f,0); //Gun follows enemy

        float lookAngle = Vector2.SignedAngle(Vector2.right, lookDirection);
        enemyGun.transform.eulerAngles = new Vector3 (0, -lookAngle, 90);
    }
}
