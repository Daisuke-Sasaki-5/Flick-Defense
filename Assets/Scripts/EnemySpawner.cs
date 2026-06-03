using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;

    [SerializeField] private float startInterval = 1.5f;
    [SerializeField] private float minInterval = 0.3f;
    [SerializeField] private float intervalDecreaseRate = 0.02f;

    float margin = 0.5f;

    private float elapsedTIme;
    private bool isSpawning;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (GameManager.instance.isGameOver) return;

        if(!GameManager.instance.IsStarted) return;

        elapsedTIme += Time.deltaTime;

        if(!isSpawning)
        {
            StartCoroutine(SpawnRoutine());
        }
    }

    private IEnumerator SpawnRoutine()
    {
        isSpawning = true;

        float interval = MathF.Max(minInterval, startInterval - elapsedTIme * intervalDecreaseRate);

        SpawnEnemy();

        yield return new WaitForSeconds(interval);

        isSpawning = false;
    }

    private void SpawnEnemy()
    {
        Vector3 leftTop = cam.ViewportToWorldPoint(new Vector3(0f, 1f, 0f));
        Vector3 rightTop = cam.ViewportToWorldPoint(new Vector3(1f,1f, 0f));

        float x = UnityEngine.Random.RandomRange(leftTop.x + margin, rightTop.x - margin);
        float y = leftTop.y + 2f;

        Instantiate(enemyPrefab,new Vector3(x,y,0),Quaternion.identity);
    }
}
