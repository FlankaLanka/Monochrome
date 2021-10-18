using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDisabler : MonoBehaviour
{
    public GameObject CamContainer;
    public int numCameras;
    public int CamerasLeft;

    private void Start()
    {
        numCameras = CamContainer.transform.childCount;
        CamerasLeft = CamContainer.transform.childCount;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && CamerasLeft > 1)
        {
            CamContainer.transform.GetChild(numCameras - CamerasLeft).gameObject.SetActive(false);
            CamerasLeft--;
        }
    }
}
