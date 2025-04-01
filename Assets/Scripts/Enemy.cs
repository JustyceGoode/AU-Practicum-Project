using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Get Enemy parts
    //public GameObject enemyGun;

    //Enemy movement variables
    //private float speed = 1.0f;
    private Rigidbody enemyRb;
    public GameObject player;
    private int xBoundary = 15;
    private int zBoundary = 8;

    //Enemy bullets
    public GameObject bulletPrefab;
    private float timePassed = 0f;

    //Enemy stats
    public int healthPoints;
    public int EnemyId;
    public static int damage;
    public static int scorePoints;

    //Enemy Direction
    Vector3 followDirection = new Vector3(0,0,0); //Direction that the enemy follows the player
    Vector2 lookDirection = new Vector2(0,0); //Direction that the enemy looks at the player
    float lookAngle = 0;
    float lookRadian = 0;
    float dist = 0; //Distance between enemy and player

    //Explosion particle variables
    public ParticleSystem explosionParticle;
    public AudioClip explosionSound;

    public UnityEngine.AI.NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        //EnemyId is a public, non-static variable, so it can be modified in the Unity interface but can't be used to modify variables in other programs.
        //A non-static variable has to be assigned to a static variable in the start function.
        damage = EnemyId * 5 + 10;
        //damage = 0;
        scorePoints = EnemyId * 10 + 10;
    }

    // Update is called once per frame
    void Update()
    {
        //This code will be ignored when the player is destroyed.
        if(WaveManager.isGameActive){
            followDirection = (player.transform.position - transform.position).normalized;
            lookDirection = new Vector2(player.transform.position.x, player.transform.position.z) - new Vector2(transform.position.x, transform.position.z);
            lookDirection = lookDirection.normalized;

            lookAngle = Vector2.SignedAngle(Vector2.right, lookDirection);
            transform.eulerAngles = new Vector3 (0, -lookAngle, 0);
            lookRadian = (lookAngle / 180) * (Mathf.PI);

            timePassed += Time.deltaTime;
            if(timePassed > 2f){
                Instantiate(bulletPrefab, transform.position + new Vector3(1.25f * Mathf.Cos(lookRadian), 0, 1.25f  *Mathf.Sin(lookRadian)), Quaternion.Euler(new Vector3(0, -lookAngle, 90)));
                timePassed = 0f;
            }

            dist = Vector3.Distance(transform.position, player.transform.position);

            // if(dist > 5.0f){
            //     agent.SetDestination(player.transform.position);
            // }
            agent.SetDestination(player.transform.position);

            // if(dist > 3.25f){
            //     //enemyRb.AddForce(followDirection * speed);
            //     //Vector3 movement = followDirection * speed;
            //     enemyRb.MovePosition(enemyRb.position + followDirection * speed * Time.fixedDeltaTime * Time.timeScale);
            // }
            // else if(dist < 2.75f){
            //     //enemyRb.AddForce(-followDirection * speed);
            //     //Vector3 movement = followDirection * speed;
            //     enemyRb.MovePosition(enemyRb.position - followDirection * speed * Time.fixedDeltaTime * Time.timeScale);
            // }

            //Keep enemy in bounds
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
        }

        // if(dist > 3.25f){
        //     //enemyRb.AddForce(followDirection * speed);
        //     //Vector3 movement = followDirection * speed;
        //     enemyRb.MovePosition(enemyRb.position + followDirection * speed * Time.fixedDeltaTime * Time.timeScale);
        // }
        // else if(dist < 2.75f){
        //     //enemyRb.AddForce(-followDirection * speed);
        //     //Vector3 movement = followDirection * speed;
        //     enemyRb.MovePosition(enemyRb.position - followDirection * speed * Time.fixedDeltaTime * Time.timeScale);
        // }

        // //Keep enemy in bounds
        // if(transform.position.x > xBoundary){
        //     transform.position = new Vector3(xBoundary, transform.position.y, transform.position.z);
        // }
        // if(transform.position.x < -xBoundary){
        //     transform.position = new Vector3(-xBoundary, transform.position.y, transform.position.z);
        // }
        // if(transform.position.z > zBoundary){
        //     transform.position = new Vector3(transform.position.x, transform.position.y, zBoundary);
        // }
        // if(transform.position.z < -zBoundary){
        //     transform.position = new Vector3(transform.position.x, transform.position.y, -zBoundary);
        // }

        //enemyWheelRb.AddForce(followDirection * speed);
        //enemyGun.transform.position = transform.position + new Vector3(0,0.5f,0); //Gun follows enemy
        
        //To update the damage from the power up
        //playerAttackDamage = PlayerController.attackDamage; 

        //When the enemy is destroyed
        if(healthPoints <= 0){
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, SoundManager.sfxVolume);
            GameObject explosionEffect = Instantiate(explosionParticle.gameObject, transform.position, transform.rotation);
            Destroy(explosionEffect, 2.0f);
            Destroy(gameObject);
            WaveManager.score += scorePoints;
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            healthPoints -= PlayerController.attackDamage;
            //Debug.Log("Enemy HP: " + healthPoints);
            //Debug.Log("Enemy Collision Detected");
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Enemy")){
            Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>()); //For this to work, Enemy's can't be tagged
        }
    }
}
