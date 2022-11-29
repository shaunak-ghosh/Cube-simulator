using System;
using System.Threading;
using UnityEngine;

public class playerCube : MonoBehaviour
{
    public bool autoMove;
    private float verticalInput;
    private float horizontalInput;
    bool jumpKeyWasPressed;
    private Rigidbody rigidBodyComponent;
    int jumpsRemaining;
    float defaultSpeed = 2.4f;

    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            jumpKeyWasPressed = true;
            jumpsRemaining -= 1;
        }
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        if(autoMove == true)
        {
            rigidBodyComponent.velocity = new Vector3(horizontalInput * defaultSpeed, rigidBodyComponent.velocity.y, 7f);
        }
        else 
        { 
            rigidBodyComponent.velocity = new Vector3(horizontalInput * defaultSpeed, rigidBodyComponent.velocity.y, verticalInput*7);
        }
        if(jumpKeyWasPressed == true)
        {
            jump();
            jumpKeyWasPressed = false;
        }
    }
    private void jump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * 6f, ForceMode.VelocityChange);
        Debug.Log("Jumps remaining: " + jumpsRemaining);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            Destroy(other.gameObject);//not needed!
            jumpsRemaining++; 
        }
        if(other.gameObject.layer == 7)
        {
            defaultSpeed = 3.0f;
            speedUpTimer();
        }
    }
    private void speedUpTimer()
    {
        throw new NotImplementedException();
    }
}
