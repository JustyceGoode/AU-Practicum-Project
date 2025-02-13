using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Get Enemy parts
    public GameObject enemyGun;

    //Enemy movement variables
    private float speed = 10.0f;
    private Rigidbody enemyRb;
    public GameObject player;
    private int xBoundary = 15;
    private int zBoundary = 8;

    //Enemy bullets
    public GameObject bulletPrefab;
    private float timePassed = 0f;

    //Enemy stats
    //TODO Balance enemy stats
    public int healthPoints;
    public int EnemyId;
    public static int damage;
    public static int scorePoints;
    //private int playerAttackDamage; //I'm making this a seperate variable because PlayerController.attackDamage doesn't work for damage;

    //Explosion particle variables
    public ParticleSystem explosionParticle;
    public AudioClip explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        //EnemyId is a public, non-static variable, so it can be modified in the Unity interface but can't be used to modify variables in other programs.
        //A non-static variable has to be assigned to a static variable in the start function.
        damage = EnemyId * 5 + 10;
        scorePoints = EnemyId * 10 + 10;
    }

    // Update is called once per frame
    void Update()
    {
        //Direction for the enemy to follow the player
        Vector3 followDirection = (player.transform.position - transform.position).normalized;

        //Direction for the enemy to aim at the player
        Vector2 lookDirection = new Vector2(player.transform.position.x, player.transform.position.z) - new Vector2(enemyGun.transform.position.x, enemyGun.transform.position.z);
        lookDirection = lookDirection.normalized;

        //Have the enemy follow the player at a distance
        float dist = Vector3.Distance(transform.position, player.transform.position);

        if(dist > 3.25f){
            enemyRb.AddForce(followDirection * speed);
        }
        else if(dist < 2.75f){
            enemyRb.AddForce(-followDirection * speed);
        }

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

        //enemyWheelRb.AddForce(followDirection * speed);
        enemyGun.transform.position = transform.position + new Vector3(0,0.5f,0); //Gun follows enemy

        float lookAngle = Vector2.SignedAngle(Vector2.right, lookDirection);
        enemyGun.transform.eulerAngles = new Vector3 (0, -lookAngle, 90);
        float lookRadian = (lookAngle / 180) * (Mathf.PI);

        //Shoot bullets while the game is active
        if(WaveManager.isGameActive){
            timePassed += Time.deltaTime;
            if(timePassed > 2f){
                Instantiate(bulletPrefab, enemyGun.transform.position + new Vector3(1.25f * Mathf.Cos(lookRadian), 0, 1.25f  *Mathf.Sin(lookRadian)), enemyGun.transform.rotation);
                //Instantiate(bulletPrefab, transform.position + new Vector3(pointerDistance*Mathf.Cos(mouseRadian), 1, pointerDistance*Mathf.Sin(mouseRadian)), Quaternion.Euler(new Vector3(0, -mouseAngle, 90)));
                timePassed = 0f;
            }
        }
        
        //To update the damage from the power up
        //playerAttackDamage = PlayerController.attackDamage; 

        //When the enemy is destroyed
        if(healthPoints <= 0){
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 0.4f);
            GameObject explosionEffect = Instantiate(explosionParticle.gameObject, transform.position, transform.rotation);
            Destroy(explosionEffect, 2.0f);
            Destroy(gameObject);
            WaveManager.score += scorePoints;
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            //healthPoints -= playerAttackDamage;
            healthPoints -= PlayerController.attackDamage;
            //Debug.Log("Enemy HP: " + healthPoints);
            Destroy(other.gameObject);
        }
    }
}
