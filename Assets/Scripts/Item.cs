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
        //TODO: Balance medkits to make them viable compared to health up items.
        //TODO: There is a bug where the if the player heals over their max health, it doesn't revert down.
        if(itemId == 0){
            PlayerController1.healthPoints += 30;
            if(PlayerController1.healthPoints > PlayerController1.maxHealthPoints){
                PlayerController1.healthPoints = PlayerController1.maxHealthPoints;
            }
        }

        //Item to increase health
        if(itemId == 1){
            PlayerController1.maxHealthPoints += 20;
            PlayerController1.healthPoints += 20;
        }

        //Item to increase damage
        if(itemId == 2){
            PlayerController1.attackDamage += 5;
        }
    }
}
