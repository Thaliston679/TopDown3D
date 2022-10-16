using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectables : MonoBehaviour
{
    public GameObject[] GetspawnPoints;
    public Transform[] spawnPoints;
    public GameObject prefabHP;
    public float timeToSpawn;
    private float currentTimer = 0;

    public bool[] areaOcupada = { false, false, false, false, false, false, false, false, false, false, false, false };

    void Start()
    {
        DeletInitialPrefabs();
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

    public void DeletInitialPrefabs()
    {
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = GetspawnPoints[i].transform;
            Destroy(GetspawnPoints[i]);
        }
    }

    public void SpawnNewHP(int rand)
    {
        areaOcupada[rand] = true;
        Instantiate(prefabHP, spawnPoints[rand].position, Quaternion.identity);
    }

    void CheckRandomizerPosition()
    {
        int rand = Random.Range(0, spawnPoints.Length);
        if (areaOcupada[rand] == false) SpawnNewHP(rand);
        else CheckRandomizerPosition();
    }
}
