using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RabbitClone : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 destino;
    public GameObject inimigo;
    public bool atacando = false;
    public GameObject areaAtk;
    public float drenarVida = 0.5f;

    public float vida;
    public float vidaMax = 10;
    public Image barHP;
    [Range(0, 1)]
    public float barra;
    public GameObject particleDestroy;

    void Start()
    {
        vida = vidaMax;
        agent = GetComponent<NavMeshAgent>();
        destino = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
        Atacar();
        LifeTimer();
        BarraHP();
    }

    public void LifeTimer()
    {
        drenarVida -= Time.deltaTime;
        if(drenarVida <= 0)
        {
            drenarVida = 0.5f;
            vida--;
        }

        if (vida <= 0)
        {
            Instantiate(particleDestroy, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void BarraHP()
    {
        barra = 1 / (vidaMax / vida);
        barHP.rectTransform.localScale = new(barra, 1, 1);
    }

    void Mover()
    {
        if (Input.GetMouseButton(0))
        {
            inimigo = null;

            Vector3 mousepoint = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mousepoint);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                destino = hit.point;
                Debug.Log(destino);
            }
        }
        

        agent.SetDestination(destino);

        if (Vector3.Distance(transform.position, destino) <= 1)
        {
            GetComponent<Animator>().SetBool("Andando", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Andando", true);
        }
    }

    void Atacar()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousepoint = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mousepoint);
            if (Physics.Raycast(castPoint, out RaycastHit hit, Mathf.Infinity, 7))
            {
                if (hit.collider.gameObject.CompareTag("Inimigo"))
                {
                    inimigo = hit.collider.gameObject;
                }
            }
        }

        if (inimigo != null)
        {
            destino = inimigo.transform.position;
            agent.SetDestination(destino);

            if (Vector3.Distance(transform.position, destino) < 3)
            {
                GetComponent<Animator>().SetBool("Atacando", true);
                transform.LookAt(destino);
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("Atacando", false);
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
