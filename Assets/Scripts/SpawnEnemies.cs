using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    private float contaTempoOnda;
    public float tempoOnda = 10;
    public int maxInimigosOnda;

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
            contaTempoOnda = 0;
        }
    }

    public void RandomSpawnEnemies()
    {
        int rand = Random.Range(1, maxInimigosOnda);

        for(int i = 1; i <= maxInimigosOnda; i++)
        {
            Instantiate(enemie, transform.position, Quaternion.identity);
        }
    }

}
