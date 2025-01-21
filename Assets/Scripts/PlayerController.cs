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

    public Vector3 mousePosition3;
    public Vector2 mousePosition2;

    // Start is called before the first frame update
    void Start()
    {
        playerWheelRb = playerWheel.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        // transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        // transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);

        playerWheelRb.AddForce(Vector3.forward * speed * verticalInput);
        playerWheelRb.AddForce(Vector3.right * speed * horizontalInput);

        playerGun.transform.position = playerWheelRb.transform.position + new Vector3(0,0.5f,0);

        mousePosition3 = Input.mousePosition;
        mousePosition2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
