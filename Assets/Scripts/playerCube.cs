using System;
using System.Threading;
using UnityEngine;

public class playerCube : MonoBehaviour
{
    public bool autoMove;
    private float verticalInput, horizontalInput;
    bool jumpKeyPressed;
    private Rigidbody rigidBodyComponent;
    int jumpsRemaining;
    float defaultSpeedY = 7f, defaultSpeedX = 2.4f, normalSpeedY, speedCoolDown; //normal speed

    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)&& jumpsRemaining > 0)
        {
            jumpKeyPressed = true;
            jumpsRemaining -= 1;
            Debug.Log("Jumps remaining" + jumpsRemaining);
        }
        if (Input.GetKeyDown(KeyCode.Space)&& jumpsRemaining > 0)
        {
            jumpKeyPressed = true;
            jumpsRemaining -= 1;
            Debug.Log("Jumps remaining" + jumpsRemaining);
        }
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        if (autoMove == true)
        {
            rigidBodyComponent.velocity = new Vector3(horizontalInput * defaultSpeedX, rigidBodyComponent.velocity.y, defaultSpeedY);
        }
        else
        {
            rigidBodyComponent.velocity = new Vector3(horizontalInput * defaultSpeedX, rigidBodyComponent.velocity.y, verticalInput * defaultSpeedY);
        }
        if (jumpKeyPressed == true)
        {
            jump();
            jumpKeyPressed = false;
        }
    }
    private void jump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * 6f, ForceMode.VelocityChange);
        Debug.Log("Jumps remaining: " + jumpsRemaining);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Destroy(other.gameObject);
            jumpsRemaining++;
        }
        if (other.gameObject.layer == 7)
        {
            defaultSpeedY = 3.0f;
            speedBoost();
        }
        if (other.gameObject.layer == 8)
        {
            rigidBodyComponent.velocity = new Vector3(0,0,0);
        }
    }
    private void speedBoost()
    {
        throw new NotImplementedException();
    }
}