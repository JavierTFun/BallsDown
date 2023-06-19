using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject holePrefab;
    private float spawnXMin = 730;
    private float spawnXMax = 800;
    private float spawnZMin = -20;
    private float spawnZMax = 20;
    public int playerCount;
    public int holeCount;
    public int waveNumber = 1;
    public int maxHolesPerWave = 5;

    private float holeSize;
    private float holeSizeMin = 3f;

    void Start()
    {
        //SpawnHoleWave(waveNumber);
        //SpawnPlayerWave(waveNumber);
    }

    void Update()
    {
        playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        holeCount = GameObject.FindGameObjectsWithTag("Hole").Length;

        if (playerCount == 0 && holeCount == 0)
        {
            waveNumber++;
            SpawnHoleWave(waveNumber);
            SpawnPlayerWave(waveNumber);
        }
    }

    void SpawnHoleWave(int holeToSpawn)
    {
        int holesToGenerate = Mathf.Min(holeToSpawn, maxHolesPerWave) - holeCount;

        bool generateLargeHole = true;

        for (int i = 0; i < holesToGenerate; i++)
        {
            Vector3 spawnPosition = GenerateSpawnPosition();

            if (generateLargeHole)
            {
                holeSize = Random.Range(7f, 8f);
                generateLargeHole = false;
            }
            else
            {
                holeSize = Random.Range(holeSizeMin, 7f);
            }

            GameObject newHole = Instantiate(holePrefab, spawnPosition, Quaternion.identity);
            newHole.transform.localScale = new Vector3(holeSize, 0, holeSize);
        }
    }

    void SpawnPlayerWave(int playerToSpawn)
    {
        int playersToGenerate = playerToSpawn - playerCount;

        for (int i = 0; i < playersToGenerate; i++)
        {
            Vector3 spawnPosition = GenerateSpawnPosition();
            float playerScale = Random.Range(2f, 7f);

            GameObject newPlayer = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            newPlayer.transform.localScale = new Vector3(playerScale, playerScale, playerScale);
        }
    }

    Vector3 GenerateSpawnPosition()
    {
        float spawnPositionX = Random.Range(spawnXMin, spawnXMax);
        float spawnPositionZ = Random.Range(spawnZMin, spawnZMax);
        Vector3 randomPos = new Vector3(spawnPositionX, 100, spawnPositionZ);
        return randomPos;
    }
}