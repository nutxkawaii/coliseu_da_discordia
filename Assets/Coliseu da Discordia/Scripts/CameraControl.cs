using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraControl : MonoBehaviourPun
{
    [SerializeField] GameObject myCamera;


    private void Awake()
    {
        if (photonView.IsMine)
        {
            GameManager.instance.cameraPlayer = myCamera.transform;
        }
        
        else
        {
            myCamera.SetActive(false);

        }

         
    }
}
