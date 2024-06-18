using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviourSingleton<BallManager>
{
    public Transform spawnPoint1, spawnPoint2;
    private float timer;
    private float spawnTime = 1;

    public void StartSpawn()
    {
        timer = spawnTime;
    }

    public void Clear()
    {
        BallSpawner.Instance.Clear();
    }

    private void Update()
    {
        if (!GameManager.Instance.isPlaying) return;
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer = spawnTime;

            var ball1 = BallSpawner.Instance.Spawn();
            ball1.transform.position = spawnPoint1.position;
            ball1.Init();

            var ball2 = BallSpawner.Instance.Spawn();
            ball2.transform.position = spawnPoint2.position;
            ball2.Init();
        }
    }
}
