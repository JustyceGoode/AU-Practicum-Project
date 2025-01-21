using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Vector3 mousePosition;
    public Vector3 mousePositionInitial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePositionInitial = Input.mousePosition;
        float temp = mousePositionInitial.y;
        mousePositionInitial.y = mousePositionInitial.z;
        mousePositionInitial.z = temp;

        mousePosition = Camera.main.ScreenToWorldPoint(mousePositionInitial);
        // temp = mousePosition.y;
        // mousePositionInitial.y = mousePosition.z;
        // mousePosition.z = temp;

        //mousePosition.y = Camera.main.transform.position.y + Camera.main.nearClipPlane;

        //transform.position = mousePosition;
    }
}
