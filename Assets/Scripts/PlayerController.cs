using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private float elapsedTime = 0f;
    private float score = 0f;
    [SerializeField] private float scoreMultipier = 10f;

    [SerializeField] private float thrustForce;
    [SerializeField] private float maxSpeed;

    Rigidbody2D rb;

    [SerializeField] private UIDocument uiDocument;
    private Label scoreText;

    public GameObject busterFlame;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
    }

    void Update()
    {
        HandleScore();

        HandleMovement();

        HandlleBusterFlame();

        HandlePlayerMaxSpeed();

    }

    private void HandleScore()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultipier);
        Debug.Log("Score " + score);
        scoreText.text = "Score " + score;
    }

    private void HandlePlayerMaxSpeed()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            // Set Player max speed
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    private void HandleMovement()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            // Calculate mouse direction
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Vector2 direction = (mousePos - transform.position).normalized;

            //Move 
            transform.up = direction;
            rb.AddForce(direction * thrustForce);
        }
    }

    private void HandlleBusterFlame()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            busterFlame.SetActive(true);
        }

        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            busterFlame.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
