using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Mystimor
{
    public class TaskUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI taskTextPrefab; //Pré-fabricado para o elemento de interface do usuário de texto da tarefa.
        [SerializeField] private Transform taskListHolder; //transformação pai para armazenar elementos da interface do usuário da tarefa.
        [SerializeField] private Image taskProgressBar; //Imagem que representa a barra de progresso da tarefa.
        [SerializeField] private Button taskButton; //Botão para interagir com a tarefa
        [SerializeField] private GameObject taskPanel; //GameObject para o painel de tarefas.
        [SerializeField] private TextMeshProUGUI taskPanelTitleText; //elemento de texto para exibir o título do painel de tarefas

        private List<TextMeshProUGUI> taskTextList; //Lista para armazenar elementos da interface do usuário de texto de tarefa instanciada.
        private int taskCompletedCount =0; //Contador para tarefas concluídas.

        private TaskBase _currentTaskInRange; //Referência à tarefa atual no intervalo.
        int _currentTaskIndex; // Índice da tarefa atual

        public TaskBase CurrentTaskInRange { get => _currentTaskInRange; } // Permite apenas retirada de informações da variavel privada currentTaskInRange


        public void PopulateTask(List<TaskInfo> tasks)
        {
            taskTextList = new List<TextMeshProUGUI>(); 

            for (int i = 0; i < tasks.Count; i++)
            {
                TextMeshProUGUI taskText = Instantiate(taskTextPrefab, taskListHolder);//Instancia os elementos da interface do usuário de texto da tarefa para cada tarefa na lista fornecida.
                taskText.text = tasks[i].taskArea + ": " + tasks[i].taskDescription; //Define o texto para cada tarefa.
                taskTextList.Add(taskText);//Atualiza o valor de preenchimento da barra de progresso da tarefa
            }

            taskProgressBar.fillAmount = taskCompletedCount / taskTextList.Count; //Define a o calculo do preechimento da barra de progresso
        }

        public void TaskOpenButton()
        {
            taskPanel.SetActive(true); //Ativa o painel de tarefas.
            //Define o texto do título do painel de tarefas com as informações atuais da tarefa
            taskPanelTitleText.text = _currentTaskInRange.TaskInfo.taskArea + ": " + _currentTaskInRange.TaskInfo.taskDescription;
        }

        public void LeaveTaskButton()
        {
            taskPanel.SetActive(false); //Desativa o painel de tarefas.
        }

        public void CompleteTaskButton()
        {
            taskPanel.SetActive(false ); //Desativa o painel de tarefas.
            taskTextList[_currentTaskIndex].color = Color.green; //Altera a cor do texto da tarefa atual para verde.
            _currentTaskInRange.TaskComplete(); //Marca a tarefa atual como concluída.
            taskCompletedCount++; //Incrementa a contagem de tarefas concluídas.
            taskProgressBar.fillAmount = (float)taskCompletedCount / taskTextList.Count; //Atualiza o valor de preenchimento da barra de progresso da tarefa.

        }

        public void SetPlayerCurrentTask(TaskBase currentTaskInRange, int currentTaskIndex)
        {
            if(currentTaskInRange != null)
            {
                //Define a tarefa atual em intervalo e seu índice
                _currentTaskIndex = currentTaskIndex;
                _currentTaskInRange = currentTaskInRange;
                //Alterna a interactabilidade do botão de tarefa com base na existência ou não de uma tarefa atual.
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
