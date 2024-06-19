using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Mystimor
{
    public class TaskUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI taskTextPrefab;
        [SerializeField] private Transform taskListHolder;
        [SerializeField] private Image taskProgressBar;
        [SerializeField] private Button taskButton;
        [SerializeField] private GameObject taskPanel;
        [SerializeField] private TextMeshProUGUI taskPanelTitleText;

        private List<TextMeshProUGUI> taskTextList;
        private int taskCompletedCount =0;

        private TaskBase _currentTaskInRange;
        int _currentTaskIndex;

        public TaskBase CurrentTaskInRange { get => _currentTaskInRange; }


        public void PopulateTask(List<TaskInfo> tasks)
        {
            taskTextList = new List<TextMeshProUGUI>();

            for (int i = 0; i < tasks.Count; i++)
            {
                TextMeshProUGUI taskText = Instantiate(taskTextPrefab, taskListHolder);
                taskText.text = tasks[i].taskArea + ": " + tasks[i].taskDescription;
                taskTextList.Add(taskText);
            }

            taskProgressBar.fillAmount = taskCompletedCount / taskTextList.Count;
        }

        public void TaskOpenButton()
        {
            taskPanel.SetActive(true);
            taskPanelTitleText.text = _currentTaskInRange.TaskInfo.taskArea + ": " + _currentTaskInRange.TaskInfo.taskDescription;
        }

        public void LeaveTaskButton()
        {
            taskPanel.SetActive(false);
        }

        public void CompleteTaskButton()
        {
            taskPanel.SetActive(false );
            taskTextList[_currentTaskIndex].color = Color.green;
            _currentTaskInRange.TaskComplete();
            taskCompletedCount++;
            taskProgressBar.fillAmount = (float)taskCompletedCount / taskTextList.Count;

        }

        public void SetPlayerCurrentTask(TaskBase currentTaskInRange, int currentTaskIndex)
        {
            if(currentTaskInRange != null)
            {
                _currentTaskIndex = currentTaskIndex;
                _currentTaskInRange = currentTaskInRange;
                taskButton.interactable = true;
            }
            else 
            {
                _currentTaskInRange = null;
                taskButton.interactable= false;
            }
        }
    }

}
