using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableSpawner : MonoBehaviour
{
    public GameObject fish;
    public GameObject bone;
    public KirasMovement kirasMovement;
    public Transform[] spawnPositions;
    public float spawnInterval = 2f;

    private float timeSinceLastSpawn = 0f;
    public UnityEvent OnResetGame;

    public void OnEnable()
    {
        ScoreManager.ResetPlayScore();
        SpawnObject();
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnObject();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnObject()
    {
        int randomIndex = Random.Range(0, spawnPositions.Length);
        Transform spawnPosition = spawnPositions[randomIndex];
        Collectable collectable = Instantiate(Random.value < 0.7f ? fish : bone, spawnPosition).GetComponent<Collectable>();
        collectable.kira = kirasMovement;
        collectable.OnResetGame += () =>
        {
            foreach (Transform spawnPoint in spawnPositions)
                foreach (Transform childs in spawnPoint)
                    childs.gameObject.SetActive(false);
            collectable.gameObject.SetActive(false);

            timeSinceLastSpawn = 0f;
            OnResetGame?.Invoke();
        };
    }
}
