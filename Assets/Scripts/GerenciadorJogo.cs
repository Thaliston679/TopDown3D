using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GerenciadorJogo : MonoBehaviour
{
    public int minhaCena;
    private ControlaCena controlaCena;
    public int inimigosDerrotados;
    public int inimigosTotais;
    public GameObject inGamePainel;
    public GameObject pausePainel;
    private SpawnEnemies spawnEnemies;
    public GameObject UIondas;
    public TextMeshProUGUI fps;
    private float fpsTimer = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        spawnEnemies = GameObject.FindGameObjectWithTag("SpawnEnemies").GetComponent<SpawnEnemies>();
        minhaCena = PlayerPrefs.GetInt("Cena");
        controlaCena = GetComponent<ControlaCena>();
    }

    // Update is called once per frame
    void Update()
    {
       CheckPauseGame();
    }

    public void VisualizarFPS()
    {
        fpsTimer -= Time.deltaTime;
        if (fpsTimer <= 0)
        {
            fpsTimer = 0.25f;
            fps.text = "FPS: " + ((int)(1 / Time.deltaTime)).ToString();
        }
    }

    public ControlaCena GetControlaCena()
    {
        return controlaCena;
    }

    public void CheckPauseGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
            
        }
    }

    public void PauseGame()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pausePainel.SetActive(false);
            inGamePainel.SetActive(true);
        }
        else if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pausePainel.SetActive(true);
            inGamePainel.SetActive(false);
        }
    }
    public void MenuIniciar()
    {
        PauseGame();
        controlaCena.Cena(0);
    }

    private void LateUpdate()
    {
        TextMeshProUGUI text = UIondas.GetComponent<TextMeshProUGUI>();
        text.text = "Onda " + spawnEnemies.onda.ToString() + " / " + spawnEnemies.maxOndaLevel.ToString();
        VisualizarFPS();
    }
}
