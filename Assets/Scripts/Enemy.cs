using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Get Enemy parts
    //public GameObject enemyWheel;
    public GameObject enemyGun;

    private float speed = 10.0f;
    private Rigidbody enemyRb;
    public GameObject player;

    private float dist;

    private int xBoundary = 7;
    private int zBoundary = 5;

    public GameObject bulletPrefab;
    private float timePassed = 0f;

    private int healthPoints = 30;
    public int playerAttackDamage = PlayerController.attackDamage;

    public ParticleSystem explosionParticle;
    public AudioClip explosionSound;
    //private AudioSource enemyAudio;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        //enemyAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 followDirection = (player.transform.position - transform.position).normalized;
        Vector2 lookDirection = new Vector2(player.transform.position.x, player.transform.position.z) - new Vector2(enemyGun.transform.position.x, enemyGun.transform.position.z);
        lookDirection = lookDirection.normalized;

        dist = Vector3.Distance(transform.position, player.transform.position);

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

        if(PlayerController.isGameActive){
            timePassed += Time.deltaTime;
            if(timePassed > 2f){
                Instantiate(bulletPrefab, enemyGun.transform.position, enemyGun.transform.rotation);
                timePassed = 0f;
            }
        }
        // timePassed += Time.deltaTime;
        // if(timePassed > 2f){
        //     Instantiate(bulletPrefab, enemyGun.transform.position, enemyGun.transform.rotation);
        //     timePassed = 0f;
        // }

        if(healthPoints <= 0){
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 0.4f);
            GameObject explosionEffect = Instantiate(explosionParticle.gameObject, transform.position, transform.rotation);
            Destroy(explosionEffect, 2.0f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            healthPoints -= playerAttackDamage;
            Destroy(other.gameObject);
        }
    }
}
