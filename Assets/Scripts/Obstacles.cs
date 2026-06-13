using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private float minSize = 0.5f;
    [SerializeField] private float maxSize = 2.0f;

    Rigidbody2D rb;

    [SerializeField] private float minSpeed = 50.0f;
    [SerializeField] private float maxSpeed = 150.0f;

    [SerializeField] private float maxSpinSpeed = 10.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        float randomTorque = Random.Range(-maxSpinSpeed, maxSpinSpeed);
        rb.AddTorque(randomTorque);

        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomSize, randomSize, 1);


        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize;
        Vector2 randomDirection = Random.insideUnitCircle;
        rb.AddForce(randomDirection * randomSpeed);

    }


    void Update()
    {

    }
}
