using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Typewriter : MonoBehaviour
{
    public TextMeshProUGUI textGO;
    public float timeDelayWrite = 0.01f;
    [TextArea(1, 10)]
    public string texto;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MostrarTexto(texto));
    }

    IEnumerator MostrarTexto(string textType)
    {
        textGO.text = "";
        for(int i = 0; i < textType.Length; i++)
        {
            textGO.text += textType[i];
            yield return new WaitForSeconds(timeDelayWrite);        }
    }
}
