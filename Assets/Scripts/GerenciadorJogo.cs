using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorJogo : MonoBehaviour
{
    public int minhaCena;
    private ControlaCena controlaCena;

    // Start is called before the first frame update
    void Start()
    {
        minhaCena = PlayerPrefs.GetInt("Cena");
        controlaCena = GetComponent<ControlaCena>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            minhaCena++;
            PlayerPrefs.SetInt("Cena", minhaCena);
            controlaCena.Cena(minhaCena);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            controlaCena.Cena(5);
        }
    }
}
