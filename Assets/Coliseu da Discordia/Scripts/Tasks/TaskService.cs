using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mystimor 
{ 
    public class TaskService : MonoBehaviour
    {
        private static TaskService instance;

        [SerializeField] private TaskUI taskUI;
        [SerializeField] private TaskBase[] taskArray;

        public static TaskService Instance { get => instance; }


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

            }
            else
            {
                Destroy(gameObject);
            }
        }
       

        public List<TaskBase> GetTasks(bool isPlayer)
        {
            List<TaskBase> newTasks = new List<TaskBase>();
            List<TaskInfo> tasksInfoList = new List<TaskInfo>();

            for (int i = 0; i < 4; i++) 
            {
                int randomNumber = Random.Range(0, taskArray.Length);

                while (newTasks.Contains(taskArray[randomNumber]))
                {
                    randomNumber = Random.Range(0, taskArray.Length);
                }

                newTasks.Add(taskArray[randomNumber]);
                if (isPlayer)
                {
                    taskArray[randomNumber].ActivateTasks();
                    tasksInfoList.Add(taskArray[randomNumber].TaskInfo );
                }
            }

            if (isPlayer)
            {
                taskUI.PopulateTask(tasksInfoList);
            }

            return newTasks;
        }
   

    }

}