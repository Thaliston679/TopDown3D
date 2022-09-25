using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    private float contaTempoOnda;
    public float tempoOnda = 15;
    public int maxInimigosOnda;
    public int onda = 1;

    public GameObject enemie;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ContadorTempoOnda();
    }

    public void ContadorTempoOnda()
    {
        contaTempoOnda += Time.deltaTime;

        if(contaTempoOnda >= tempoOnda)
        {
            RandomSpawnEnemies();
            onda++;
            contaTempoOnda = 0;
            maxInimigosOnda++;
        }
    }

    public void RandomSpawnEnemies()
    {
        int rand = Random.Range(1, maxInimigosOnda);

        for(int i = 1; i <= rand; i++)
        {
            Instantiate(enemie, transform.position, Quaternion.identity);
        }
    }

}
