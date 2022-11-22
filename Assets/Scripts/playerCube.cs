using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCube : MonoBehaviour
{
    private float verticalInput;
    private float horizontalInput;
    bool jumpKeyWasPressed;
    private Rigidbody rigidBodyComponent;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        //rigidBodyComponent.velocity = new Vector3(horizontalInput, rigidBodyComponent.velocity.y, 4f);
        rigidBodyComponent.velocity = new Vector3(horizontalInput, rigidBodyComponent.velocity.y, verticalInput*3);
        if(jumpKeyWasPressed == true)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
    }
}
