using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class InimigoAI : MonoBehaviour
{
    private GameObject player;
    private GameObject toca;
    private GameObject destino;
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
    public GameObject areaAtk;

    public GameObject particleDestroy;
    void Start()
    {
        vida = vidaMax;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        toca = GameObject.FindGameObjectWithTag("Toca");
        GJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<GerenciadorJogo>();
        destino = toca;

        agent.SetDestination(toca.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 5) destino = player;
        else destino = toca;

        if (destino == player)
        {
            agent.SetDestination(player.transform.position);
        }
        if(destino == toca)
        {
            agent.SetDestination(toca.transform.position);
        }

        BarraHP();
        PodeLevarDano();

        if (Vector3.Distance(transform.position, player.transform.position) < 3)
        {
            GetComponent<Animator>().SetBool("Atacando", true);
            transform.LookAt(player.transform.position);
        }

        AnimationControl();
    }

    void AnimationControl() 
    {
        if (Vector3.Distance(transform.position, destino.transform.position) <= 2)
        {
            GetComponent<Animator>().SetBool("Andando", false);
            GetComponent<Animator>().SetBool("Atacando", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Andando", true);
            GetComponent<Animator>().SetBool("Atacando", false);
        }
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

    public void AtivarAtk()
    {
        areaAtk.SetActive(true);
    }

    public void DesativarAtk()
    {
        areaAtk.SetActive(false);
    }
}
