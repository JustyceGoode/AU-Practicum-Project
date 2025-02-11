using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController1 : MonoBehaviour
{
    //Get components of the player
    //public GameObject playerGun;

    private Animator playerAnimator;

    //Input Variables
    private float horizontalInput;
    private float verticalInput;
    
    private Rigidbody playerRb;
    public float speed = 10.0f;

    //World Boundary
    private int xBoundary = 7;
    private int zBoundary = 5;

    //Bullets
    public GameObject bulletPrefab;

    private static int baseHealthPoints = 100; //Keep this as static so that it can be assigned to HP & maxHP
    public static int healthPoints;
    public static int maxHealthPoints;
    public TextMeshProUGUI playerHpText;
    private int baseAttackDamage = 10;
    public static int attackDamage;
    private float fireRate = 0.4f;
    private float canFire = 0f;

    public AudioClip shootSound;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        healthPoints = baseHealthPoints;
        maxHealthPoints = baseHealthPoints;
        attackDamage = baseAttackDamage;
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
            // playerRb.AddForce(Vector3.forward * speed * verticalInput);
            // playerRb.AddForce(Vector3.right * speed * horizontalInput);

            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed;
            //playerAnimator.setFloat("forwardMovement", movement.z);
            if(movement.z > 0f){
                playerAnimator.SetBool("isMovingForward", true);
            }
            else{
                playerAnimator.SetBool("isMovingForward", false);
            }

            playerRb.MovePosition(playerRb.position + movement * Time.fixedDeltaTime);
        }

        //playerGun.transform.position = transform.position + new Vector3(0,0.5f,0); //Gun follows player

        //Code for the gun to lock at the mouse
        Vector3 mousePositionScreen = Input.mousePosition;
        mousePositionScreen.z = Camera.main.transform.position.y - 1;
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);

        Vector2 direction = new Vector2(mousePositionWorld.x, mousePositionWorld.z) - new Vector2(transform.position.x, transform.position.z);
        float mouseAngle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3 (0, -mouseAngle + 90, 0);

        if(Input.GetMouseButtonDown(0) && Time.time > canFire && WaveManager.isGameActive && Time.timeScale == 1){
            //Debug.Log("Player Attack: " + attackDamage);
            canFire = Time.time + fireRate;
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            playerAudio.PlayOneShot(shootSound, 0.4f);
        }

        //Update player HP text
        playerHpText.text = "Player HP: " + healthPoints + " / " + maxHealthPoints;
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Enemy")){
            healthPoints -= Enemy.damage;
            //playerHpText.text = "Player HP: " + healthPoints + " / " + maxHealthPoints;
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Item")){
            Destroy(other.gameObject);
        }
    }
}