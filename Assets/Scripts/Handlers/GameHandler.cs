using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameHandler : MonoBehaviour
{
    public PlayerMovementHandler MovementHandler;
    public bool IsRunning = true;
    public int Score = 0;

    public List<SpawnPoint> SpawnPoints = new List<SpawnPoint>();
    public GameObject EnemyPrefab;
    public Transform EnemySpace;

    public TimeSpan WaveCountdown = new TimeSpan();

    private DateTime _lastSpawnTime = DateTime.MinValue;

    //Game loop
    void Update()
    {
        if (IsRunning)
        {
            MovementHandler.HandleInputs();

            DateTime now = DateTime.Now;
            WaveCountdown = Shortcuts.ROUND_DURATION - (now - _lastSpawnTime);
            if (WaveCountdown.TotalSeconds <= 0)
            {
                SpawnEnemy();
                _lastSpawnTime = now;
            }
        }
    }

    public void SetRunning(bool state)
    {
        IsRunning = state;
    }

    public void SpawnEnemy()
    {
        for (int i = 0; i < Shortcuts.ENEMIES_PER_ROUND; i++) 
        {
            SpawnPoint spawnSelected = SpawnPoints.FirstOrDefault(x => x.OccupiedBy == null && Vector3.Distance(x.transform.position, Shortcuts.CHARACTER.transform.position) > Shortcuts.ENEMY_SPAWN_DISTANCE);
            if (spawnSelected != null)
            {
                Vector3 shift = new Vector3
                {
                    x = UnityEngine.Random.Range(-1f, 1f) * Shortcuts.ENEMY_SPAWN_RADIUS,
                    y = 0,
                    z = UnityEngine.Random.Range(-1f, 1f) * Shortcuts.ENEMY_SPAWN_RADIUS
                };
                spawnSelected.OccupiedBy = GameObject.Instantiate(EnemyPrefab, spawnSelected.transform.position + shift, new Quaternion(), EnemySpace).GetComponent<Enemy>();
            }
        } 
    }
}
