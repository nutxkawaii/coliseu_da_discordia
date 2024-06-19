using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mystimor
{

    public class TaskBase : MonoBehaviour
    {
        private Collider objCollider;
        [SerializeField] private TaskInfo taskInfo;
        [SerializeField] private GameObject highLight, infoIcon;

        public TaskInfo TaskInfo { get => taskInfo; }
        public GameObject InfoIcon { get => infoIcon; }
        public GameObject HighLight { get => highLight; }

        private void Awake()
        {
            objCollider = GetComponent<Collider>();
        }


        public void ActivateTasks()
        {
            highLight.SetActive(true);
            infoIcon.SetActive(true);
            objCollider.enabled = true;
        }

        public void TaskComplete()
        {
            highLight.SetActive(false);
            infoIcon.SetActive(false);
            objCollider.enabled = false;
        }

       

    }



    // O objetivo desse scrip é : 1* dizer em qual área a tarefa está - 2* dizer a descrição da tarefa - 3º o transforme da localização da tarefa
    [System.Serializable]
    public struct TaskInfo
    {
        public string taskArea;
        public string taskDescription;
        public Transform taskLocation;
    }

}