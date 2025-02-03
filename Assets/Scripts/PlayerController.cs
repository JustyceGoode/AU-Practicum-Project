using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public static int baseHealthPoints = 50;
    public static int healthPoints;
    public static int maxHealthPoints;
    public TextMeshProUGUI playerHpText;
    public static int attackDamage = 10;
    private float fireRate = 0.4f;
    private float canFire = 0f;

    public AudioClip shootSound;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        healthPoints = baseHealthPoints;
        maxHealthPoints = baseHealthPoints;
        playerHpText.text = "Player HP: " + healthPoints + " / " + maxHealthPoints;
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

        if(WaveManager.isGameActive){
            playerRb.AddForce(Vector3.forward * speed * verticalInput);
            playerRb.AddForce(Vector3.right * speed * horizontalInput);
        }

        playerGun.transform.position = transform.position + new Vector3(0,0.5f,0); //Gun follows player

        //Code for the gun to lock at the mouse
        Vector3 mousePositionScreen = Input.mousePosition;
        mousePositionScreen.z = Camera.main.transform.position.y - 1;
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);

        Vector2 direction = new Vector2(mousePositionWorld.x, mousePositionWorld.z) - new Vector2(playerGun.transform.position.x, playerGun.transform.position.z);
        float mouseAngle = Vector2.SignedAngle(Vector2.right, direction);
        playerGun.transform.eulerAngles = new Vector3 (0, -mouseAngle, 90);

        if(Input.GetMouseButtonDown(0) && Time.time > canFire && WaveManager.isGameActive){
            canFire = Time.time + fireRate;
            Instantiate(bulletPrefab, playerGun.transform.position, playerGun.transform.rotation);
            playerAudio.PlayOneShot(shootSound, 0.4f);
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Enemy")){
            healthPoints -= 10;
            playerHpText.text = "Player HP: " + healthPoints + " / " + maxHealthPoints;
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Medkit")){
            healthPoints += 30;
            if(healthPoints > maxHealthPoints){
                healthPoints = maxHealthPoints;
            }
            playerHpText.text = "Player HP: " + healthPoints + " / " + maxHealthPoints;
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Health Power Up")){
            maxHealthPoints += 50;
            healthPoints += 50;
            playerHpText.text = "Player HP: " + healthPoints + " / " + maxHealthPoints;
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Item")){
            Destroy(other.gameObject);
        }
    }
}