using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int itemId;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if(itemId == 0){
            PlayerController.healthPoints += 30;
            if(PlayerController.healthPoints > PlayerController.maxHealthPoints){
                PlayerController.healthPoints = PlayerController.maxHealthPoints;
            }
            //Destroy(gameObject);
        }
        if(itemId == 1){
            PlayerController.maxHealthPoints += 50;
            PlayerController.healthPoints += 50;
            //Destroy(gameObject);
        }
    }
}
