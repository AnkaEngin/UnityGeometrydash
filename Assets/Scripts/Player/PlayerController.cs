using UnityEngine;

// Handles player jump mechanics with arc motion and rotation
public class PlayerController : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float jumpDuration = 0.5f;
    [SerializeField] private float rotationAmount = 180f;
    
    [Header("Audio")]
    [SerializeField] private AudioClip jumpSound;
    
    private Vector3 startPosition;
    private float jumpTimer;
    private bool isJumping;
    private float startRotation;
    private AudioSource audioSource;
    
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.eulerAngles.z;
        
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    
    void Update() // Input detection and jump handler
    {
        if (!isJumping && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            StartJump();
        }
        
        if (isJumping)
        {
            AnimateJump();
        }
    }
    
    private void StartJump() // Initiates jump and plays sound
    {
        isJumping = true;
        jumpTimer = 0f;
        
        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }
    
    private void AnimateJump() // Handles jump arc and rotation
    {
        jumpTimer += Time.deltaTime;
        float progress = jumpTimer / jumpDuration;
        
        if (progress >= 1f)
        {
            transform.position = startPosition;
            isJumping = false;
            jumpTimer = 0f;
            return;
        }

        float heightOffset = 4f * jumpHeight * progress * (1f - progress);
        
        Vector3 newPosition = startPosition;
        newPosition.y += heightOffset;
        transform.position = newPosition;
        
        float currentRotation = startRotation + (progress * rotationAmount);
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);
    }
}