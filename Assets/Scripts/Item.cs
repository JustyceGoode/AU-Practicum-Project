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
        //Medkit
        if(itemId == 0){
            PlayerController.healthPoints += 30;
            if(PlayerController.healthPoints > PlayerController.maxHealthPoints){
                PlayerController.healthPoints = PlayerController.maxHealthPoints;
            }
        }

        //Item to increase health
        if(itemId == 1){
            PlayerController.maxHealthPoints += 20;
            PlayerController.healthPoints += 20;
        }

        //Item to increase damage
        if(itemId == 2){
            PlayerController.attackDamage += 5;
        }
    }
}
