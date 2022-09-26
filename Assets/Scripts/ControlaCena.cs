using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaCena : MonoBehaviour
{
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("Cena"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cena(int a)
    {
        SceneManager.LoadScene(a);
    }

    public void IniciarJogo()
    {
        PlayerPrefs.SetInt("Cena", 1);
        SceneManager.LoadScene(1);
    }

    public void ContinuarJogo()
    {
        int cenaAtual = PlayerPrefs.GetInt("Cena");
        SceneManager.LoadScene(cenaAtual);
    }
}
