using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bandeira : MonoBehaviour
{
    public float vida;
    public float vidaMax;
    public Image barHP;
    [Range(0, 1)]
    public float barra;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        barra = 1 / (vidaMax / vida);
        barHP.fillAmount = barra;
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
    }
}
