using Unity.VisualScripting;
using UnityEngine;

public class orangeBlock : MonoBehaviour
{
    [SerializeField] float distanceToCover;
    [SerializeField] float speed;

    private Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }
    void Update()
    {
        Vector3 v = startingPosition;
        v.x += distanceToCover * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 11)
        {
            distanceToCover = 0;
            speed = 0;
        }
    }
}