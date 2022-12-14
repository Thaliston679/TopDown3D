using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnEnemies : MonoBehaviour
{
    private float contaTempoOnda;
    public float contaTempoOndaNaTela = 0;
    public float tempoOnda = 15;
    public int inimigosOnda;
    public int onda = 1;
    public int maxOndaLevel;
    public int fase;
    public Transform[] spawnPosition;
    public GameObject[] enemies;
    public GameObject bossGO;
    public GerenciadorJogo GJ;
    public GameObject ondaPanel;
    public TextMeshProUGUI ondaTimer;

    private void Start()
    {
        contaTempoOnda = tempoOnda - 0.1f;
        contaTempoOndaNaTela = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GJ.inimigosDerrotados.ToString() + " " + GJ.inimigosTotais.ToString());
        ChecagemDeOndasPorLevel();
        if (GJ.inimigosDerrotados >= GJ.inimigosTotais) InstaciaNovaOnda();
        ContadorTempoOnda();
    }

    public void ContadorTempoOnda()
    {
        if(GJ.inimigosDerrotados >= GJ.inimigosTotais)
        {
            Debug.Log(contaTempoOnda + "T");
            if (!ondaPanel.activeSelf) ondaPanel.SetActive(true);
            contaTempoOnda += Time.deltaTime;
            ondaTimer.text = "Tempo at? a pr?xima onda: " + ((int)contaTempoOndaNaTela).ToString();
            contaTempoOndaNaTela -= Time.deltaTime;
        }
    }

    public void InstaciaNovaOnda()
    {
        if (contaTempoOnda >= tempoOnda && onda < maxOndaLevel)
        {
            GJ.inimigosTotais += inimigosOnda;
            StartCoroutine(GeradorDeInimigosComTimer(inimigosOnda+1));
            onda++;
            contaTempoOnda = 0;
            ondaPanel.SetActive(false);
            contaTempoOndaNaTela = tempoOnda;
            inimigosOnda++;
            RandomizadorInimigoBoss();
        }
    }

    public void PularEsperaEntreOndas()
    {
        contaTempoOnda = tempoOnda;
        contaTempoOndaNaTela = 0;
    }
    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// 

    IEnumerator GeradorDeInimigosComTimer(int currentInimigosOnda)
    {
        for (int i = 1; i < currentInimigosOnda; i++)
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
