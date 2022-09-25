using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSlider : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 0, 0);
    }
}
