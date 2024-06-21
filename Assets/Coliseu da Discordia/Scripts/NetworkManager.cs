using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;




 /*NetworkManager: Herda de MonoBehaviourPunCallbacks,
  fornecendo retornos de chamada de rede Photon,
  e MonoBehaviour, a classe base para todos os scripts Unity.*/

public class NetworkManager : MonoBehaviourPunCallbacks
    {
        
        public static NetworkManager instance;
    /*  Transforma esse script Network Manager para um singleton, Usamos apenas quando temos certeza que APENAS UM OBJETO obter� essa classe[script] 
        [Beneficio] Podemos acessar esse scrip de qualquer objeto que fizermos refer�ncia ao Network Manager e suas funcionalidades
     */
        [Header("Login Sistem")]
        [Tooltip("Campo de entrada para o apelido do jogador")]
        public TMP_InputField inputNickName; // Campo para digitar o nome do jogador ++++++++

        [Tooltip("Refer�ncia � tela de menu GameObject")]
        public GameObject menuScreen; //alterar para MenuController ++++++++

        [Tooltip("Refer�ncia � tela de carregamento GameObject")]
        public GameObject loadingScreen; //alterar para MenuController ++++++++

        [Tooltip("Refer�ncia � tela de tarefas GameObject")]
        public GameObject taskScreen; //alterar para MenuController ++++++++

       


        /* List<Photon.Realtime.Player> players;
        Pedir ao Cleber para Criando uma lista com o nome de todos os jogadores da rede 
        podemos usar esse recurso para a vota��o quando o jogador assasino for suspeito */

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                menuScreen.SetActive(true);
                loadingScreen.SetActive(false);
                taskScreen.SetActive(false);
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        private void Start()
        {
            Debug.Log("Start");


        }

        //M�todo para realizar a conec��o com o Photon
        public void ConnectToPhoton()
        {
            Debug.Log("ConnectToPhoton");
            loadingScreen.SetActive(true);
            menuScreen.SetActive(false);

            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaste");
            PhotonNetwork.JoinLobby();
        }
        //Procura uma sala aleat�ria para se conectar
        public override void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby");
            PhotonNetwork.JoinRandomRoom();
        }

        /* Quando nao conseguir entrar em uma sala aleat�ria
        Criar uma sala para se conectar null seria o nome da sala, mas nao precisamos especificar*/
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("OnJoinRandomFailed");
            PhotonNetwork.CreateRoom(null);
        }

        //Crie uma sala
        public override void OnCreatedRoom()
        {
            Debug.Log("OnCreatedRoom");
            //Debug.Log("Sala Criada");

        }
        //Entre na sala cria e me diga quantos jogadores est�o conectados a ela
        public override void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom");
            Debug.Log("Playercount: " + PhotonNetwork.CurrentRoom.PlayerCount);
            Debug.Log("NickName: " + inputNickName.text);
            //PhotonNetwork.LocalPlayer.NickName = inputNickName.text;
            GameManager.instance.StartGame();
            // photonView.RPC("CreatePlayerAvatar", PhotonNetwork.LocalPlayer);
            taskScreen.SetActive(true);
        }



    }


