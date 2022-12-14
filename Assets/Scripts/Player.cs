using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 destino;
    public GameObject inimigo;
    public bool atacando = false;
    public GameObject areaAtk;

    public float vida;
    public float vidaMax;
    public Image barHP;
    [Range(0, 1)]
    public float barra;

    //Poderes
    public GameObject[] poder;
    public Image[] poderBar;
    public bool[] poderUsado;
    public float[] poderResp;
    public ParticleSystem[] particulaPoder;

    public GameObject rabbitClone;

    void Start()
    {
        for(int i = 0; i < poderUsado.Length; i++)
        {
            poderUsado[i] = false;
        }
        agent = GetComponent<NavMeshAgent>();
        destino = transform.position;
        vida = vidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
        Atacar();
        Poderes();
        PoderesMobile();
        BarraHP();
        BarraSkills();
    }

    public void BarraSkills()
    {
        float barSkillQ;
        float barSkillW;

        if(poderResp[0] > 0)
        {
            barSkillQ = poderResp[0] / 10;
        }
        else
        {
            barSkillQ = 1;
        }

        if (poderResp[1] > 0)
        {
            barSkillW = poderResp[1] / 10;
        }
        else
        {
            barSkillW = 1;
        }

        poderBar[0].fillAmount = barSkillQ;
        poderBar[1].fillAmount = barSkillW;
    }

    public void BarraHP()
    {
        barra = 1 / (vidaMax / vida);
        //barHP.rectTransform.localScale = new(barra, 1, 1);
        barHP.fillAmount = barra;
    }

    void Mover()
    {
        if (Input.GetMouseButton(0))
        {
            inimigo = null;

            Vector3 mousepoint = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mousepoint);
            RaycastHit hit;
            if(Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                destino = hit.point;
                //Debug.Log(destino);
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

#if PLATFORM_ANDROID
        if (Input.GetMouseButtonDown(0))
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
#endif

        if (inimigo != null)
        {
            destino = inimigo.transform.position;
            agent.SetDestination(destino);

            if(Vector3.Distance(transform.position, destino) < 4)
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
        //Poder 1
        if(poderUsado[0] == false)
        {
            if (Input.GetKeyDown(KeyCode.Q))
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

        //Poder 2
        if (poderUsado[1] == false)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Instantiate(rabbitClone, transform.position, Quaternion.identity);
                poderUsado[1] = true;
            }
        }
        else
        {
            poderResp[1] += Time.deltaTime;
            if (poderResp[1] >= 10)
            {
                poderResp[1] = 0;
                poderUsado[1] = false;
            }
        }
    }

#if PLATFORM_ANDROID

    public void PoderesMobile()
    {
        //Poder 1

        if (poderUsado[0] == true)
        {
            poderResp[0] += Time.deltaTime;
            if (poderResp[0] >= 4)
            {
                poder[0].SetActive(false);
                particulaPoder[0].Stop();
            }
            if (poderResp[0] >= 10)
            {
                poderResp[0] = 0;
                poderUsado[0] = false;
            }
        }

        //Poder 2

        if (poderUsado[1] == true)
        {
            poderResp[1] += Time.deltaTime;
            if (poderResp[1] >= 10)
            {
                poderResp[1] = 0;
                poderUsado[1] = false;
            }
        }
    }

    public void SkillQ()
    {
        if (poderUsado[0] == false)
        {
            poder[0].SetActive(true);
            particulaPoder[0].Play();
            poderUsado[0] = true;
        }
    }

    public void SkillW()
    {
        if (poderUsado[1] == false)
        {
            Instantiate(rabbitClone, transform.position, Quaternion.identity);
            poderUsado[1] = true;
        }
    }

#endif

    public void AtivarAtk()
    {
        areaAtk.SetActive(true);
    }

    public void DesativarAtk()
    {
        areaAtk.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AreaAtkInimigo"))
        {
            vida--;
            if (vida <= 0)
            {
                ControlaCena controlaCena = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControlaCena>();
                controlaCena.Cena(5);
            }
        }

        if (other.gameObject.CompareTag("HP"))
        {
            if(vida < vidaMax)
            {
                other.gameObject.SetActive(false);
                SpawnCollectables spawnHP = GameObject.FindGameObjectWithTag("spawnHP").GetComponent<SpawnCollectables>();
                spawnHP.CheckCollect();
                vida += 5;
                if (vida > vidaMax) vida = vidaMax;
            }
        }
    }
}
