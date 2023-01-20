using UnityEngine;

public class playerCube : MonoBehaviour
{
    public bool autoMove;
    private float verticalInput, horizontalInput, timeRemaining = 10;
    bool jumpKeyPressed, boost = false;
    private Rigidbody rigidBodyComponent;
    int jumpsRemaining;
    float defaultSpeedZ = 7f, defaultSpeedX = 2.4f; //z is forward and x is sideways

    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //bug not fixed yet. Check collision with ground
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && jumpsRemaining > 0 && GetComponent<Transform>().position.y <= 0.9973934)
        {
            jumpKeyPressed = true;
            jumpsRemaining -= 1;
            jump();
            Debug.Log("Jumps remaining" + jumpsRemaining);
        }
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        if (boost == true)
        {
            if (timeRemaining > 1)
            {
                Debug.Log("Timer is running" + timeRemaining);
                timeRemaining -= Time.deltaTime;
            }
            else if (timeRemaining < 1)
            {
                Debug.Log("Timer has run out");
                boost = false;
                timeRemaining = 10;
                defaultSpeedZ= 7f;
            }
        }
    }
    private void FixedUpdate()
    {
        if (autoMove == true)
        {
            rigidBodyComponent.velocity = new Vector3(horizontalInput * defaultSpeedX, rigidBodyComponent.velocity.y, defaultSpeedZ);
        }
        else
        {
            rigidBodyComponent.velocity = new Vector3(horizontalInput * defaultSpeedX, rigidBodyComponent.velocity.y, verticalInput * defaultSpeedZ);
        }
    }
    private void jump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * 6f, ForceMode.VelocityChange);
        Debug.Log("Jumps remaining: " + jumpsRemaining);
        jumpKeyPressed = false;
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
            Destroy(other.gameObject);
            speedBoost();
        }
        if (other.gameObject.layer == 8 && GetComponent<Transform>().position.y == 291.78)
        {
            defaultSpeedX = 0;
            defaultSpeedZ = 0;
        }
    }
    private void speedBoost()
    {
        boost = true;
        defaultSpeedZ = 14f;
    }
}