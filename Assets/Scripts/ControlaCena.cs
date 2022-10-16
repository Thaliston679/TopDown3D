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
        PlayerPrefs.SetInt("Fase", 1);
        SceneManager.LoadScene(1);
    }

    public void ContinuarJogo()
    {
        if(PlayerPrefs.HasKey("Fase"))
        {
            cenaAtual = PlayerPrefs.GetInt("Fase");
        }
        else
        {
            cenaAtual = 1;
            PlayerPrefs.SetInt("Fase", 1);
        }

        SceneManager.LoadScene(cenaAtual);
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("Fase", PlayerPrefs.GetInt("Fase") + 1);
    }
}
