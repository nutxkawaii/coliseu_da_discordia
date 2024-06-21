using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mystimor
{

    public class TaskBase : MonoBehaviour
    {
        [Header("Login Sistem")]
        private Collider objCollider; // Armazena o componente collider do objeto de tarefa no mapa.

        [Tooltip("Campo para adicionar informa��es sobre a tarefa")]
        [SerializeField] private TaskInfo taskInfo;

        [Tooltip("Objeto Que representa o local onde a tarefa deve ser executada")]
        [SerializeField] private GameObject highLight, infoIcon;


        public TaskInfo TaskInfo { get => taskInfo; }
        public GameObject InfoIcon { get => infoIcon; }
        public GameObject HighLight { get => highLight; }

        /* em rela��o a utiliza��o de = private GameObject highLight | depois | public GameObject HighLight { get => highLight; }
 
          Uma propriedade � um membro que oferece um mecanismo flex�vel para ler,
          gravar ou calcular o valor de um campo particular. 
          As propriedades podem ser usadas como se fossem membros de dados p�blicos, 
          mas s�o m�todos realmente especiais chamados acessadores. 
          Esse recurso permite que os dados sejam acessados facilmente 
          e ainda ajuda a promover a seguran�a e a flexibilidade dos m�todos.*/

        private void Awake()
        {
            objCollider = GetComponent<Collider>(); //Inicializa o objCollider obtendo o componente Collider anexado ao GameObject.

        }


        public void ActivateTasks()
        {
            highLight.SetActive(true); //Ativa o �cone de highLight e informa��es GameObjects
            infoIcon.SetActive(true);
            objCollider.enabled = true; //Permite que o componente colisor interaja com a tarefa.
        }

        public void TaskComplete()
        {
            highLight.SetActive(false); //Desativa o �cone de highLight e informa��es GameObjects
            infoIcon.SetActive(false);
            objCollider.enabled = false; //Desativa o componente collider para indicar que a tarefa est� conclu�da.
        }

       

    }



    // O objetivo desse scrip � : 1* dizer em qual �rea a tarefa est� - 2* dizer a descri��o da tarefa - 3� o transforme da localiza��o da tarefa
    [System.Serializable]
    public struct TaskInfo
    {
        public string taskArea;
        public string taskDescription;
        public Transform taskLocation;
    }

}
