using UnityEngine;

// Individual spike obstacle that moves and triggers game over on collision
public class Spike : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 900f;
    
    private RectTransform rectTransform;
    
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    
    void Update() // Movement and cleanup
    {
        // Move left
        rectTransform.anchoredPosition += Vector2.left * moveSpeed * Time.deltaTime;
        
        // Destroy when off-screen
        if (rectTransform.anchoredPosition.x < -1000f)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other) // Collision detection
    {
        if (other.CompareTag("Player"))
        {
            TriggerGameOver();
        }
    }
    
    private void TriggerGameOver() // Finds game manager and calls game over
    {
        GameManager gameManager = FindAnyObjectByType<GameManager>();
        if (gameManager != null)
        {
            gameManager.GameOver();
        }
    }
}