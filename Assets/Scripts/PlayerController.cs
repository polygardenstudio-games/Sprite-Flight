using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
    private Button restartButton;

    [SerializeField] private GameObject explosionEffect;

    public GameObject busterFlame;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += ReloadScene;
        
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
        Instantiate(explosionEffect, transform.position, transform.rotation);
        restartButton.style.display = DisplayStyle.Flex;
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
