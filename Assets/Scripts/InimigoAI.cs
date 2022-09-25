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

    public GameObject particleDestroy;
    void Start()
    {
        vida = vidaMax;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
        BarraHP();
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
                Instantiate(particleDestroy, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}
