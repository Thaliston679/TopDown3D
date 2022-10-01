using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    private float contaTempoOnda;
    public float tempoOnda = 15;
    public int inimigosOnda;
    public int onda = 1;
    public int maxOndaLevel;
    public int fase;
    public Transform[] spawnPosition;

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
            GeradorDeInimigos();
            onda++;
            contaTempoOnda = 0;
            inimigosOnda++;
        }
    }

    public void GeradorDeInimigos()
    {
        for(int i = 1; i <= inimigosOnda; i++)
        {
            Instantiate(enemie, transform.position, Quaternion.identity);
        }
    }

    public void ChecagemDeOndasPorLevel()
    {
        switch (fase)
        {
            case 1:

                break;

        }
    }

}
