using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class PlayerController : MonoBehaviour
{
    //Get components of the player
    public GameObject playerGun;

    //Input Variables
    public float horizontalInput;
    public float verticalInput;
    
    private Rigidbody playerRb;
    private float speed = 20.0f;

    //World Boundary
    private int xBoundary = 7;
    private int zBoundary = 5;

    //Bullets
    public GameObject bulletPrefab;

    private float healthPoints = 30f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get Input for player movement
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        // transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        // transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);

        //Keep player in bounds
        if(transform.position.x > xBoundary){
            transform.position = new Vector3(xBoundary, transform.position.y, transform.position.z);
        }
        if(transform.position.x < -xBoundary){
            transform.position = new Vector3(-xBoundary, transform.position.y, transform.position.z);
        }
        if(transform.position.z > zBoundary){
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundary);
        }
        if(transform.position.z < -zBoundary){
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBoundary);
        }

        playerRb.AddForce(Vector3.forward * speed * verticalInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);

        playerGun.transform.position = transform.position + new Vector3(0,0.5f,0); //Gun follows player

        //Code for the gun to lock at the mouse
        Vector3 mousePositionScreen = Input.mousePosition;
        mousePositionScreen.z = Camera.main.transform.position.y - 1;
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);

        Vector2 direction = new Vector2(mousePositionWorld.x, mousePositionWorld.z) - new Vector2(playerGun.transform.position.x, playerGun.transform.position.z);
        float mouseAngle = Vector2.SignedAngle(Vector2.right, direction);
        playerGun.transform.eulerAngles = new Vector3 (0, -mouseAngle, 90);

        if(Input.GetMouseButtonDown(0)){
            Instantiate(bulletPrefab, playerGun.transform.position, playerGun.transform.rotation);
            //Instantiate(bulletPrefab, playerGun.transform.position + new Vector3(Math.Cos(-mouseAngle), 0, Math.Sin(-mouseAngle)), playerGun.transform.rotation);
        }

        if(healthPoints <= 0){
            Debug.Log("Game over!");
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Enemy")){
            healthPoints -= 10f;
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Item")){
            Destroy(other.gameObject);
        }
    }
}