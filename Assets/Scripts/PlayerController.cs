using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Get components of the player
    public GameObject playerWheel;
    public GameObject playerGun;

    //Input Variables
    public float horizontalInput;
    public float verticalInput;
    
    private Rigidbody playerWheelRb;
    public float speed = 3.0f;

    //public Vector3 mousePosition3;
    //public Vector2 mousePosition2;

    // Start is called before the first frame update
    void Start()
    {
        playerWheelRb = playerWheel.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get Input for player movement
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        // transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        // transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);

        playerWheelRb.AddForce(Vector3.forward * speed * verticalInput);
        playerWheelRb.AddForce(Vector3.right * speed * horizontalInput);

        playerGun.transform.position = playerWheelRb.transform.position + new Vector3(0,0.5f,0); //Gun follows player

        //Code for the gun to lock at the mouse
        Vector3 mousePositionScreen = Input.mousePosition;
        mousePositionScreen.z = Camera.main.transform.position.y - 1;
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);

        Vector2 direction = new Vector2(mousePositionWorld.x, mousePositionWorld.z) - new Vector2(playerGun.transform.position.x, playerGun.transform.position.z);
        float mouseAngle = Vector2.SignedAngle(Vector2.right, direction);
        playerGun.transform.eulerAngles = new Vector3 (0, -mouseAngle, 90);
    }
}
