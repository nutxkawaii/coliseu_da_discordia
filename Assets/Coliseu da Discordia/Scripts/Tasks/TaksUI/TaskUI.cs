using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Mystimor
{
    public class TaskUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI taskTextPrefab; //Pr�-fabricado para o elemento de interface do usu�rio de texto da tarefa.
        [SerializeField] private Transform taskListHolder; //transforma��o pai para armazenar elementos da interface do usu�rio da tarefa.
        [SerializeField] private Image taskProgressBar; //Imagem que representa a barra de progresso da tarefa.
        [SerializeField] private Button taskButton; //Bot�o para interagir com a tarefa
        [SerializeField] private GameObject taskPanel; //GameObject para o painel de tarefas.
        [SerializeField] private TextMeshProUGUI taskPanelTitleText; //elemento de texto para exibir o t�tulo do painel de tarefas

        private List<TextMeshProUGUI> taskTextList; //Lista para armazenar elementos da interface do usu�rio de texto de tarefa instanciada.
        private int taskCompletedCount =0; //Contador para tarefas conclu�das.

        private TaskBase _currentTaskInRange; //Refer�ncia � tarefa atual no intervalo.
        int _currentTaskIndex; // �ndice da tarefa atual

        public TaskBase CurrentTaskInRange { get => _currentTaskInRange; } // Permite apenas retirada de informa��es da variavel privada currentTaskInRange


        public void PopulateTask(List<TaskInfo> tasks)
        {
            taskTextList = new List<TextMeshProUGUI>(); 

            for (int i = 0; i < tasks.Count; i++)
            {
                TextMeshProUGUI taskText = Instantiate(taskTextPrefab, taskListHolder);//Instancia os elementos da interface do usu�rio de texto da tarefa para cada tarefa na lista fornecida.
                taskText.text = tasks[i].taskArea + ": " + tasks[i].taskDescription; //Define o texto para cada tarefa.
                taskTextList.Add(taskText);//Atualiza o valor de preenchimento da barra de progresso da tarefa
            }

            taskProgressBar.fillAmount = taskCompletedCount / taskTextList.Count; //Define a o calculo do preechimento da barra de progresso
        }

        public void TaskOpenButton()
        {
            taskPanel.SetActive(true); //Ativa o painel de tarefas.
            //Define o texto do t�tulo do painel de tarefas com as informa��es atuais da tarefa
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
            _currentTaskInRange.TaskComplete(); //Marca a tarefa atual como conclu�da.
            taskCompletedCount++; //Incrementa a contagem de tarefas conclu�das.
            taskProgressBar.fillAmount = (float)taskCompletedCount / taskTextList.Count; //Atualiza o valor de preenchimento da barra de progresso da tarefa.

        }

        public void SetPlayerCurrentTask(TaskBase currentTaskInRange, int currentTaskIndex)
        {
            if(currentTaskInRange != null)
            {
                //Define a tarefa atual em intervalo e seu �ndice
                _currentTaskIndex = currentTaskIndex;
                _currentTaskInRange = currentTaskInRange;
                //Alterna a interactabilidade do bot�o de tarefa com base na exist�ncia ou n�o de uma tarefa atual.
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
