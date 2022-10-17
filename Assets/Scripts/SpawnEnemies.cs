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
    public GameObject bossGO;
    public GerenciadorJogo GJ;

    private void Start()
    {
        contaTempoOnda = tempoOnda - 0.1f;
    }

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
            RandomizadorInimigoBoss();
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
            int rand = Random.Range(0, enemies.Length);
            GameObject enemie = enemies[rand];
            //GameObject enemie = RandomizadorInimigo();
            Instantiate(enemie, spawnRand.position, Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
    }

    public void GeradorDeInimigos()
    {
        for(int i = 1; i <= inimigosOnda; i++)
        {
            int rand = Random.Range(0, enemies.Length);
            GameObject enemie = enemies[rand];
            //GameObject enemie = RandomizadorInimigo();
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
            GJ.GetControlaCena().NextLevel();
            GJ.GetControlaCena().Cena(4);
            Debug.Log(PlayerPrefs.GetInt("Fase"));
            /*
            GJ.minhaCena++;
            PlayerPrefs.SetInt("Fase", GJ.minhaCena);
            GJ.GetControlaCena().Cena(GJ.minhaCena);
            */
        }

        if (onda >= maxOndaLevel && GJ.minhaCena == 3 && GJ.inimigosDerrotados >= GJ.inimigosTotais)
        {
            if (PlayerPrefs.HasKey("BossDefeated"))
            {
                if(PlayerPrefs.GetInt("BossDefeated")  == 1)
                {
                    GJ.GetControlaCena().Cena(6);
                }
            }
            else
            {
                PlayerPrefs.SetInt("EndGame", 1);
                Debug.Log("Inimigos elimidados. Aguardando matar chefe...");
            }
        }
    }

    public void RandomizadorInimigoBoss()
    {
        if(onda >= 15)
        {
            Transform spawnRand = spawnPosition[Random.Range(0, spawnPosition.Length)];
            Instantiate(bossGO, spawnRand.position, Quaternion.identity);
        }
    }

}
