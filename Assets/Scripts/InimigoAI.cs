using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class InimigoAI : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    public float vida;
    public float vidaMax;
    public Image barHP;
    [Range(0, 1)]
    public float barra;
    //Para habilidades de dano contínuo
    public bool levarDano = true;
    public float tempoInvulneravel;
    public GerenciadorJogo GJ;

    public GameObject particleDestroy;
    void Start()
    {
        vida = vidaMax;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        GJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<GerenciadorJogo>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
        BarraHP();
        PodeLevarDano();
    }

    public void BarraHP()
    {
        barra = 1 / (vidaMax/vida);
        barHP.rectTransform.localScale = new(barra, 1, 1);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AreaAtk"))
        {
            vida--;
            if(vida <= 0)
            {
                GJ.inimigosDerrotados++;
                Instantiate(particleDestroy, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
    

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("AreaAtkContinuo") && levarDano)
        {
            levarDano = false;
            vida--;
            if (vida <= 0)
            {
                GJ.inimigosDerrotados++;
                Instantiate(particleDestroy, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }

    public void PodeLevarDano()
    {
        if (!levarDano)
        {
            tempoInvulneravel += Time.deltaTime;
        }

        if(tempoInvulneravel >= 1)
        {
            levarDano = true;
            tempoInvulneravel = 0;
        }
    }
}
