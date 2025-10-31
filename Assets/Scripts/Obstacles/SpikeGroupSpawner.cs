using UnityEngine;

// Spawns groups of spikes with random gaps and timing
public class SpikeGroupSpawner : MonoBehaviour
{
    [Header("Spike Settings")]
    [SerializeField] private GameObject spikePrefab;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private float spikeWidth = 66f;
    [SerializeField] private float spikeSpeed = 900f;
    
    [Header("Spawn Settings")]
    [SerializeField] private float spawnX = 2000f;
    [SerializeField] private float groundY = 150f;
    
    [Header("Group Settings")]
    [SerializeField] private int minSpikesPerGroup = 1;
    [SerializeField] private int maxSpikesPerGroup = 3;
    
    [Header("Gap Settings")]
    [SerializeField] private float minGapInSpikeWidths = 5f;
    [SerializeField] private float maxGapInSpikeWidths = 10f;
    
    private float timeSinceLastSpawn;
    private float nextSpawnDelay;
    
    void Start()
    {
        SpawnSpikeGroup();
        CalculateNextSpawnDelay();
    }
    
    void Update() // Spawn timer
    {
        timeSinceLastSpawn += Time.deltaTime;
        
        if (timeSinceLastSpawn >= nextSpawnDelay)
        {
            SpawnSpikeGroup();
            CalculateNextSpawnDelay();
            timeSinceLastSpawn = 0f;
        }
    }
    
    private void SpawnSpikeGroup() // Creates random spike groups
    {
        if (spikePrefab == null || canvas == null) return;
        
        int spikeCount = Random.Range(minSpikesPerGroup, maxSpikesPerGroup + 1);
        
        for (int i = 0; i < spikeCount; i++)
        {
            SpawnSingleSpike(spawnX + (i * spikeWidth));
        }
    }
    
    private void SpawnSingleSpike(float xPosition) // Spawns individual spike at position
    {
        GameObject spike = Instantiate(spikePrefab, canvas);
        RectTransform spikeRect = spike.GetComponent<RectTransform>();
        
        if (spikeRect != null)
        {
            spikeRect.anchoredPosition = new Vector2(xPosition, groundY);
        }
    }
    
    private void CalculateNextSpawnDelay() // Calculates random gap timing
    {
        float gapInSpikeWidths = Random.Range(minGapInSpikeWidths, maxGapInSpikeWidths);
        float gapInPixels = gapInSpikeWidths * spikeWidth;
        
        nextSpawnDelay = gapInPixels / spikeSpeed;
    }
    
    public void StopSpawning() // Disables spawner
    {
        enabled = false;
    }
}