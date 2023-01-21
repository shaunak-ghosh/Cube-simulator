using UnityEngine;
public class playerCube : MonoBehaviour
{
    public bool autoMove;
    private float verticalInput, horizontalInput, timeRemaining = 6;
    bool jumpable,jumpKeyPressed = false, boost = false, move = true;
    private Rigidbody rigidBodyComponent;
    int jumpsRemaining;
    float defaultSpeedZ = 7f, defaultSpeedX = 2.4f;
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (move == true)
        {
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && jumpsRemaining >=1 && jumpable == true)
            {
                jumpKeyPressed = true;
                jumpsRemaining -= 1;
                jump();
            }
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");
        }
        if (boost == true)
        {
            if (timeRemaining > 1)
            {
                timeRemaining -= Time.deltaTime;
            }
            else if (timeRemaining < 1)
            {
                boost = false;
                timeRemaining = 6;
                defaultSpeedZ= 7f;
            }
        }
    }
    private void FixedUpdate()
    {
        if (move == true)
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
    }
    private void jump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * 6f, ForceMode.VelocityChange);
        Debug.Log("Jumps remaining: " + jumpsRemaining);
        jumpable = false;
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
        if(other.gameObject.layer == 8)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        }
        if(other.gameObject.layer == 9)
        {
            jumpable = true;
        }
    }
    private void speedBoost()
    {
        boost = true;
        defaultSpeedZ = 14f;
    }
}