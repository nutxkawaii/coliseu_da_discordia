using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mystimor 
{ 
    public class TaskService : MonoBehaviour
    {
        [SerializeField] private TaskBase[] tasks;

        public TaskBase[] Tasks { get => tasks; }
   
    }

}