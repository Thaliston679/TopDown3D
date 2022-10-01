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
    public GameObject[] enemies;

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
        if (contaTempoOnda >= tempoOnda && onda < maxOndaLevel)
        {
            GJ.inimigosTotais += inimigosOnda;
            StartCoroutine(GeradorDeInimigosComTimer());
            onda++;
            contaTempoOnda = 0;
            inimigosOnda++;
        }
    }
    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// 

    IEnumerator GeradorDeInimigosComTimer()
    {
        for (int i = 1; i < inimigosOnda; i++)
        {
            Transform spawnRand = spawnPosition[Random.Range(0, spawnPosition.Length)];
            GameObject enemie = RandomizadorInimigo();
            Instantiate(enemie, spawnRand.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void GeradorDeInimigos()
    {
        for(int i = 1; i <= inimigosOnda; i++)
        {
            GameObject enemie = RandomizadorInimigo();
            GJ.inimigosTotais++;
            Instantiate(enemie, transform.position, Quaternion.identity);
        }
    }
    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////
    /// </summary>

    public void ChecagemDeOndasPorLevel()
    {
        if(onda >= maxOndaLevel && GJ.minhaCena <= 2 && GJ.minhaCena > 0 && GJ.inimigosDerrotados >= GJ.inimigosTotais)
        {
            GJ.minhaCena++;
            PlayerPrefs.SetInt("Cena", GJ.minhaCena);
            GJ.GetControlaCena().Cena(GJ.minhaCena);
        }
    }

    public GameObject RandomizadorInimigo()
    {
        if(onda > 20)
        {
            int rand = Random.Range(0, 3);
            return enemies[rand];
        }
        else if(onda > 10)
        {
            int rand = Random.Range(0, 2);
            return enemies[rand];
        }
        else
        {
            int rand = Random.Range(0, 1);
            return enemies[rand];
        }
    }

}
