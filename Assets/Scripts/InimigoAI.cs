using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InimigoAI : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    public int vida = 5;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AreaAtk"))
        {
            vida--;
            if(vida <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
