using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectables : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public float timeToSpawn;
    private float currentTimer = 0;

    public bool[] areaOcupada = { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };

    void Start()
    {
        DesactiveInitialPrefabs();
    }

    // Update is called once per frame
    void Update()
    {
        TimerSpawn();
    }

    public void TimerSpawn()
    {
        currentTimer += Time.deltaTime;

        if(currentTimer >= timeToSpawn)
        {
            currentTimer = 0;
            CheckRandomizerPosition();
        }
    }

    public void DesactiveInitialPrefabs()
    {
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i].SetActive(false);
        }
    }

    public void SpawnNewHP(int rand)
    {
        areaOcupada[rand] = true;
        spawnPoints[rand].SetActive(true);
    }

    void CheckRandomizerPosition()
    {
        int rand = Random.Range(0, spawnPoints.Length);
        if (areaOcupada[rand] == false) SpawnNewHP(rand);
        else CheckRandomizerPosition();
    }

    public void CheckCollect()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!spawnPoints[i].activeSelf)
            {
                areaOcupada[i] = false;
            }
        }
    }
}
