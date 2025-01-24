using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Get Enemy parts
    public GameObject enemyWheel;
    public GameObject enemyGun;

    private float speed = 3.0f;
    private Rigidbody enemyWheelRb;
    public GameObject player;

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
        Vector3 lookDirection = followDirection;

        enemyWheelRb.AddForce(followDirection * speed);
    }
}
