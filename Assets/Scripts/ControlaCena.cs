using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaCena : MonoBehaviour
{
    int cenaAtual;

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
        if(PlayerPrefs.HasKey("Cena"))
        {
            cenaAtual = PlayerPrefs.GetInt("Cena");
        }
        else
        {
            cenaAtual = 1;
            PlayerPrefs.SetInt("Cena", 1);
        }

        SceneManager.LoadScene(cenaAtual);
    }
}
