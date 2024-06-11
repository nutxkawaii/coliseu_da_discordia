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

    // O objetivo desse scrip � : 1* dizer em qual �rea a tarefa est� - 2* dizer a descri��o da tarefa - 3� o transforme da localiza��o da tarefa
    [System.Serializable]
    public struct TaskInfo
    {
        public string area;
        public string taskDescription;
        public Transform taskLocation;
    }

}