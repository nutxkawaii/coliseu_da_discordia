using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Mystimor
{


    public class GameUI : MonoBehaviour
    {
        
        [SerializeField] GameObject miniMapCamera;

        public void ActivateMapButton()
        {
            miniMapCamera.SetActive(!miniMapCamera.activeInHierarchy);
        }

    }

}
