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

    public GerenciadorJogo GJ;

    // Update is called once per frame
    void Update()
    {
        if (GJ.inimigosDerrotados >= GJ.inimigosTotais) InstaciaNovaOnda();
        ContadorTempoOnda();
        ChecagemDeOndasPorLevel();
    }

    public void ContadorTempoOnda()
    {
        contaTempoOnda += Time.deltaTime;
    }

    public void InstaciaNovaOnda()
    {
        if (contaTempoOnda >= tempoOnda)
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
            GJ.inimigosTotais++;
            Instantiate(enemie, transform.position, Quaternion.identity);
        }
    }

    public void ChecagemDeOndasPorLevel()
    {
        if(onda >= maxOndaLevel && GJ.minhaCena <= 2 && GJ.minhaCena > 0 && GJ.inimigosDerrotados >= GJ.inimigosTotais)
        {
            GJ.minhaCena++;
            PlayerPrefs.SetInt("Cena", GJ.minhaCena);
            GJ.GetControlaCena().Cena(GJ.minhaCena);
        }
    }

}
