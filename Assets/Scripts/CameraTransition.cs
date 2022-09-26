using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public int cameraReference;
    public bool inTransition;

    public GameObject camera0;
    public GameObject camera1;

    public Vector3[] refCameraPos;
    public Quaternion[] refCameraRot;

    void Start()
    {
        refCameraPos[0] = camera0.transform.position;
        refCameraRot[0] = camera0.transform.rotation;

        refCameraPos[1] = camera1.transform.position;
        refCameraRot[1] = camera1.transform.rotation;

        transform.SetPositionAndRotation(camera0.transform.position, camera0.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (inTransition) InTransition();
    }

    public void InTransition()
    {
        transform.position = Vector3.Lerp(refCameraPos[0], refCameraPos[1], 2.5f * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(refCameraRot[0], refCameraRot[1], 2.5f * Time.deltaTime);
        //transform.SetPositionAndRotation(Vector3.Lerp(refCameraPos[0], refCameraPos[1], 2.5f * Time.deltaTime), Quaternion.Lerp(refCameraRot[0], refCameraRot[1], 2.5f * Time.deltaTime));
    }
}
