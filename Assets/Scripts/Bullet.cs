using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = -15.0f;
    private float boundary = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(WaveManager.isGameActive){
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        //transform.Translate(Vector3.up * speed * Time.deltaTime);

        if(transform.position.x > boundary || transform.position.x < -boundary){
            Destroy(gameObject);
        }

        if(transform.position.z > boundary || transform.position.z < -boundary){
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other){
        //Debug.Log("Collision detected");
        if(other.gameObject.CompareTag("Box")){
            //Debug.Log("Collision detected");
            Destroy(gameObject);
        }
    }
}
