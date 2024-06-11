using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mystimor
{

    public class TaskBase : MonoBehaviour
    {
        [SerializeField] private TaskInfo taskInfo;
        [SerializeField] private GameObject highLight, mapIcon;

        public TaskInfo TaskInfo { get => taskInfo; }

        private Collider objCollider;

        private void Awake()
        {
            objCollider = GetComponent<Collider>();
        }
    }

    // O objetivo desse scrip é : 1* dizer em qual área a tarefa está - 2* dizer a descrição da tarefa - 3º o transforme da localização da tarefa
    [System.Serializable]
    public struct TaskInfo
    {
        public string area;
        public string taskDescription;
        public Transform taskLocation;
    }

}