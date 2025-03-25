using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //Get components of the player
    public GameObject playerPointer;
    private float pointerDistance = 1.6f;

    private Animator playerAnimator;

    //Input Variables
    private float horizontalInput;
    private float verticalInput;
    
    private Rigidbody playerRb;
    private float speed = 2.0f;

    //World Boundary
    private int xBoundary = 20;
    private int zBoundary = 10;

    //Bullets
    public GameObject bulletPrefab;

    //Player Stats
    private static int baseHealthPoints = 100; //Keep this as static so that it can be assigned to HP & maxHP
    public static int healthPoints;
    public static int maxHealthPoints;
    public TextMeshProUGUI playerHpText;
    private int baseAttackDamage = 15;
    public static int attackDamage;
    private float fireRate = 0.45f;
    private float canFire = 0f;

    //Sound variables for bullets
    public AudioClip shootSound;
    private AudioSource playerAudio;
    private static float shootVolume;

    //Explosion particle variables
    public ParticleSystem explosionParticle;
    public AudioClip explosionSound;

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
        //mouseAngle = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Get Input for player movement
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

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

        //Gun looks at mouse
        Vector3 mousePositionScreen = Input.mousePosition;
        mousePositionScreen.z = Camera.main.transform.position.y - 1;
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);

        Vector2 direction = new Vector2(mousePositionWorld.x, mousePositionWorld.z) - new Vector2(transform.position.x, transform.position.z);
        float mouseAngle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3 (0, -mouseAngle + 90, 0);
        
        //Spawns bullet away from player
        float mouseRadian = (mouseAngle / 180) * (Mathf.PI);
        playerPointer.transform.position = transform.position + new Vector3(pointerDistance*Mathf.Cos(mouseRadian), 1, pointerDistance*Mathf.Sin(mouseRadian));

        if(Input.GetMouseButton(0) && Time.time > canFire && WaveManager.isGameActive && Time.timeScale == 1){
            canFire = Time.time + fireRate; //Controls the fire rate
            Instantiate(bulletPrefab, transform.position + new Vector3(pointerDistance*Mathf.Cos(mouseRadian), 1, pointerDistance*Mathf.Sin(mouseRadian)), Quaternion.Euler(new Vector3(0, -mouseAngle, 90)));
            //playerAudio.PlayOneShot(shootSound, 0.4f);
            shootVolume = SoundManager.sfxVolume;
            //Debug.Log("Shoot Volume: " + shootVolume);
            playerAudio.PlayOneShot(shootSound, shootVolume);
        }

        //Player can move while game is active
        if(WaveManager.isGameActive){

            //Move player
            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed;
            playerRb.MovePosition(playerRb.position + movement * Time.fixedDeltaTime * Time.timeScale);

            //Check if the player is moving
            if(movement.x != 0f || movement.z != 0f){
                playerAnimator.SetBool("isMoving", true);
            }
            else{
                playerAnimator.SetBool("isMoving", false);
            }

            //If the player is looking right
            if(mouseAngle > -45 && mouseAngle < 45){

                //Play Forward Animation
                if(movement.x > 0f){
                    playerAnimator.SetBool("movingForward", true);
                    playerAnimator.SetBool("movingBackward", false);
                    playerAnimator.SetBool("movingLeft", false);
                    playerAnimator.SetBool("movingRight", false);
                }

                //Play Backward Animation
                else if(movement.x < 0f){
                    playerAnimator.SetBool("movingForward", false);
                    playerAnimator.SetBool("movingBackward", true);
                    playerAnimator.SetBool("movingLeft", false);
                    playerAnimator.SetBool("movingRight", false);
                }

                //Play Left Animation
                else if(movement.z > 0f && movement.x == 0f){
                    playerAnimator.SetBool("movingForward", false);
                    playerAnimator.SetBool("movingBackward", false);
                    playerAnimator.SetBool("movingLeft", true);
                    playerAnimator.SetBool("movingRight", false);
                }

                //Play Right Animation
                else if(movement.z < 0f && movement.x == 0f){
                    playerAnimator.SetBool("movingForward", false);
                    playerAnimator.SetBool("movingBackward", false);
                    playerAnimator.SetBool("movingLeft", false);
                    playerAnimator.SetBool("movingRight", true);
                }
            }

            //If the player is looking left
            if(mouseAngle < -135 || mouseAngle > 135){

                //Play forward animation
                if(movement.x < 0f){
                    playerAnimator.SetBool("movingForward", true);
                    playerAnimator.SetBool("movingBackward", false);
                    playerAnimator.SetBool("movingLeft", false);
                    playerAnimator.SetBool("movingRight", false);
                }

                //Play backward animation
                else if(movement.x > 0f){
                    playerAnimator.SetBool("movingForward", false);
                    playerAnimator.SetBool("movingBackward", true);
                    playerAnimator.SetBool("movingLeft", false);
                    playerAnimator.SetBool("movingRight", false);
                }

                //Play left animation
                else if(movement.z < 0f && movement.x == 0f){
                    playerAnimator.SetBool("movingForward", false);
                    playerAnimator.SetBool("movingBackward", false);
                    playerAnimator.SetBool("movingLeft", true);
                    playerAnimator.SetBool("movingRight", false);
                }

                //Play right animation
                else if(movement.z > 0f && movement.x == 0f){
                    playerAnimator.SetBool("movingForward", false);
                    playerAnimator.SetBool("movingBackward", false);
                    playerAnimator.SetBool("movingLeft", false);
                    playerAnimator.SetBool("movingRight", true);
                }
            }

            //If the player is looking up
            if(mouseAngle > 45 && mouseAngle < 135){

                //Play forward animation
                if(movement.z > 0f){
                    playerAnimator.SetBool("movingForward", true);
                    playerAnimator.SetBool("movingBackward", false);
                    playerAnimator.SetBool("movingLeft", false);
                    playerAnimator.SetBool("movingRight", false);
                }

                //Play backward animation
                else if(movement.z < 0f){
                    playerAnimator.SetBool("movingForward", false);
                    playerAnimator.SetBool("movingBackward", true);
                    playerAnimator.SetBool("movingLeft", false);
                    playerAnimator.SetBool("movingRight", false);
                }

                //Play left animation
                else if(movement.x < 0f && movement.z == 0f){
                    playerAnimator.SetBool("movingForward", false);
                    playerAnimator.SetBool("movingBackward", false);
                    playerAnimator.SetBool("movingLeft", true);
                    playerAnimator.SetBool("movingRight", false);
                }

                //Play right animation
                else if(movement.x > 0f && movement.z == 0f){
                    playerAnimator.SetBool("movingForward", false);
                    playerAnimator.SetBool("movingBackward", false);
                    playerAnimator.SetBool("movingLeft", false);
                    playerAnimator.SetBool("movingRight", true);
                }
            }

            //If the player is looking down
            if(mouseAngle < -45 && mouseAngle > -135){

                //Play forward animation
                if(movement.z < 0f){
                    playerAnimator.SetBool("movingForward", true);
                    playerAnimator.SetBool("movingBackward", false);
                    playerAnimator.SetBool("movingLeft", false);
                    playerAnimator.SetBool("movingRight", false);
                }

                //Play backward animation
                else if(movement.z > 0f){
                    playerAnimator.SetBool("movingForward", false);
                    playerAnimator.SetBool("movingBackward", true);
                    playerAnimator.SetBool("movingLeft", false);
                    playerAnimator.SetBool("movingRight", false);
                }

                //Play left animation
                else if(movement.x > 0f && movement.z == 0f){
                    playerAnimator.SetBool("movingForward", false);
                    playerAnimator.SetBool("movingBackward", false);
                    playerAnimator.SetBool("movingLeft", true);
                    playerAnimator.SetBool("movingRight", false);
                }

                //Play right animation
                else if(movement.x < 0f && movement.z == 0f){
                    playerAnimator.SetBool("movingForward", false);
                    playerAnimator.SetBool("movingBackward", false);
                    playerAnimator.SetBool("movingLeft", false);
                    playerAnimator.SetBool("movingRight", true);
                }
            }
        }

        //Update player HP text
        playerHpText.text = "Player HP: " + healthPoints + " / " + maxHealthPoints;

        //When the player is destroyed
        if(healthPoints <= 0){
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, SoundManager.sfxVolume);
            GameObject explosionEffect = Instantiate(explosionParticle.gameObject, transform.position, transform.rotation);
            Destroy(explosionEffect, 2.0f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Enemy")){
            healthPoints -= Enemy.damage;
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Item")){
            Destroy(other.gameObject);
        }
    }
}