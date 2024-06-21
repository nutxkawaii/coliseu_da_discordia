using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mystimor
{

    public class TaskBase : MonoBehaviour
    {
        [Header("Login Sistem")]
        private Collider objCollider; // Armazena o componente collider do objeto de tarefa no mapa.

        [Tooltip("Campo para adicionar informações sobre a tarefa")]
        [SerializeField] private TaskInfo taskInfo;

        [Tooltip("Objeto Que representa o local onde a tarefa deve ser executada")]
        [SerializeField] private GameObject highLight, infoIcon;


        public TaskInfo TaskInfo { get => taskInfo; }
        public GameObject InfoIcon { get => infoIcon; }
        public GameObject HighLight { get => highLight; }

        /* em relação a utilização de = private GameObject highLight | depois | public GameObject HighLight { get => highLight; }
 
          Uma propriedade é um membro que oferece um mecanismo flexível para ler,
          gravar ou calcular o valor de um campo particular. 
          As propriedades podem ser usadas como se fossem membros de dados públicos, 
          mas são métodos realmente especiais chamados acessadores. 
          Esse recurso permite que os dados sejam acessados facilmente 
          e ainda ajuda a promover a segurança e a flexibilidade dos métodos.*/

        private void Awake()
        {
            objCollider = GetComponent<Collider>(); //Inicializa o objCollider obtendo o componente Collider anexado ao GameObject.

        }


        public void ActivateTasks()
        {
            highLight.SetActive(true); //Ativa o ícone de highLight e informações GameObjects
            infoIcon.SetActive(true);
            objCollider.enabled = true; //Permite que o componente colisor interaja com a tarefa.
        }

        public void TaskComplete()
        {
            highLight.SetActive(false); //Desativa o ícone de highLight e informações GameObjects
            infoIcon.SetActive(false);
            objCollider.enabled = false; //Desativa o componente collider para indicar que a tarefa está concluída.
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
