using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

// Handles button click animation and scene loading
public class LevelButton : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string sceneName;
    
    [Header("Animation Settings")]
    [SerializeField] private float scaleAmount = 0.9f;
    [SerializeField] private float animationDuration = 0.1f;
    [SerializeField] private float delayBeforeLoad = 1f;
    
    private Vector3 originalScale;
    private Button button;
    private bool isAnimating;
    
    void Start()
    {
        originalScale = transform.localScale;
        button = GetComponent<Button>();
        
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }
    
    public void OnButtonClick() // Triggers button animation
    {
        if (!isAnimating)
        {
            StartCoroutine(ButtonClickAnimation());
        }
    }
    
    private IEnumerator ButtonClickAnimation() // Animates button and loads scene
    {
        isAnimating = true;
        
        if (button != null)
        {
            button.interactable = false;
        }
        
        // Scale down animation
        float elapsed = 0f;
        while (elapsed < animationDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float progress = elapsed / animationDuration;
            transform.localScale = Vector3.Lerp(originalScale, originalScale * scaleAmount, progress);
            yield return null;
        }
        
        // Wait before loading
        yield return new WaitForSecondsRealtime(delayBeforeLoad - animationDuration);
        
        // Resume time and load scene
        Time.timeScale = 1f;
        
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}