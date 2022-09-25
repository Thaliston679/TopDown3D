using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 destino;
    public GameObject inimigo;
    public bool atacando = false;
    public GameObject areaAtk;

    //Poderes
    public GameObject[] poder;
    public bool[] poderUsado;
    public float[] poderResp;
    public ParticleSystem[] particulaPoder;

    void Start()
    {
        for(int i = 0; i < poderUsado.Length; i++)
        {
            poderUsado[i] = false;
        }
        agent = GetComponent<NavMeshAgent>();
        destino = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
        Atacar();
        Poderes();
        PauseGame();
    }

    void Mover()
    {
        if (Input.GetMouseButtonDown(0))
        {
            inimigo = null;

            Vector3 mousepoint = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mousepoint);
            RaycastHit hit;
            if(Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                destino = hit.point;
            }
        }

        agent.SetDestination(destino);

        if (Vector3.Distance(transform.position, destino) < 1)
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
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.CompareTag("Inimigo"))
                {
                    inimigo = hit.collider.gameObject;
                }
            }
        }

        if(inimigo != null)
        {
            destino = inimigo.transform.position;
            agent.SetDestination(destino);

            if(Vector3.Distance(transform.position, destino) < 3)
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

    public void Poderes()
    {
        if(poderUsado[0] == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                poder[0].SetActive(true);
                particulaPoder[0].Play();
                poderUsado[0] = true;
            }
        }
        else
        {
            poderResp[0] += Time.deltaTime;
            if(poderResp[0] >= 4)
            {
                poder[0].SetActive(false);
                particulaPoder[0].Stop();
            }
            if(poderResp[0] >= 10)
            {
                poderResp[0] = 0;
                poderUsado[0] = false;
            }
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

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }

        }
    }
}
