using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

// Manages the game state, score tracking, death sequence, and audio
public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private CanvasGroup deathPanelCanvasGroup;
    
    [Header("Audio")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip deathSound;
    
    [Header("Death Animation")]
    [SerializeField] private float deathPanelDelay = 2f;
    [SerializeField] private float deathPanelFadeDuration = 1f;
    
    [Header("Game References")]
    [SerializeField] private SpikeGroupSpawner spikeSpawner;
    
    private int score;
    private float scoreTimer;
    private bool isGameOver;
    private AudioSource sfxSource;
    
    void Start()
    {
        // Create separate audio source for sound effects
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
    }
    
    void Update() // Tick counter
    {
        if (isGameOver) return;
        
        // Increment score every second
        scoreTimer += Time.deltaTime;
        if (scoreTimer >= 1f)
        {
            score++;
            UpdateScoreDisplay();
            scoreTimer = 0f;
        }
    }
    
    public void GameOver() // Stops all functionality and shows gameover panel
    {
        if (isGameOver) return;
        isGameOver = true;
        
        StopMusic();
        PlayDeathSound();
        StopSpawningObstacles();
        PauseGame();
        ShowFinalScore();
        
        StartCoroutine(ShowDeathPanel());
    }
    
    private void UpdateScoreDisplay() // Score updater
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }
    
    private void StopMusic() // Stops music on death
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }
    
    private void PlayDeathSound() // Death sound effect player
    {
        if (deathSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(deathSound);
        }
    }
    
    private void StopSpawningObstacles() // Stops spawning spikes upon death
    {
        if (spikeSpawner != null)
        {
            spikeSpawner.StopSpawning();
        }
    }

    private void PauseGame() // Pausing mechanism
    {
        Time.timeScale = 0f;
    }
    
    private void ShowFinalScore() // Shows final score
    {
        if (finalScoreText != null)
        {
            finalScoreText.text = $"Score: {score}";
        }
    }
    
    private IEnumerator ShowDeathPanel()
    {
        if (deathPanelCanvasGroup == null) yield break;
        
        // Setup panel
        deathPanelCanvasGroup.gameObject.SetActive(true);
        deathPanelCanvasGroup.alpha = 0f;
        
        // Wait before showing panel
        yield return new WaitForSecondsRealtime(deathPanelDelay);
        
        // Fade in panel
        float elapsed = 0f;
        while (elapsed < deathPanelFadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float fadeProgress = elapsed / deathPanelFadeDuration;
            deathPanelCanvasGroup.alpha = Mathf.Lerp(0f, 1f, fadeProgress);
            yield return null;
        }
        
        // Ensures fully visible
        deathPanelCanvasGroup.alpha = 1f;
    }
}